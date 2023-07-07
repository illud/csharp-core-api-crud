using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Microsoft.AspNetCore.Authorization;
using Models;
using Managers;

namespace Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ClientsController : ControllerBase
    {
        [HttpPost]
        public async Task<string> Post([FromBody] ClientsModel clients)
        {
            DatabaseConn conn = new();

            string query = $"INSERT INTO clients (name, age) VALUES('{clients.Name}', '{clients.Age}')";

            //open connection
            if (conn.OpenConnection() == true)
            {
                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new(query, conn.connection);

                //Execute command
                await cmd.ExecuteNonQueryAsync();

                //close connection
                conn.CloseConnection();

                return "Created";
            }

            return "Bad";
        }

        [HttpGet]
        public async Task<List<ClientsModel>> Get()
        {
            DatabaseConn conn = new();

            string query = "SELECT * FROM clients";

            //Create a list to store the result
            List<ClientsModel> list = new();

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
                    list.Add(new ClientsModel() { Id = (int)dataReader["id"], Name = (string)dataReader["name"], Age = (int)dataReader["age"] });
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

        [HttpPut("{id}")]
        public async Task<string> Put(int id, ClientsModel client)
        {
            DatabaseConn conn = new();

            string query = $"UPDATE clients SET name = '{client.Name}', age = '{client.Age}' WHERE id = '{id}' ";

            //open connection
            if (conn.OpenConnection() == true)
            {
                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new(query, conn.connection);

                //Execute command
                await cmd.ExecuteNonQueryAsync();

                //close connection
                conn.CloseConnection();

                return "Updated";
            }
            else
            {
                return "Error";
            }
        }

        [HttpDelete("{id}")]
        public async Task<string> Delete(int id)
        {
            DatabaseConn conn = new();

            string query = $"DELETE FROM clients WHERE id = '{id}' ";

            //open connection
            if (conn.OpenConnection() == true)
            {
                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new(query, conn.connection);

                //Execute command
                await cmd.ExecuteNonQueryAsync();

                //close connection
                conn.CloseConnection();

                return "Deleted";
            }
            else
            {
                return "Error";
            }
        }
    }
}
