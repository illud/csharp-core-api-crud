using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Dto;
using Models;
using Responses;
using Bll;

namespace Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly UsersBll _userBll;
        public UsersController(UsersBll userBll)
        {
            _userBll = userBll;
        }

        [HttpPost(Name = "PostUser"), AllowAnonymous]
        public async Task<UserLoginResponseObject> Post(UserDto users)
        {
            return await _userBll.Post(users);
        }

        [HttpGet(Name = "GetUsers")]
        public async Task<List<UsersModel>> Get()
        {
            return await _userBll.GetUsers();
        }

        [HttpPost("login"), AllowAnonymous]
        public async Task<ActionResult<UserLoginResponseObject>> Login(UserLoginDto user)
        {
            return await _userBll.Login(user);
        }
    }
}
