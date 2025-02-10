using System.ComponentModel.DataAnnotations;

namespace EmailServiceAPI.Controllers.Email
{
    public class EmailSendRequest
    {
        [Required]
        public string Recipient { get; set; }
        [Required]
        public string Subject { get; set; }
        [Required]
        public string Body { get; set; }
    }
}
