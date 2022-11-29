using System;
using Api.Data;
using Api.Models;
using Api.Models.Dto;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Api.Logging;
using Microsoft.EntityFrameworkCore;


namespace Api.Controllers
{
    [Route("Api/Auth")]
    [ApiController]
    public class ApiAuthController : ControllerBase
    {
        private readonly ILogging _logger;
        private DbConnect _dbConnect;

        public ApiAuthController(ILogging logger, DbConnect dbConnect)
        {
            _logger = logger;
            _dbConnect = dbConnect;
        }

        

        [HttpGet("{authLogin}, {authPassword}", Name = "GetAuthByLoginPassword")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<int> GetAuthByLoginPassword(string authLogin, string authPassword)
        {
            AuthDto authDto = new AuthDto() { Login = authLogin, Pass = authPassword };
            var login = _dbConnect.Authorisation.AsNoTracking().FirstOrDefault(a => a.Login == authDto.Login);
            if (login == null)
            {
                return BadRequest("Login not found");
            }
            else
            {
                if(login.Pass == authDto.Pass)
                {
                    var person = _dbConnect.People.Include(a => a.Auth).AsNoTracking().FirstOrDefault(p => p.Auth.Id == login.Id);
                    _logger.Log("Success", LogType.Message);
                    return Ok(person.Id);
                }
                return BadRequest("Password is incorrect");
            }
        }
    }
}

