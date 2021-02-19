using System.Net;
using Microsoft.AspNetCore.Mvc;
using SealedHelperServer.DatabaseControllers;
using SealedHelperServer.Models;

namespace SealedHelperServer.Controllers
{
    [ApiController]
    [Route("events/sealed")]
    public class PlayerController : Controller
    {
        private PlayerDatabaseController _databaseController;

        private PlayerDatabaseController DatabaseController
        {
            get { return _databaseController ??= new PlayerDatabaseController(); }
        }
        
        [HttpPost("register")]
        public ActionResult RegisterNewPlayer([FromBody] UserData userData)
        {
            var databaseResponse = DatabaseController.AddPlayer(userData);

            if (databaseResponse is PlayerAlreadyAddedResponse)
            {
                return Conflict(new ErrorResponse
                {
                    Code = "already-registered",
                    Message = "This username is already registered for this event.",
                    ErrorData = new AlreadyRegisteredErrorData {Name = userData.Name}
                });
            }
            
            if (databaseResponse is PlayerDataResponse)
            {
                return Accepted(new PlayerDeckResponse((PlayerDataResponse) databaseResponse));
            }

            return BadRequest(new ErrorResponse
            {
                Code = "unknown-error",
                Message = "Something bad happened here :(",
                ErrorData = new ErrorData()
            });
        }

        [HttpGet("player")]
        public ActionResult GetPlayerData([FromBody] UserData userData)
        {
            var databaseResponse = DatabaseController.GetPlayer(userData);

            if (databaseResponse is PlayerDataResponse)
            {
                return Accepted(new PlayerDeckResponse((PlayerDataResponse) databaseResponse));
            }

            if (databaseResponse is WrongSecretResponse)
            {
                return Unauthorized(new ErrorResponse
                {
                    Code = "unauthorized",
                    Message = "Username and secret does not match, or the user doesn't exist",
                    ErrorData = new ErrorData()
                });
            }
            
            return BadRequest(new ErrorResponse
            {
                Code = "unknown-error",
                Message = "Something bad happened here :(",
                ErrorData = new ErrorData()
            });
        }
    }
}