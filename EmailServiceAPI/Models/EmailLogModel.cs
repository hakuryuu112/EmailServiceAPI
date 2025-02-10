using System.ComponentModel.DataAnnotations;

namespace EmailServiceAPI.Models
{
    public class EmailLogModel
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Recipient { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }
}
