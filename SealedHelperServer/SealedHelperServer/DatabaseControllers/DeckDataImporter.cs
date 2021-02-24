using System;
using System.IO;
using SealedHelperServer.DBContexts;

namespace SealedHelperServer.DatabaseControllers
{
    public class DeckDataImporter
    {
        public void ImportDeckData()
        {
            var context = new DeckContext();
            var decks = context.Decks;
            var deckParser = new DeckParser();
            
            StreamReader file =
                new StreamReader("./dok_decks.csv");
            file.ReadLine();
            
            var line = String.Empty;
            var counter = 0;
            
            while((line = file.ReadLine()) != null)
            {
                var deck = deckParser.GetDeckDataFromRawString(line);

                decks.Add(deck);
                counter++;
            }
            
            Console.WriteLine($"{counter}");
            file.Close();

            context.SaveChanges();
        }
    }
}