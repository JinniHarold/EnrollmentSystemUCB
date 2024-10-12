﻿using System.ComponentModel.DataAnnotations;

namespace EnrollmentSystemUCB.Models
{
    public class AddSubjectViewModel
    {
        [Key]
        [Required(ErrorMessage = "Subject Code is required.")]
        public string SubjectCode { get; set; }

        [Required(ErrorMessage = "Subject Description is required.")]
        public string SubjectDescription { get; set; }

        [Required(ErrorMessage = "Subject Units are required.")]
        public int SubjectUnits { get; set; }

        [Required(ErrorMessage = "Subject Category is required.")]
        public string SubjectCategory { get; set; }

        [Required(ErrorMessage = "Subject Offering is required.")]
        public string SubjectOffering { get; set; }

        [Required(ErrorMessage = "Subject Course is required.")]
        public string SubjectCourse { get; set; }

        [Required(ErrorMessage = "Curriculum Year is required.")]
        public string SubjectCurrYear { get; set; }
    }
}