using System.ComponentModel.DataAnnotations;

namespace EmailServiceAPI.Models
{
    public class KPIModel
    {
        [Key]
        public Guid Id { get; set; }
        public Guid CompanyId { get; set; }
        public double Score { get; set; }
        public double AverageKpiScore { get; set; }
        public DateTime MeasuredAt { get; set; }

        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }
}
