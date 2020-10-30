using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Mvc.Models;
using Mvc.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Mvc.Data;
using Microsoft.AspNetCore.Identity;
using IdentityServer4.Services;
using IdentityServer4.EntityFramework;
using System.Reflection;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;

namespace Mvc
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public string? migrationAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

        public IConfiguration Configuration { get; }

        private void InitializeDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();

                var context = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();

                context.Database.Migrate();

                if (!context.Clients.Any())
                {
                    foreach (var client in Config.GetClients())
                    {
                        context.Clients.Add(client.ToEntity());
                    }
                    context.SaveChanges();
                }

                if (!context.ApiScopes.Any())
                {
                    foreach (var scope in Config.GetApiScopes())
                    {
                        context.ApiScopes.Add(scope.ToEntity());
                    }
                    context.SaveChanges();
                }

                if (!context.IdentityResources.Any())
                {
                    foreach (var identity in Config.GetIdentityResources())
                    {
                        context.IdentityResources.Add(identity.ToEntity());
                    }
                    context.SaveChanges();
                }

                if (!context.ApiResources.Any())
                {
                    foreach (var api in Config.GetApiResources())
                    {
                        context.ApiResources.Add(api.ToEntity());
                    }
                    context.SaveChanges();
                }

                
            }
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var appSetting = new AppSettings();
            Configuration.Bind(appSetting);

            services.Configure<AppSettings>(Configuration);

            services.AddScoped<ConsentService>();

            services.AddDbContext<Auth2Context>(options =>
            {
                options.UseMySql(appSetting.ConnectionStrings.Auth2Context);
            });

            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<Auth2Context>()
                .AddDefaultTokenProviders();

            services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                //.AddInMemoryIdentityResources(Config.GetIdentityResources())
                //.AddInMemoryApiScopes(Config.GetApiScopes())
                //.AddInMemoryApiResources(Config.GetApiResources())
                //.AddInMemoryClients(Config.GetClients())
                .AddConfigurationStore(options =>
                {
                    options.ConfigureDbContext = builder =>
                    {
                        builder.UseMySql(appSetting.ConnectionStrings.IdentityServer4Context, sql => sql.MigrationsAssembly(migrationAssembly));
                    };
                })
                .AddOperationalStore(options =>
                {
                    options.ConfigureDbContext = builder =>
                    {
                        builder.UseMySql(appSetting.ConnectionStrings.IdentityServer4Context, sql => sql.MigrationsAssembly(migrationAssembly));
                    };
                })
                .AddAspNetIdentity<ApplicationUser>()
                .Services.AddScoped<IProfileService, ProFileService>()
                ;
            //.AddTestUsers(Config.GetTestUsers())
            ;

            services.AddControllersWithViews();

            //修改cookie策略
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.MinimumSameSitePolicy = Microsoft.AspNetCore.Http.SameSiteMode.Lax;
            });





        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IOptions<AppSettings> options)
        {
            var db = options.Value.ConnectionStrings.Auth2Context;
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            InitializeDatabase(app);
            //使用cookie 策略
            app.UseCookiePolicy();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseIdentityServer();


            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
