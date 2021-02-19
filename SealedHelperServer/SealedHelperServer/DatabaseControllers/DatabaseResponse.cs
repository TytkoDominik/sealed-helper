using System.Collections.Generic;
using SealedHelperServer.Models;

namespace SealedHelperServer.DatabaseControllers
{
    public abstract class DatabaseResponse
    {
    }

    public class PlayerDataResponse : DatabaseResponse
    {
        public string Player { get; set; }
        public string Deck { get; set; }
        public string Link { get; set; }

        public PlayerDataResponse(Player player)
        {
            Player = player.Name;
            Deck = player.Deck;
            Link = player.DeckLink;
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
}