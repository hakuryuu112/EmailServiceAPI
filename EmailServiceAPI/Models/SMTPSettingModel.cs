using System.ComponentModel.DataAnnotations;

namespace EmailServiceAPI.Models
{
    public class SMTPSettingModel
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Server { get; set; }
        public int Port { get; set; }
        public string SenderEmail { get; set; }
        public string SenderPassword { get; set; }
        public bool EnableSSL { get; set; }
    }
}
