using System;
using System.Collections.Generic;
using System.IO;
using SealedHelperServer.DBContexts;
using SealedHelperServer.Models;

namespace SealedHelperServer.DatabaseControllers
{
    public class DeckDataImporter
    {
        public void ImportDeckData()
        {
            var context = new DeckContext();
            var cards = context.Cards;
            var decks = context.Decks;
            var deckParser = new DeckParser();
            
            StreamReader file =
                new StreamReader("./dok_decks.csv");
            file.ReadLine();
            
            var line = String.Empty;
            var counter = 0;
            var cardList = new List<Card>();
            
            while((line = file.ReadLine()) != null)
            {
                var deck = deckParser.GetDeckDataFromRawString(line, cardList);
                
                if (deck.ContainsHouse(HouseType.Dis) && deck.Sas == 70)
                {
                    decks.Add(deck);
                    counter++;
                }
            }
            
            Console.WriteLine($"{counter}");
            file.Close();

            foreach (var card in cardList)
            {
                cards.Add(card);
            }

            context.SaveChanges();
        }
    }
}