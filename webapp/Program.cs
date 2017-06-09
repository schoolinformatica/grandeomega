using System.IO;
using Microsoft.AspNetCore.Hosting;
using webapp.wwwroot.scripts;

namespace WebApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            OnStartUp.Init();
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();
            
            host.Run();
        }
    }
}
