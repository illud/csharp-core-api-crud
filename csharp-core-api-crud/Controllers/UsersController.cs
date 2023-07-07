using Microsoft.AspNetCore.Mvc;
using Services;
using Microsoft.AspNetCore.Authorization;
using Dto;
using Models;
using Responses;
using Data;

namespace Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly DataContextDb _db;
        public UsersController(DataContextDb contextDb)
        {
            _db = contextDb;
        }

        [HttpPost(Name = "PostUser"), AllowAnonymous]
        public async Task<UserLoginResponseObject> Post(UserDto users)
        {
            UsersService usersService = new();
            return await usersService.UsersCreate(users, _db);
        }

        [HttpGet(Name = "GetUsers")]
        public async Task<List<UsersModel>> Get()
        {
            UsersService usersService = new();
            return await usersService.Get(_db);
        }

        [HttpPost("login"), AllowAnonymous]
        public async Task<ActionResult<UserLoginResponseObject>> Login(UserLoginDto user)
        {
            UsersService usersService = new();
            return await usersService.GetOne(user, _db);
        }
    }
}
