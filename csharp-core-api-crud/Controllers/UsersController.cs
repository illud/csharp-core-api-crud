using Microsoft.AspNetCore.Mvc;
using csharp_core_api_crud.Models;
using Services;
using MySql.Data.MySqlClient;

namespace csharp_core_api_crud.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {

        [HttpPost(Name = "PostUser")]
        public async Task<string> Post(UsersModel users)
        {
            DatabaseConn conn = new();

            string query = $"INSERT INTO users (name, age, username, password) VALUES('{users.Name}', '{users.Age}', '{users.UserName}', '{users.Password}')";
            
            if(conn.OpenConnection() == true)
            {
                MySqlCommand cmd = new(query, conn.connection);

                await cmd.ExecuteNonQueryAsync();

                conn.CloseConnection();

                return "Created";
            }
            return "Error";
        }

        [HttpGet(Name = "GetUsers")]
        public async Task<List<UsersModel>> Get()
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

        [HttpPost("login")]
        public string Login()
        {
            JwtService token = new();
            return token.GenerateJwt("user");
        }
    }
}
