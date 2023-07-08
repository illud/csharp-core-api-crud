using Models;
using Dto;
using Microsoft.AspNetCore.Mvc;
using Responses;
using Data;
using Microsoft.EntityFrameworkCore;
using Managers;

namespace Repository
{
    public class UsersRepository : IUserRepository
    {
        private readonly DataContextDb _db;
        public UsersRepository(DataContextDb contextDb)
        {
            _db = contextDb;
        }

        public async Task<UserLoginResponseObject> Create(UserDto users)
        {
            BcryptService bcryptService = new();

            _db.Users.Add(new UsersModel {
                Id = users.Id,
                Name = users.Name,
                Age = users.Age,
                UserName = users.UserName,
                Password = bcryptService.HashPassword(users.Password)
            });

            await _db.SaveChangesAsync();
            return new UserLoginResponseObject { response = "Created" };
        }

        public async Task<List<UsersModel>> GetUsers()
        {
            return await _db.Users.ToListAsync();
        }

        public async Task<ActionResult<UserLoginResponseObject>> GetOneUser(UserLoginDto user)
        {
            List<UsersModel> userList = await _db.Users.Where(u => u.UserName == user.userName).ToListAsync();

            //return token if password math
            BcryptService veryfyHash = new();

            if (veryfyHash.VerifyPassword(user.password, userList[0].Password))
            {
                JwtService token = new();

                return new UserLoginResponseObject { response = token.GenerateJwt("user") };
            }
            return new UserLoginResponseObject { response = "Error" };
        }
    }
}
