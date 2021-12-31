using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            //we need to go and get access to our Data Context, because we are outside of our Services Container
            //and the Start up class where we don't have control over lifetime of when we create this particular instance
            //of the context we are going to do this inside a "using" statement
            //"using" statement means any code that runs inside of this is going to be disposed of as soon as we finish the methods inside it
            //we don't need to worry about cleaning up and disposing
            using (var scope= host.Services.CreateScope()) 
            {
                    var services= scope.ServiceProvider;
                    //loggerfactory helps to log messages to console
                    var loggerFactory = services.GetRequiredService<ILoggerFactory>(); 
                    try
                    {
                        var context = services.GetRequiredService<StoreContext>();
                        await context.Database.MigrateAsync();
                        //this above code will create database if database doesn't exist
                        await StoreContextSeed.SeedAsync(context,loggerFactory);
                    } catch(Exception ex)
                    {
                        var logger = loggerFactory.CreateLogger<Program>();
                        logger.LogError(ex,"An Error occured during migration");
                    }
            }
            host.Run();

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
