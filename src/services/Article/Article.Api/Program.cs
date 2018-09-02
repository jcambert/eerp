using Microsoft.AspNetCore;
using Microsoft.AspNetCore.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace Article.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseHealthChecks("/hc")
                .UseContentRoot(Directory.GetCurrentDirectory());
    }
}
