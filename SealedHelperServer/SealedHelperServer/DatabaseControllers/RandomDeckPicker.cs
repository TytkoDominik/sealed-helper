using System;
using System.Collections.Generic;
using System.Linq;
using SealedHelperServer.DBContexts;
using SealedHelperServer.Models;

namespace SealedHelperServer.DatabaseControllers
{
    public class RandomDeckPicker
    {
        public Deck GetRandomDeck(List<string> alreadyUsedDecks)
        {
            var context = new DeckContext();
            var decks = context.Decks;
            var rawHouseDis = (int) HouseType.Dis;

            var queryResult = decks.Where(d => 
                (d.House_0 == rawHouseDis || d.House_1 == rawHouseDis || d.House_2 == rawHouseDis) 
                && d.Sas == 70).ToList();

            Deck deck;

            do
            {
                var rand = new Random();
                int index = rand.Next(queryResult.Count);
                deck = queryResult[index];
            } 
            while 
                (alreadyUsedDecks.Contains(deck.Name));
            

            return deck;
        }
    }
}