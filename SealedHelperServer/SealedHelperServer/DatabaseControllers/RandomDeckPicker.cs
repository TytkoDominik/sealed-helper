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
            var queryResult = decks.ToList();

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