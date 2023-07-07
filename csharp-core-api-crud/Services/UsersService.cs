using Data;
using Dto;
using Microsoft.AspNetCore.Mvc;
using Models;
using Repository;
using Responses;

namespace Services
{
    public class UsersService
    {
        public async Task<UserLoginResponseObject> UsersCreate(UserDto users, DataContextDb _db)
        {
            UsersRepository usersRepository = new();

            return await usersRepository.Create(users, _db);
        }

        public async Task<List<UsersModel>> Get(DataContextDb _db)
        {
            UsersRepository usersRepository = new();

            return await usersRepository.GetUsers(_db);
        }

        public async Task<ActionResult<UserLoginResponseObject>> GetOne(UserLoginDto user, DataContextDb _db)
        {
            UsersRepository userRepository = new();

            return await userRepository.GetOneUser(user, _db);     
        }
    }
}
