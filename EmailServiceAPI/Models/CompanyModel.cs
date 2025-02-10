using System.ComponentModel.DataAnnotations;

namespace EmailServiceAPI.Models
{
    public class CompanyModel
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Industry { get; set; }

        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }
}
