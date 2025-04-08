using System;
using Domain_Core.Bus;
using E_Client.Data;
using E_Client.Models;
using E_Client.Domain.IntegrationsEventHandlers;
using E_Client.Repository;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using E_Client.Domain.IntegrationEvents.Product;
using Swashbuckle.AspNetCore.Swagger;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using E_Client.Domain.IntegrationEvents.Brand;
using E_Client.Data.Repository;
using E_Client.Domain.Interface;
using E_Client.Domain.IntegrationEvents.ProductType;
using E_Client.Domain.Events.Order;
using Microsoft.IdentityModel.Tokens;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace E_Client
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DataContext>(opt => opt.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));

            // ######################## USE THIS FOR LOCALHOST DEBUGGING. ############################
            //services.AddEntityFrameworkNpgsql().AddDbContext<DataContext>(opt =>
            //         opt.UseNpgsql(Configuration.GetConnectionString("PostgreSQL")));
            // #######################################################################################

            // ####################### USE THIS FOR DOCKER DEBUGGING. ###############################
            // services.AddEntityFrameworkNpgsql()
            //     .AddDbContext<DataContext>(options =>
            //     options.UseNpgsql(Configuration["ConnectionStrings:DockerConnectionString"]));
            // #####################################################################################

            services.AddMvc();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "E-Client Microservice API",
                    Description = "E-Client API",
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

            services.AddMediatR();
            services.AddCors();

            services.AddScoped(typeof(IClientRepository<Client>), typeof(ClientRepository<Client>));
            services.AddScoped(typeof(IProductRepository), typeof(ProductRepository));
            services.AddScoped(typeof(IBrandRepository), typeof(BrandRepository));
            services.AddScoped(typeof(IProductTypeRepository), typeof(ProductTypeRepository));
            services.AddScoped(typeof(IOrderRepository), typeof(OrderRepository));
            services.AddScoped(typeof(IOrderItemRepository), typeof(OrderItemRepository));
            services.AddScoped<IEventStoreRepository, EventStoreRepository>();

            services.AddTransient<ProductIntegrationEventHandler>();
            services.AddTransient<OrderIntegrationEventHandler>();
            services.AddTransient<BrandIntegrationEventHandler>();
            services.AddTransient<ProductTypeIntegrationEventHandler>();
            services.AddScoped<IMediatorHandler, InMemoryBus>();
            services.AddSingleton<IEventBus, EventBus>();

            services.AddTransient<DataContext>();

            #region Token
            var signingConfigurations = new SigningConfigurations();
            services.AddSingleton(signingConfigurations);

            var tokenConfigurations = new TokenConfigurations();
            new ConfigureFromConfigurationOptions<TokenConfigurations>(
                Configuration.GetSection("TokenConfigurations"))
                    .Configure(tokenConfigurations);
            services.AddSingleton(tokenConfigurations);

            services.AddAuthentication(authOptions =>
            {
                authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(bearerOptions =>
            {
                bearerOptions.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = signingConfigurations.Key,
                    ValidAudience = tokenConfigurations.Audience,
                    ValidIssuer = tokenConfigurations.Issuer,

                    ValidateIssuerSigningKey = true,

                    ValidateLifetime = true,

                    ClockSkew = TimeSpan.Zero
                };

                bearerOptions.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        Console.WriteLine("Token inválido...:" + context.Exception.Message);
                        return Task.CompletedTask;
                    },
                    OnTokenValidated = context =>
                    {
                        Console.WriteLine("Token válido...:" + context.SecurityToken);
                        return Task.CompletedTask;
                    }
                };
            });

            services.AddAuthorization(auth =>
           {
               auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                   .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                   .RequireAuthenticatedUser().Build());
           });

            var container = new ContainerBuilder();
            container.Populate(services);
            return new AutofacServiceProvider(container.Build());
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "E-Client Microservice");
            });
            app.UseMvc();
            ConfigureEventBus(app);
        }
        private void ConfigureEventBus(IApplicationBuilder app)
        {
            IEventBus eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();

            //Product
            eventBus.Subscribe<ProductCreatedIntegrationEvent, ProductIntegrationEventHandler>();
            eventBus.Subscribe<ProductUpdatedIntegrationEvent, ProductIntegrationEventHandler>();
            eventBus.Subscribe<ProductRemovedIntegrationEvent, ProductIntegrationEventHandler>();

            //Brand
            eventBus.Subscribe<BrandCreatedIntegrationEvent, BrandIntegrationEventHandler>();
            eventBus.Subscribe<BrandUpdatedIntegrationEvent, BrandIntegrationEventHandler>();
            eventBus.Subscribe<BrandRemovedIntegrationEvent, BrandIntegrationEventHandler>();

            //ProductTpye
            eventBus.Subscribe<ProductTypeCreatedIntegrationEvent, ProductTypeIntegrationEventHandler>();
            eventBus.Subscribe<ProductTypeUpdatedIntegrationEvent, ProductTypeIntegrationEventHandler>();
            eventBus.Subscribe<ProductTypeRemovedIntegrationEvent, ProductTypeIntegrationEventHandler>();

            //Order
            eventBus.Subscribe<OrderCreatedIntegrationEvent, OrderIntegrationEventHandler>();
            eventBus.Subscribe<OrderUpdatedIntegrationEvent, OrderIntegrationEventHandler>();
            eventBus.Subscribe<OrderRemovedIntegrationEvent, OrderIntegrationEventHandler>();

            //OrderItem
            eventBus.Subscribe<OrderCreatedIntegrationEvent, OrderIntegrationEventHandler>();
            eventBus.Subscribe<OrderUpdatedIntegrationEvent, OrderIntegrationEventHandler>();
            eventBus.Subscribe<OrderRemovedIntegrationEvent, OrderIntegrationEventHandler>();

            eventBus.Consume("queue.client");
        }
    }
}
