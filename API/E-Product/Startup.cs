using System;
using System.Collections.Generic;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using Domain_Core.Bus;
using E_Product.Data;
using E_Product.Domain.Commands.Brand;
using E_Product.Domain.Commands.Product;
using E_Product.Domain.Commands.ProductType;
using E_Product.Domain.Commands.Provider;
using E_Product.Domain.EventHandlers;
using E_Product.Domain.Events.Brand;
using E_Product.Domain.Events.Product;
using E_Product.Domain.Events.ProductType;
using E_Product.Domain.Events.Provider;
using E_Product.Domain.IntegrationEventHandler;
using E_Product.Domain.IntegrationEvents;
using E_Product.Domain.Queries;
using E_Product.Domain.Queries.EFQueries;
using E_Product.Models;
using E_Product.Models.Dtos;
using E_Product.Repository;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;

namespace E_Product
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
            services.AddAutoMapper();
            ConfigureAutoMapper();

            // ############################## SQLITE #################################################
            services.AddDbContext<DataContext>(options =>
               options.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));
            services.AddDbContext<EventStoreContext>(options =>
               options.UseSqlite(Configuration.GetConnectionString("EventStoreConnection")));
            // #######################################################################################

            // ######################## USE THIS FOR LOCALHOST DEBUGGING. ############################
            // services.AddEntityFrameworkNpgsql().AddDbContext<DataContext>(opt =>
            //         opt.UseNpgsql(Configuration.GetConnectionString("PostgreSQL")));
            // #######################################################################################

            // ####################### USE THIS FOR DOCKER DEBUGGIING: ###############################
            // services.AddEntityFrameworkNpgsql()
            //         .AddDbContext<DataContext>(options =>
            //     options.UseNpgsql(Configuration["ConnectionStrings:DockerConnectionString"]));
            // services.AddEntityFrameworkNpgsql()
            //         .AddDbContext<EventStoreContext>(options =>
            //     options.UseNpgsql(Configuration["ConnectionStrings:DockerESConnectionString"]));
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

            services.AddMediatR();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "E-Product Microservice API",
                    Description = "E-Product API",
                    TermsOfService = "None",
                });

                var security = new Dictionary<string, IEnumerable<string>>
                {
                    {"Bearer", new string[] {}},
                };

                c.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    Description = "JWT Authorization header. Example: \"Bearer {token}\"",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                });

                c.AddSecurityRequirement(security);
            });


            services.AddScoped<IProviderRepository, ProviderRepository>();
            services.AddScoped<IBrandRepository, BrandRepository>();
            services.AddScoped<IProductTypeRepository, ProductTypeRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IEventStoreRepository, EventStoreRepository>();
            services.AddScoped<IMediatorHandler, InMemoryBus>();
            services.AddScoped<IBrandQueries, EFBrandQueries>();
            services.AddScoped<IProductQueries, EFProductQueries>();
            services.AddScoped<IProductTypeQueries, EFProductTypeQueries>();
            services.AddScoped<IProviderQueries, EFProviderQueries>();

            services.AddTransient(typeof(ProductIntegrationEventHandler));

            services.AddSingleton<IEventBus, EventBus>();
            services.AddAuthentication(o =>
                {
                    o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                }
            )
            /* .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters 
                {
                        ValidateAudience = false,
                        ValidateIssuer = false,
                        ValidateActor = false,
                        ValidateLifetime = true,
                };


                options.Audience = "/api/Auth/login"; 
             //   options.Authority = "http://authentication";
                options.RequireHttpsMetadata = false;
              //  options.Authority = "https://localhost:5003/";
            }) */.AddIdentityServerAuthentication(o =>
                 {
                    o.RequireHttpsMetadata = false;
                   //  o.Authority = "http://172.20.128.6:5003";
                    
                    o.Authority = "http://localhost:5003";
                 }
            );


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
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseCors("AllowAll");
            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.DocumentTitle = "Product Swagger";
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "E-Product");
            });

            ConfigureEventBus(app);
        }
        private void ConfigureEventBus(IApplicationBuilder app)
        {
            IEventBus eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
            eventBus.Subscribe<ProductOrderRequestedIntegrationEvent, ProductIntegrationEventHandler>();
            eventBus.Subscribe<ProductOrderCancelledIntegrationEvent, ProductIntegrationEventHandler>();

            eventBus.Consume("queue.product");
        }
        private void ConfigureAutoMapper()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Product, ProductToView>()
                  /*   .ForMember(q => q.Brand, options => options.MapFrom(q => q.Brand))
                    .ForMember(q => q.ProductType, options => options.MapFrom(q => q.ProductType))
                    .ForMember(q => q.Provider, options => options.MapFrom(q => q.Provider)); */;
                cfg.CreateMap<CreateProductCommand, Product>();
                cfg.CreateMap<Product, CreateProductCommand>();
                cfg.CreateMap<ProductDto, CreateProductCommand>();
                cfg.CreateMap<ProductDto, Product>();
                cfg.CreateMap<Product, ProductDto>();
                cfg.CreateMap<BrandDto, CreateBrandCommand>();
                cfg.CreateMap<ProviderDto, CreateProviderCommand>();
                cfg.CreateMap<ProductTypeDto, CreateProductTypeCommand>();
                cfg.CreateMap<Product, ProductCreatedEvent>();
                cfg.CreateMap<Product, ProductUpdatedEvent>();
                cfg.CreateMap<CreateProductTypeCommand, ProductType>();
                cfg.CreateMap<ProductType, CreateProductTypeCommand>();
                cfg.CreateMap<CreateProviderCommand, Provider>();
                cfg.CreateMap<Provider, CreateProviderCommand>();
                cfg.CreateMap<Brand, BrandCreatedEvent>();
                cfg.CreateMap<Brand, BrandUpdatedEvent>();
                cfg.CreateMap<ProductType, ProductTypeCreatedEvent>();
                cfg.CreateMap<ProductType, ProductTypeUpdatedEvent>();
                cfg.CreateMap<Provider, ProviderCreatedEvent>();
                cfg.CreateMap<Provider, ProviderUpdatedEvent>();
                cfg.CreateMap<CreateProviderCommand, Provider>();
                cfg.CreateMap<CreateBrandCommand, Brand>();
                cfg.CreateMap<Brand, CreateBrandCommand>();
                cfg.CreateMap<Brand, UpdateBrandCommand>();
                cfg.CreateMap<UpdateBrandCommand, Brand>();
                cfg.CreateMap<UpdateProductTypeCommand, ProductType>();
                cfg.CreateMap<UpdateProviderCommand, Provider>();
                cfg.CreateMap<UpdateProductCommand, Product>();
            });
        }

    }
}