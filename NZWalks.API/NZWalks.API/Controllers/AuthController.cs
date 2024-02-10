using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepo;
        private readonly ITokenHandler _tokenHandler;

        public AuthController(IUserRepository userRepository, ITokenHandler tokenHandler)
        {
            this._userRepo = userRepository;
            this._tokenHandler = tokenHandler;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginAsync(LoginRequest loginRequest)
        {
            //Check if user is authenticated
            //Check username and password
            var userFromDb = await _userRepo
                .AuthenticateAsync(loginRequest.Username, loginRequest.Password);

            if (userFromDb != null)
            {
                //Generate a JWT Token
                var token = await _tokenHandler.CreateTokenAsync(userFromDb);
                return Ok(token); 
            }

            return BadRequest("Username or Password is incorrect.");
        }
    }
}
