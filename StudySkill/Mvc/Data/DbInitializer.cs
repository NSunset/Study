using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mvc.Data
{
    public static class DbInitializer
    {

        public static async Task<IHost> CreateDbIfNotExistsAsync(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<Auth2Context>();
                    await InitializeAsync(context, services);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred creating the DB.");
                }
            }
            return host;
        }

        public static async Task InitializeAsync(Auth2Context context, IServiceProvider serviceProvider)
        {
            context.Database.EnsureCreated();

            var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            if (!context.ApplicationRole.Any())
            {
                var role = new ApplicationRole
                {
                    Name = "管理员",
                    NormalizedName = "admin"
                };
                var result = await roleManager.CreateAsync(role);
                if (!result.Succeeded)
                {
                    throw new Exception("初始化角色信息失败:" + result.Errors.SelectMany(e => e.Description));
                }
            }

            if (!context.ApplicationUser.Any())
            {
                var user = new ApplicationUser
                {
                    UserName = "admin",
                    Email = "admin@163.com",
                    NormalizedUserName = "admin",
                    Avatar = "https://cdn2.codingdojo.com/new_design_image/global/icon/stacks/big/net_core_stack.png",
                };

                var result = await userManager.CreateAsync(user, "123456$Admin");

                await userManager.AddToRoleAsync(user, "管理员");

                if (!result.Succeeded)
                {
                    throw new Exception("初始默认用户失败:" + result.Errors.SelectMany(e => e.Description));
                }

            }

        }
    }
}
