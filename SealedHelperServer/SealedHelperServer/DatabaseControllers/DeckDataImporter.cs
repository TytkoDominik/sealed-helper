using System;
using System.Collections.Generic;
using System.IO;
using SealedHelperServer.Models;

namespace SealedHelperServer.DatabaseControllers
{
    public class DeckDataImporter
    {
        public void ImportDeckData(string expansion)
        {
            var playerDatabaseController = new PlayerDatabaseController();
            var deckParser = new DeckParser();
            var decks = new List<Deck>();
            var metadata = playerDatabaseController.GetMetadata();
            
            StreamReader file =
                new StreamReader($"./dok_decks_{expansion}.csv");
            file.ReadLine();
            
            var line = String.Empty;
            var counter = metadata.DecksCount;
            
            while((line = file.ReadLine()) != null)
            {
                var deck = deckParser.GetDeckDataFromRawString(line);
                deck.Index = counter;
                decks.Add(deck);
                counter++;
            }

            metadata.DecksCount = counter;
            var metadataUpdateResponse = playerDatabaseController.SetMetadata(metadata);
            var response = playerDatabaseController.AddDecks(decks);
            
            Console.WriteLine($"{metadataUpdateResponse}: {response.GetType()}");
            
            file.Close();
        }
    }
}