using Microsoft.AspNetCore.Mvc;

namespace Models
{
    public class UsersModel
    {
        public int Id { get; set; } 
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }

        public string UserName { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

    }
}
