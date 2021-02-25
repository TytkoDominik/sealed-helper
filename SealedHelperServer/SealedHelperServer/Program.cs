using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using SealedHelperServer.DatabaseControllers;

namespace SealedHelperServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            /*var deckImporter = new DeckDataImporter();
            deckImporter.ImportDeckData("cota");
            deckImporter.ImportDeckData("aoa");
            deckImporter.ImportDeckData("wc");
            deckImporter.ImportDeckData("mm");*/
            
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}