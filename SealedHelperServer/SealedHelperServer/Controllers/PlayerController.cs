using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
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
        
        public ActionResult RegisterNewPlayer(UserData userData)
        {
            var databaseResponse = DatabaseController.AddPlayer(userData);

            if (databaseResponse is PlayerAlreadyAddedResponse)
            {
                return Accepted(new ErrorResponse
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

            return Accepted(new ErrorResponse
            {
                Code = "unknown-error",
                Message = "Something bad happened here :(",
                ErrorData = new ErrorData()
            });
        }

        [HttpPost("player")]
        public ActionResult GetPlayerData([FromBody] UserData userData)
        {
            var databaseResponse = DatabaseController.GetPlayer(userData);

            if (databaseResponse is PlayerDataResponse)
            {
                return Accepted(new PlayerDeckResponse((PlayerDataResponse) databaseResponse));
            }

            if (databaseResponse is NoSuchPlayerResponse)
            {
                return RegisterNewPlayer(userData);
            }

            if (databaseResponse is WrongSecretResponse)
            {
                return Accepted(new ErrorResponse
                {
                    Code = "unauthorized",
                    Message = "Username and secret does not match, or the user doesn't exist",
                    ErrorData = new ErrorData()
                });
            }
            
            return Accepted(new ErrorResponse
            {
                Code = "unknown-error",
                Message = "Something bad happened here :(",
                ErrorData = new ErrorData()
            });
        }
    }
}