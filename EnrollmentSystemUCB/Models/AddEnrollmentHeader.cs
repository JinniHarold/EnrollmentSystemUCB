using System.ComponentModel.DataAnnotations;

namespace EnrollmentSystemUCB.Models
{
    public class AddEnrollmentHeader
    {
        public int EnrollmentId { get; set; }

        [Key]
        public int Id { get; set; }

        [Required]
        public int StudYear { get; set; }
        [Required]
        public DateTime DateEnrolled { get; set; }

        [Required(ErrorMessage = "A name is required.")]
        public string Encoder { get; set; }
        public double TotalUnits { get; set; }
    }
}
