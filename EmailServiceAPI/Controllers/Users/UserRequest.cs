namespace EmailServiceAPI.Controllers.Users
{
    public class UserRequest
    {
        public Guid RoleId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Department { get; set; }

    }
}
