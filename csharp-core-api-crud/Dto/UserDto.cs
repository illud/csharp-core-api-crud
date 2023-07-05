namespace Dto
{
    public class UserDto
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class UserLoginDto
    {
        public string userName { get; set; }
        public string password { get; set; }
    }
}
