using System.ComponentModel.DataAnnotations;

namespace EnrollmentSystemUCB.Models.Entities
{
    public class Subject
    {
        [Key]
        public string SubjectCode { get; set; }

        public string SubjectDescription { get; set; }
        public int SubjectUnits { get; set; }
        public string SubjectCategory { get; set; }
        public string SubjectOffering { get; set; }
        public string SubjectCourse { get; set; }
        public string SubjectCurrYear { get; set; }
        public string SubjectPre { get; set; }
        public string SubjectCo { get; set; }
    }
}
