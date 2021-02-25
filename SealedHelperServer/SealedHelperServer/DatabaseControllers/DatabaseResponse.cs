using System.Collections.Generic;
using SealedHelperServer.Models;

namespace SealedHelperServer.DatabaseControllers
{
    public abstract class DatabaseResponse
    {
    }

    public class PlayerDataResponse : DatabaseResponse
    {
        public string Player { get; }
        public string Deck { get; }
        public string Link { get; }
        public int RerollCount { get; }

        public PlayerDataResponse(Player player)
        {
            Player = player.Name;
            Deck = player.Deck;
            Link = player.DeckLink;
            RerollCount = player.RerollCount;
        }
    }

    public class AllPlayersDataReponse : DatabaseResponse
    {
        public string TournamentName { get; set; }
        public List<PlayerDataResponse> Players { get; set; }
    }

    public class NoSuchPlayerResponse : DatabaseResponse
    {
    }

    public class PlayerAlreadyAddedResponse : DatabaseResponse
    {
    }

    public class WrongSecretResponse : DatabaseResponse
    {
    }

    public class DecksAddedResponse : DatabaseResponse
    {
    }

    public class NotEnoughRerollsResponse : DatabaseResponse
    {
    }
}