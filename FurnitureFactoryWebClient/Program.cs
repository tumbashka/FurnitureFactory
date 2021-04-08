using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FurnitureFactoryBusinessLogic.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace FurnitureFactoryWebClient
{
    public class Program
    {
        public static ClientViewModel Client = null;
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }
        public static bool AdminMode = false;
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
