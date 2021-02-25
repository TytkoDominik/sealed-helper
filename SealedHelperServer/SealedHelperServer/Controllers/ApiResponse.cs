using SealedHelperServer.DatabaseControllers;

namespace SealedHelperServer.Controllers
{
    public abstract class ApiResponse
    {
    }

    public class PlayerDeckResponse : ApiResponse
    {
        public string Name { get; }
        public string EventId { get; }
        public string Type { get; }
        public DeckData GeneratedDeck { get; }
        public string RerollDesc { get; }
        public bool RerollActive { get;  }

        public PlayerDeckResponse(PlayerDataResponse playerData, bool rerollSuspended)
        {
            Name = playerData.Player;
            EventId = "SAS70Sealed";
            Type = "sealed";
            
            GeneratedDeck = new DeckData
            {
                Name = playerData.Deck,
                DokLink = playerData.Link
            };

            RerollActive = rerollSuspended || playerData.RerollCount <= 0;
            RerollDesc = rerollSuspended ? "Rerolls disabled" : $"Reroll deck ({playerData.RerollCount})";
        }
    }

    public class ErrorResponse : ApiResponse
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public ErrorData ErrorData { get; set; }
    }

    public class ErrorData
    {
    }

    public class AlreadyRegisteredErrorData : ErrorData
    {
        public string Name { get; set; }
    }

    public class DeckData
    {
        public string Name { get; set; }
        public string DokLink { get; set; }
    }
}