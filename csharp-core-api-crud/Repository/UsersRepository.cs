using Models;
using MySql.Data.MySqlClient;
using Services;
using Dto;
using Microsoft.AspNetCore.Mvc;
using Responses;

namespace Repository
{
    public class UsersRepository
    {
        public async Task<UserLoginResponseObject> Create(UserDto users)
        {
            DatabaseConn conn = new();

            BcryptService bcryptService = new();

            string query = $"INSERT INTO users (name, age, username, password) VALUES('{users.Name}', '{users.Age}', '{users.UserName}', '{bcryptService.HashPassword(users.Password)}')";

            if (conn.OpenConnection() == true)
            {
                MySqlCommand cmd = new(query, conn.connection);

                await cmd.ExecuteNonQueryAsync();

                conn.CloseConnection();

                return new UserLoginResponseObject { response = "Created" };
            }
            return new UserLoginResponseObject { response = "Error" };
        }

        public async Task<List<UsersModel>> GetUsers()
        {
            DatabaseConn conn = new();

            string query = "SELECT * FROM users";

            //Create a list to store the result
            List<UsersModel> list = new();

            //Open connection
            if (conn.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new(query, conn.connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = (MySqlDataReader)await cmd.ExecuteReaderAsync();

                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    list.Add(new UsersModel() { Id = (int)dataReader["id"], Name = (string)dataReader["name"], Age = (int)dataReader["age"], UserName = (string)dataReader["username"], Password = (string)dataReader["password"] });
                }

                //close Data Reader
                dataReader.Close();

                //close Connection
                conn.CloseConnection();

                //return list to be displayed
                return list;
            }
            else
            {
                return list;
            }
        }

        public async Task<ActionResult<UserLoginResponseObject>> GetOneUser(UserLoginDto user)
        {
            DatabaseConn conn = new();

            string query = $"SELECT * FROM users WHERE username = '{user.userName}'";

            //Create a list to store the result
            List<UsersModel> userList = new();

            //Open connection
            if (conn.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new(query, conn.connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = (MySqlDataReader)await cmd.ExecuteReaderAsync();

                //Read the data and store them in the userList
                while (dataReader.Read())
                {
                    userList.Add(new UsersModel() { Id = (int)dataReader["id"], Name = (string)dataReader["name"], Age = (int)dataReader["age"], UserName = (string)dataReader["username"], Password = (string)dataReader["password"] });
                }

                //close Data Reader
                dataReader.Close();

                //close Connection
                conn.CloseConnection();

                //return token if password math
                BcryptService veryfyHash = new();

                if (veryfyHash.VerifyPassword(user.password, userList[0].Password))
                {
                    JwtService token = new();

                    return new UserLoginResponseObject { response = token.GenerateJwt("user") };
                }
                return new UserLoginResponseObject { response = "Error" };
            }
            else
            {
                return new UserLoginResponseObject { response = "Error" };
            }
        }
    }
}
