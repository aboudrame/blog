using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using blog.Data;
using Microsoft.AspNetCore.Identity;
using blog.Services;

namespace blog
{
    public class Program
    {
        public static void Main(string[] args)
        {
             CreateWebHostBuilder(args).Build().Run();
            //var host = CreateWebHostBuilder(args).Build();
            //using (var scope = host.Services.CreateScope())
            //{
            //    var serviceProvider = scope.ServiceProvider;
            //    var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            //    var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            //    try
            //    {
            //        Seeds.Initializer(context, userManager);
            //    }
            //    catch (Exception ex)
            //    {
            //        var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
            //        logger.LogError(ex, "An error occurred seeding the DB.");
            //    }
            //}

            //host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
