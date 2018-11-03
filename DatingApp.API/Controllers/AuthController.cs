using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DatingApp.API.Data;
using DatingApp.API.Dtos;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DatingApp.API.Controllers
{
    [Route("api/[controller]")] // "api/Auth"
    [ApiController]
    public class AuthController : ControllerBase
    {
        //dependency injection Auth and IAuth repository 
        private readonly IAuthRepository _repo;
        private readonly IConfiguration _config;
        public AuthController(IAuthRepository repo, IConfiguration config)
        {
            _config = config;
            _repo = repo;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto)
        {
            //validate request 

            //we dont want all username to be different only lower or upper case 
            //so we wont allow John for one user and john for one user because it will be too same 
            //so when we check if this username already exists we have to lowercase it first 
            userForRegisterDto.Username = userForRegisterDto.Username.ToLower();

            //check if user exist in the repo (AuthRepository) in the data folder we created
            if (await _repo.UserExists(userForRegisterDto.Username)) //if true (if username exists)
                return BadRequest("Username already exist"); // return this

            //create new user instance                
            var userToCreate = new User
            {
                //store new username that got from registration and store in database 
                //in this new instance
                //ok! this new instance of User now has a new Username!
                Username = userForRegisterDto.Username
            };

            //Now lets pass in this newly created user into the AuthRepository 
            //to get the other parts such as password salt and hash 
            //after getting all the parts he is then saved into the database 
            //and then returned a fully whole and complete user and store it 
            //in this CreatedUser
            var createdUser = await _repo.Register(userToCreate, userForRegisterDto.Password);

            return StatusCode(201);



        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserForLoginDto userForLoginDto)
        {
            //send to check if user exist in Authrepository 
            var userFromRepo = await _repo.Login(userForLoginDto.Username.ToLower(), userForLoginDto.Password);
            //if user doesn't exist 
            if (userFromRepo == null)
                return Unauthorized();

            //add information into token 
            //once the server gets a token it can get the info inside this token 
            //so it doesn't need to go look at info in the database all the time 
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userFromRepo.Id.ToString()),
                new Claim(ClaimTypes.Name, userFromRepo.Username)
            };

            //a key to sign our token 
            //will be hashed and not going to be readable 
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));

            //create signing credentials 
            //recive the key created at the top and the algorithym to create hash 
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            //create secutity token descriptor 
            //contains claims, expiry date for our tokens, and signing credentials 
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1), //Expires in 1 day 
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            //create a token and pass token descriptor 
            //this token contains the JWT token that we want to return to our clients
            var token = tokenHandler.CreateToken(tokenDescriptor);
            
            return Ok(new {
                //write token into our reponse that we are sending back to our client 
                token = tokenHandler.WriteToken(token)
            });


        }


    }
}