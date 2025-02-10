using System.ComponentModel.DataAnnotations;

namespace EmailServiceAPI.Models
{
    public class UserModel
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid RoleId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Department { get; set; }
        
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }
}
