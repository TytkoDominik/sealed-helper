using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using SealedHelperServer.DatabaseControllers;
using SealedHelperServer.Models;

namespace SealedHelperServer.Controllers
{
    [ApiController]
    [Route("events/players")]
    public class TournamentOrganizerController : Controller
    {
        private PlayerDatabaseController _databaseController;

        private PlayerDatabaseController DatabaseController
        {
            get { return _databaseController ??= new PlayerDatabaseController(); }
        }
        
        [HttpPost]
        public ActionResult GetPlayerData([FromBody] UserData userData)
        {
            var databaseResponse = DatabaseController.GetAllPlayers(userData);

            if (databaseResponse is AllPlayersDataReponse)
            {
                return Accepted(databaseResponse);
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