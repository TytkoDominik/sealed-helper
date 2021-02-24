using SealedHelperServer.DatabaseControllers;

namespace SealedHelperServer.Controllers
{
    public abstract class ApiResponse
    {
    }

    public class PlayerDeckResponse : ApiResponse
    {
        public string Name { get; set; }
        public string EventId { get; set; }
        public string Type { get; set; }
        public DeckData GeneratedDeck { get; set; }

        public PlayerDeckResponse( PlayerDataResponse playerData)
        {
            Name = playerData.Player;
            EventId = "SAS70Sealed";
            Type = "sealed";
            
            GeneratedDeck = new DeckData
            {
                Name = playerData.Deck,
                DokLink = playerData.Link
            };
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