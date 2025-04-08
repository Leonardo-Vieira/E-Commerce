using System;
using e_order.Data;
using e_order.Repository;
using Domain_Core.Bus;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using e_order.Domain.Models;
using e_order;  
using e_order.Domain.IntegrationEvents.Product;
using e_order.Domain.IntegrationsEventHandlers;
using Swashbuckle.AspNetCore.Swagger;
using Autofac.Extensions.DependencyInjection;
using Autofac;
using e_order.Domain.IntegrationEvents.Client;
using e_order.Domain.IntegrationEventHandlers;
using AutoMapper;
using e_order.Common;
using e_order.Domain.IntegrationEvents.Person;
using e_order.Domain.IntegrationEvents.Brand;
using e_order.Data.Repository;
using System.Collections.Generic;
using E_Order.Data;
using E_Order.Domain.Queries.EFQueries;
using E_Order.Domain.Queries;

namespace E_Order
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DataContext>(x => x.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));
            services.AddDbContext<EventStoreContext>(x => x.UseSqlite(Configuration.GetConnectionString("EventStoreConnection")));

            // ######################## USE THIS FOR LOCALHOST DEBUGGING. ############################
            // services.AddEntityFrameworkNpgsql().AddDbContext<DataContext>(opt =>
            //         opt.UseNpgsql(Configuration.GetConnectionString("PostgreSQL")));
            // #######################################################################################

            // ####################### USE THIS FOR DOCKER DEBUGGIING: ###############################
            // services.AddEntityFrameworkNpgsql()
            //         .AddDbContext<DataContext>(options =>
            //     options.UseNpgsql(Configuration["ConnectionStrings:DockerConnectionString"]));
            // #######################################################################################
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", p =>
                {
                    p.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddMvcCore().AddAuthorization().AddJsonFormatters();

           /*  services.AddAuthentication("Bearer")
                    .AddIdentityServerAuthentication(options =>
                    {
                        options.Authority = "https://localhost:5003";
                        options.RequireHttpsMetadata = true;
                        options.ApiName = "Authorization.api";
                    });
 */

            services.AddAuthentication(o =>
                {
                    o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                }
            ).AddIdentityServerAuthentication(o =>
                 {
                    o.RequireHttpsMetadata = false;
                   //  o.Authority = "http://172.20.128.6:5003";
                    
                    o.Authority = "http://localhost:5003";
                 });
            services.AddMediatR();

            var config = new MapperConfiguration(c =>
            {
                c.AddProfile(new ApplicationProfile());
            });

            var mapper = config.CreateMapper();
            services.AddSingleton(mapper);

            // All Repositories are fetched here.
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IEventStoreRepository, EventStoreRepository>();
            services.AddScoped<IOrderItemRepository, OrderItemRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IBrandRepository, BrandRepository>();
            services.AddScoped<IMediatorHandler, InMemoryBus>();
            services.AddScoped<IOrderQueries, EFOrderQueries>();
            services.AddScoped<IBrandQueries, EFBrandQueries>();
            services.AddScoped<IClientQueries, EFClientQueries>();
            services.AddScoped<IProductQueries, EFProductQueries>();
            services.AddSingleton<IEventBus, EventBus>();

            services.AddTransient(typeof(ProductIntegrationEventHandler));
            services.AddTransient(typeof(ClientIntegrationEventHandler));
            //services.AddTransient(typeof(PersonIntegrationEventHandler));
            services.AddTransient(typeof(BrandIntegrationEventHandler));

            var signingConfigurations = new SigningConfigurations();
            services.AddSingleton(signingConfigurations);

            var tokenConfigurations = new TokenConfigurations();
            new ConfigureFromConfigurationOptions<TokenConfigurations>(
                Configuration.GetSection("TokenConfigurations"))
                    .Configure(tokenConfigurations);
            services.AddSingleton(tokenConfigurations);

           /*  services.AddAuthentication(authOptions =>
            {
                authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(bearerOptions =>
             {
                 var paramsValidation = bearerOptions.TokenValidationParameters;
                 paramsValidation.IssuerSigningKey = signingConfigurations.Key;
                 paramsValidation.ValidAudience = tokenConfigurations.Audience;
                 paramsValidation.ValidIssuer = tokenConfigurations.Issuer;

                 paramsValidation.ValidateIssuerSigningKey = true;

                 paramsValidation.ValidateLifetime = true;

                 paramsValidation.ClockSkew = TimeSpan.Zero;
             });

            services.AddAuthorization(auth =>
           {
               auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                   .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                   .RequireAuthenticatedUser().Build());
           });
 */
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "E-Order Microservice API",
                    Description = "E-Order API",
                    TermsOfService = "None",
                });

                var security = new Dictionary<string, IEnumerable<string>>
                {
                    {"Bearer", new string[] {}},
                };

                c.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    Description = "JWT Authorization header. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                });

                c.AddSecurityRequirement(security);
            });

            var container = new ContainerBuilder();
            container.Populate(services);
            container.RegisterModule(new ApplicationModule(Configuration.GetConnectionString("DefaultConnection")));

            return new AutofacServiceProvider(container.Build());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                //app.UseHsts();
            }
            app.UseAuthentication();
            app.UseCors("AllowAll");
            //app.UseHttpsRedirection();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "E-Order API");
            });

            app.UseAuthentication();
            app.UseCors("AllowAll");
            app.UseMvc();
            ConfigureEventBus(app);
        }

        private void ConfigureEventBus(IApplicationBuilder app)
        {
            IEventBus eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
          /*   eventBus.Subscribe<PersonCreatedIntegrationEvent, PersonIntegrationEventHandler>();
            eventBus.Subscribe<PersonRemovedIntegrationEvent, PersonIntegrationEventHandler>();
            eventBus.Subscribe<PersonUpdatedIntegrationEvent, PersonIntegrationEventHandler>(); */

            eventBus.Subscribe<ClientRegisteredIntegrationEvent, ClientIntegrationEventHandler>();
            eventBus.Subscribe<ClientUpdatedIntegrationEvent, ClientIntegrationEventHandler>();
            eventBus.Subscribe<ClientRemovedIntegrationEvent, ClientIntegrationEventHandler>();

            eventBus.Subscribe<ProductCreatedIntegrationEvent, ProductIntegrationEventHandler>();
            eventBus.Subscribe<ProductUpdatedIntegrationEvent, ProductIntegrationEventHandler>();
            eventBus.Subscribe<ProductRemovedIntegrationEvent, ProductIntegrationEventHandler>();

            eventBus.Subscribe<BrandCreatedIntegrationEvent, BrandIntegrationEventHandler>();
            eventBus.Subscribe<BrandRemovedIntegrationEvent, BrandIntegrationEventHandler>();
            eventBus.Subscribe<BrandUpdatedIntegrationEvent, BrandIntegrationEventHandler>();

            eventBus.Consume("queue.order");
        }
    }
}
