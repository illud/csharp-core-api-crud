using Dto;
using Managers;
using Microsoft.AspNetCore.Mvc;
using Models;
using Responses;
using Repository;

namespace Bll
{
    public class UsersBll
    {
        private readonly IUserRepository _userRepository;
        public UsersBll(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserLoginResponseObject> Post(UserDto users)
        {
            return await _userRepository.Create(users);
        }

        public async Task<List<UsersModel>> GetUsers()
        {
            return await _userRepository.GetUsers();
        }

        public async Task<ActionResult<UserLoginResponseObject>> Login(UserLoginDto user)
        {
            UsersModel userList = await _userRepository.GetOneUser(user);
        
            //return token if password math
            BcryptService veryfyHash = new();

            if (veryfyHash.VerifyPassword(user.password, userList.Password))
            {
                JwtService token = new();

                return new UserLoginResponseObject { response = token.GenerateJwt("user") };
            }
            return new UserLoginResponseObject { response = "Error" };
        }
    }
}
