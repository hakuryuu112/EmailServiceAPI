namespace EmailServiceAPI.Models
{
    public class RoleModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
    }
}
