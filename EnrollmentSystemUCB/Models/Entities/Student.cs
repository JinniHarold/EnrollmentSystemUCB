using System.ComponentModel.DataAnnotations;

namespace EnrollmentSystemUCB.Models.Entities
{
    public class Student
    {
        public int Id { get; set; }

        public string StudLName { get; set; }
        public string StudFName { get; set; }
        public string ? StudMInitial { get; set; }
        public string StudCourse { get; set; }
        public int StudYear { get; set;}
        public string Password { get; set; }
    }
}
