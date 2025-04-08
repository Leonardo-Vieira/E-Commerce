using Authentication.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Authentication.Models;
using Swashbuckle.AspNetCore.Swagger;
using Authentication.Data.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using IdentityServer4;
using IdentityServer4.Services;
using IdentityServer4.Configuration;
using System.Security.Cryptography.X509Certificates;
using Authentication.Configuration;
using System;
using Authentication.Certificates;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Domain_Core.Bus;
using MediatR;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Authentication.Repository;

namespace Authentication
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
            services.AddDbContext<DataContext>(options =>
                options.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));
            services.AddDbContext<EventStoreContext>(options =>
                options.UseSqlite(Configuration.GetConnectionString("EventStoreConnection")));

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<DataContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 8;
                options.Password.RequireLowercase = false;

                options.User.RequireUniqueEmail = true;
            });

            services.AddScoped<IUserService<User>, UserService>();
            services.AddScoped<IMediatorHandler, InMemoryBus>();
             services.AddScoped<IEventStoreRepository, EventStoreRepository>();
            services.AddTransient<IdentityServerTools>();
            services.AddTransient<IdentityServerOptions>();
            services.AddTransient<ITokenCreationService, DefaultTokenCreationService>();
            services.AddTransient<IKeyMaterialService, DefaultKeyMaterialService>();
            services.AddSingleton<IEventBus, EventBus>();
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", p =>
                {
                    p.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });

            services.AddMvc(o =>
            {
                o.SslPort = 5003;
             //   o.Filters.Add(new RequireHttpsAttribute());
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
             services.AddMediatR();

            services.AddAntiforgery(o =>
                    {
                        o.Cookie.Name = "_af";
                        o.Cookie.HttpOnly = true;
                        o.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                        o.HeaderName = "X-XSRF-TOKEN";
                    });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();

            services.AddSwaggerGen(c =>
                        {
                            c.SwaggerDoc("v50", new Info
                            {
                                Version = "v50",
                                Title = "Authentication",
                            });
                        });

            services.AddIdentityServer(x =>
            {
                //x.IssuerUri = "http://172.20.128.6:5003";
                x.IssuerUri = "http://localhost:5003";
            })
                .AddSigningCredential(new X509Certificate2("./Certificates/certificate.pfx", "password1"))
                .AddInMemoryIdentityResources(Config.GetResources())
                .AddInMemoryApiResources(Config.GetApis())
                .AddInMemoryClients(Config.GetClients())
                .AddAspNetIdentity<User>();


            var container = new ContainerBuilder();
            container.Populate(services);
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
           // app.UseRewriter(new RewriteOptions().AddRedirectToHttps());
            app.UseIdentityServer();
           // app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v50/swagger.json", "Authentication");
            });
            
            app.UseCors("AllowAll");
            app.UseMvc();

            ConfigureEventBus(app);
        }

        private void ConfigureEventBus(IApplicationBuilder app)
        {
            IEventBus eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
        }
    }
}
