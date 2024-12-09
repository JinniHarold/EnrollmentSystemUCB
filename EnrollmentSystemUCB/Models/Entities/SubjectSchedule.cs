using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EnrollmentSystemUCB.Models.Entities
{
    public class SubjectSchedule
    {
        [Key]
        public int SubjectEDPCode { get; set; }
        [Required]
        public string SubjectCode { get; set; }

        [ForeignKey("SubjectCode")]
        public virtual Subject Subject { get; set; }

        public string SUbjectDescription { get; set; }
        public TimeSpan SubjectTimeStart{ get; set; }
        public TimeSpan SubjectTimeEnd{ get; set; }
        public string SubjectDays { get; set; }
        public string SubjectSection { get; set; }
        public string SubjectSY { get; set; }
    }
}
