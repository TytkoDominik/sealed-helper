using System;
using System.Linq;
using System.Web.Helpers;
using System.Web.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using SealedHelperServer.Models;

namespace SealedHelperServer.DatabaseControllers
{
    public class PlayerDatabaseController
    {
        private readonly MongoClient _client;
        private readonly IMongoDatabase _database;
        private readonly RandomDeckPicker _deckPicker;

        public PlayerDatabaseController()
        {
            _client = new MongoClient("mongodb+srv://Vaultkeeper:keyforge321@vault.tbejo.mongodb.net/DoKDecks?retryWrites=true&w=majority");
            _database = _client.GetDatabase("SealedPlayers");
            _deckPicker = new RandomDeckPicker();
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

            var player = playersCollection.AsQueryable().FirstOrDefault(p => p.Name == playerName);

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

        public DatabaseResponse AddPlayer(UserData playerData)
        {
            var playerName = playerData.Name;
            var secret = playerData.Secret;
            
            var playersCollection = _database.GetCollection<Player>("Players");

            if (playersCollection.AsQueryable().Any(p => p.Name == playerName))
            {
                return new PlayerAlreadyAddedResponse();
            }

            var alreadyUsedDecks = playersCollection.AsQueryable().Select(p => p.Deck).ToList();

            var deck = _deckPicker.GetRandomDeck(alreadyUsedDecks);
            
            var newPlayer = new Player
            {
                Name = playerName,
                Secret = secret,
                Deck = deck.Name,
                DeckLink = deck.DoKLink
            };
            
            playersCollection.InsertOneAsync(newPlayer);

            return new PlayerDataResponse(newPlayer);
        }
    }
}