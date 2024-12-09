using System.ComponentModel.DataAnnotations;

namespace EnrollmentSystemUCB.Models.Entities
{
    public class EnrollmentHeader
    {
        public int EnrollmentId { get; set; }

        [Key]
        public int Id { get; set; }

        [Required]
        public int StudYear { get; set; }
        [Required]
        public DateTime DateEnrolled { get; set; }
        [Required]
        public string Encoder { get; set; }
        public double TotalUnits { get; set; }
    }
}
