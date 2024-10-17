using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EnrollmentSystemUCB.Models.Entities
{
    public class SubjectSchedule
    {
        [Key]
        public int SubjectEDPCode { get; set; }
        public string SubjectCode { get; set; }
        [ForeignKey("SubjectCode")]
        public virtual Subject Subject { get; set; }

        public string SubjectDescription { get; set; }
        public TimeOnly SubjectTimeStart{ get; set; }
        public TimeOnly SubjectTimeEnd{ get; set; }
        public string SubjectDays { get; set; }
        public string SubjectSection { get; set; }
        public string SubjectSY { get; set; }
    }
}
