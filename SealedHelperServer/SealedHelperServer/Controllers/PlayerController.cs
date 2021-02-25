using System;
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
        private DateTime _rerollSuspensionDate = new DateTime(2021, 2, 27, 17, 59, 0);

        private PlayerDatabaseController DatabaseController
        {
            get { return _databaseController ??= new PlayerDatabaseController(); }
        }

        private bool RerollsSuspended()
        {
            return GetTime() > _rerollSuspensionDate;
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
                return Accepted(new PlayerDeckResponse((PlayerDataResponse) databaseResponse, RerollsSuspended()));
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
                return Accepted(new PlayerDeckResponse((PlayerDataResponse) databaseResponse, RerollsSuspended()));
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
                    Message = "Username and secret do not match!",
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

        [HttpGet("time")]
        public DateTime GetTime()
        {
            return DateTime.Now + new TimeSpan(0, 1, 0, 0);
        }

        [HttpPost("reroll")]
        public ActionResult RerollPlayerDeck([FromBody] UserData userData)
        {
            var databaseResponse = DatabaseController.TryRerollPlayerDeck(userData);

            if (databaseResponse is PlayerDataResponse)
            {
                return Accepted(new PlayerDeckResponse((PlayerDataResponse) databaseResponse, RerollsSuspended()));
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
                    Message = "Username and secret do not match!",
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