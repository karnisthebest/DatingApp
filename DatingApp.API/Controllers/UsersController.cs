using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using DatingApp.API.Data;
using DatingApp.API.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IDatingRepository _repo;
        private readonly IMapper _mapper;
        public UsersController(IDatingRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _repo.Getusers();

            var userToReturn = _mapper.Map<IEnumerable<UserForListDto>>(users);
            return Ok(userToReturn);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _repo.GetUser(id);

            // we don't want to return all the secret info to the users like the passwords #endregion
            // so we're gonna map it and return only the info that we want to send #endregion
            // UserForDetailedDto => destination to map to, its the info we want to send
            // user => map from, its the too much details that we dont want to send 
            var userToReturn = _mapper.Map<UserForDetailedDto>(user);

            return Ok(userToReturn);
        }

    }
}