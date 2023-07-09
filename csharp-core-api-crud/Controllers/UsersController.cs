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
        private readonly UserService _userService;
        public UsersController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost(Name = "PostUser"), AllowAnonymous]
        public async Task<UserLoginResponseObject> Post(UserDto users)
        {
            return await _userService.Post(users);
        }

        [HttpGet(Name = "GetUsers")]
        public async Task<List<UsersModel>> Get()
        {
            return await _userService.GetUsers();
        }

        [HttpPost("login"), AllowAnonymous]
        public async Task<ActionResult<UserLoginResponseObject>> Login(UserLoginDto user)
        {
            return await _userService.Login(user);
        }
    }
}
