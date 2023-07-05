using Dto;
using Microsoft.AspNetCore.Mvc;
using Models;
using Repository;
using Responses;

namespace Services
{
    public class UsersService
    {
        public async Task<UserLoginResponseObject> UsersCreate(UserDto users)
        {
            UsersRepository usersRepository = new();

            return await usersRepository.Create(users);
        }

        public async Task<List<UsersModel>> Get()
        {
            UsersRepository usersRepository = new();

            return await usersRepository.GetUsers();
        }

        public async Task<ActionResult<UserLoginResponseObject>> GetOne(UserLoginDto user)
        {
            UsersRepository userRepository = new();

            return await userRepository.GetOneUser(user);
               
        }
    }
}
