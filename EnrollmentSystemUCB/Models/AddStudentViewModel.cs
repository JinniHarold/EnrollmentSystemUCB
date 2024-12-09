using System.ComponentModel.DataAnnotations;

namespace EnrollmentSystemUCB.Models
{
    public class AddStudentViewModel
    {
        [Required(ErrorMessage = "First name is required.")]
        public string StudFName { get; set; }

        public string ? StudMInitial { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        public string StudLName { get; set; }

        [Required(ErrorMessage = "Course is required.")]
        public string StudCourse { get; set; }

        [Required(ErrorMessage = "Year is required.")]
        [Range(1, 4, ErrorMessage = "Year must be between 1 and 4.")]
        public int StudYear { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }
    }
}
