using System;
using SealedHelperServer.Models;

namespace SealedHelperServer.DatabaseControllers
{
    public class RandomDeckPicker
    {
        public Deck GetRandomDeck()
        {
            var playerDatabaseController = new PlayerDatabaseController();
            var metadata = playerDatabaseController.GetMetadata();
            var random = new Random();
            
            var rand = random.Next(metadata.DecksCount);

            while (metadata.UsedDeckIndexes.Contains(rand))
            {
                rand = random.Next(metadata.DecksCount);
            }

            metadata.UsedDeckIndexes.Add(rand);
            playerDatabaseController.SetMetadata(metadata);
            
            return playerDatabaseController.GetDeck(rand);
        }
    }
}