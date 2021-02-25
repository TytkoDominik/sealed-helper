using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Helpers;
using System.Web.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using SealedHelperServer.Models;

namespace SealedHelperServer.DatabaseControllers
{
    public class PlayerDatabaseController
    {
        private readonly MongoClient _client;
        private readonly IMongoDatabase _database;
        private readonly RandomDeckPicker _deckPicker;
        private readonly int _startingRerollCount = 2;

        public PlayerDatabaseController()
        {
            _client = new MongoClient("mongodb+srv://Vaultkeeper:keyforge321@vault.tbejo.mongodb.net/DoKDecks?retryWrites=true&w=majority");
            _database = _client.GetDatabase("SealedPlayers");
            _deckPicker = new RandomDeckPicker();
        }

        public DBMetadata GetMetadata()
        {
            var metadataCollection = _database.GetCollection<DBMetadata>("Metadata");
            var result = metadataCollection.AsQueryable().FirstOrDefault();
            
            if (result == null)
            {
                metadataCollection.InsertOne(new DBMetadata {DecksCount = 0, UsedDeckIndexes = new List<int>()});
                result = metadataCollection.AsQueryable().FirstOrDefault();
            }
            
            return result;
        }

        public UpdateResult SetMetadata(DBMetadata dbMetadata)
        {
            var metadataCollection = _database.GetCollection<DBMetadata>("Metadata");
            var filter = Builders<DBMetadata>.Filter.Exists(m => m.DecksCount);
            var update = Builders<DBMetadata>.Update.Set("DecksCount", dbMetadata.DecksCount)
                .Set("UsedDeckIndexes", dbMetadata.UsedDeckIndexes);

            return metadataCollection.UpdateMany(filter, update);
        }

        public DatabaseResponse GetAllPlayers(UserData toData)
        {
            var tournamentOrganizerName = toData.Name;
            var secret = toData.Secret;
            var toCollection = _database.GetCollection<UserData>("TournamentOrganizers");

            var to = toCollection.AsQueryable().
                FirstOrDefault(t => t.Name == tournamentOrganizerName);

            if (to == null || to.Secret != secret)
            {
                return new WrongSecretResponse();
            }
            
            var playersCollection = _database.GetCollection<Player>("Players");

            var players = playersCollection.AsQueryable().ToList();

            var playersData = players.Select(p => new PlayerDataResponse(p)).ToList();
            
            return new AllPlayersDataReponse { TournamentName = "Sealed Dis 70", Players = playersData };
        }

        public DatabaseResponse GetPlayer(UserData userData)
        {
            var playerName = userData.Name;
            var secret = userData.Secret;
            
            var playersCollection = _database.GetCollection<Player>("Players");

            var player = playersCollection.AsQueryable().FirstOrDefault(p => p.Name.ToLower() == playerName.ToLower());

            if (player == null)
            {
                return new NoSuchPlayerResponse();
            }
            
            if (player.Secret != secret)
            {
                return new WrongSecretResponse();
            }
            
            return new PlayerDataResponse(player);
        }

        public DatabaseResponse AddDecks(List<Deck> decks)
        {
            var decksCollection = _database.GetCollection<Deck>("Decks");
            
            decksCollection.InsertMany(decks);

            return new DecksAddedResponse();
        }

        public Deck GetDeck(int index)
        {
            var decksCollection = _database.GetCollection<Deck>("Decks");
            var filter = Builders<Deck>.Filter.Eq("Index", index);

            return decksCollection.FindSync(filter).First();
        }

        public DatabaseResponse AddPlayer(UserData playerData)
        {
            var playerName = playerData.Name;
            var secret = playerData.Secret;
            
            var playersCollection = _database.GetCollection<Player>("Players");

            if (playersCollection.AsQueryable().Any(p => p.Name == playerName))
            {
                return new PlayerAlreadyAddedResponse();
            }

            var deck = _deckPicker.GetRandomDeck();
            
            var newPlayer = new Player
            {
                Name = playerName,
                Secret = secret,
                Deck = deck.Name,
                DeckLink = deck.DoKLink,
                RerollCount = _startingRerollCount
            };
            
            playersCollection.InsertOneAsync(newPlayer);

            return new PlayerDataResponse(newPlayer);
        }

        public DatabaseResponse TryRerollPlayerDeck(UserData playerData)
        {
            var playerName = playerData.Name;
            var secret = playerData.Secret;
            
            var playersCollection = _database.GetCollection<Player>("Players");

            var player = playersCollection.AsQueryable().FirstOrDefault(p => p.Name.ToLower() == playerName.ToLower());

            if (player == null)
            {
                return new NoSuchPlayerResponse();
            }
            
            if (player.Secret != secret)
            {
                return new WrongSecretResponse();
            }

            if (player.RerollCount <= 0)
            {
                return new NotEnoughRerollsResponse();
            }

            var rerollCount = player.RerollCount - 1;
            var deck = _deckPicker.GetRandomDeck();

            var filter = Builders<Player>.Filter.Eq("Name", playerName);
            var update = Builders<Player>.Update.Set("Deck", deck.Name).Set("DeckLink", deck.DoKLink)
                .Set("RerollCount", rerollCount);

            playersCollection.UpdateMany(filter, update);
            
            var updatedPlayer =  playersCollection.AsQueryable().FirstOrDefault(p => p.Name.ToLower() == playerName.ToLower());

            return new PlayerDataResponse(updatedPlayer);
        }
    }
}