using Models;
using Dto;
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

        public async Task<UsersModel> GetOneUser(UserLoginDto user)
        {
            List<UsersModel> userList = await _db.Users.Where(u => u.UserName == user.userName).ToListAsync();
            return userList[0];
        }
    }
}
