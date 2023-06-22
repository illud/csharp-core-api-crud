using Microsoft.AspNetCore.Mvc;
using csharp_core_api_crud.Models;

namespace csharp_core_api_crud.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
         static List<UsersModel> users = new();

        [HttpPost(Name = "PostUser")]
        public List<UsersModel> Post()
        {
            users.Add(new UsersModel() { Name = "test", Age = 19 });
            return users;
        }

        [HttpGet(Name = "GetUsers")]
        public List<UsersModel> Get()
        {   
            return users;
        }
    }
}
