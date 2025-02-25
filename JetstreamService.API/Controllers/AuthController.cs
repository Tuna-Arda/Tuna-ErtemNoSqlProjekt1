using Microsoft.AspNetCore.Mvc;
using JetstreamService.API.Models;
using JetstreamService.API.Services;
using System.Collections.Generic;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace JetstreamService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        // POST: api/auth/login
        [HttpPost("login")]
        public ActionResult<User> Login([FromBody] User login)
        {
            var user = _userService.GetUser(login.Benutzername, login.Passwort);
            if (user == null)
            {
                return Unauthorized();
            }
            // In einer produktiven Umgebung sollte hier ein JWT generiert werden.
            return Ok(user);
        }
    }
}
