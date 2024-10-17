using EnrollmentSystemUCB.Models.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EnrollmentSystemUCB.Models
{
    public class AddSubjectScheduleViewModel
    {
        [Key]
        public int SubjectEDPCode { get; set; }
        public string SubjectCode { get; set; }
        [ForeignKey("SubjectCode")]
        public virtual Subject Subject { get; set; }

        public string SUbjectDescription { get; set; }
        public TimeOnly SubjectTimeStart { get; set; }
        public TimeOnly SubjectTimeEnd { get; set; }
        public string SubjectDays { get; set; }
        public string SubjectSection { get; set; }
        public string SubjectSY { get; set; }
    }
}
