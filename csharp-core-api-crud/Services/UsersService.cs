using Dto;
using Microsoft.AspNetCore.Mvc;
using Models;
using Repository;

namespace Services
{
    public class UsersService
    {
        public async Task<string> UsersCreate(UsersModel users)
        {
            UsersRepository usersRepository = new();

            return await usersRepository.Create(users);
        }

        public async Task<List<UsersModel>> Get()
        {
            UsersRepository usersRepository = new();

            return await usersRepository.GetUsers();
        }

        public async Task<ActionResult<string>> GetOne(UserLoginDto user)
        {
            UsersRepository userRepository = new();

            return await userRepository.GetOneUser(user);
               
        }
    }
}
