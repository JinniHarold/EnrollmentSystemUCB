using System.ComponentModel.DataAnnotations;

namespace EnrollmentSystemUCB.Models.Entities
{
    public class Student
    {
        public Guid Id { get; set; }

        public string StudLName { get; set; }
        public string StudFName { get; set; }
        public string ? StudMInitial { get; set; }
        public string StudCourse { get; set; }
        public int StudYear { get; set;}
    }
}
