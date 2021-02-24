using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using SealedHelperServer.DatabaseControllers;

namespace SealedHelperServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            new DeckDataImporter().ImportDeckData();
           // CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}