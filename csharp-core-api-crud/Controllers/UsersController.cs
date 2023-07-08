using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Dto;
using Models;
using Responses;
using Repository;

namespace Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        public UsersController(IUserRepository userRepository) {
            _userRepository = userRepository;
        }

        [HttpPost(Name = "PostUser"), AllowAnonymous]
        public async Task<UserLoginResponseObject> Post(UserDto users)
        {
            return await _userRepository.Create(users);
        }

        [HttpGet(Name = "GetUsers")]
        public async Task<List<UsersModel>> Get()
        {
            return await _userRepository.GetUsers();
        }

        [HttpPost("login"), AllowAnonymous]
        public async Task<ActionResult<UserLoginResponseObject>> Login(UserLoginDto user)
        {
            return await _userRepository.GetOneUser(user);
        }
    }
}
