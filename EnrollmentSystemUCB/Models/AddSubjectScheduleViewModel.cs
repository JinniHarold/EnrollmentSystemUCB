using EnrollmentSystemUCB.Models.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EnrollmentSystemUCB.Models
{
    public class AddSubjectScheduleViewModel
    {
        [Key]
        public int SubjectEDPCode { get; set; }
        [Required]
        public string SubjectCode { get; set; }

        [ForeignKey("SubjectCode")]
        public virtual Subject Subject { get; set; }

        public string SUbjectDescription { get; set; }
        public TimeSpan SubjectTimeStart { get; set; }
        public TimeSpan SubjectTimeEnd { get; set; }
        public string SubjectDays { get; set; } = string.Empty;
        public string SubjectSection { get; set; }
        public string SubjectSY { get; set; }
    }
}
