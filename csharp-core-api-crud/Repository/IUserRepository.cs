using Data;
using Dto;
using Microsoft.AspNetCore.Mvc;
using Models;
using Responses;

namespace Repository
{
    public interface IUserRepository
    {
        Task<UserLoginResponseObject> Create(UserDto users);
        Task<List<UsersModel>> GetUsers();
        Task<UsersModel> GetOneUser(UserLoginDto user);
    }
}
