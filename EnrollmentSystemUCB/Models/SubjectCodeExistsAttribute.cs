using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using EnrollmentSystemUCB.Data;

public class SubjectCodeExistsAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var dbContext = (ApplicationDbContext)validationContext.GetService(typeof(ApplicationDbContext));
        var subjectCode = value as string;

        // Check if the subject code exists in the database
        var exists = dbContext.Subjects.Any(s => s.SubjectCode == subjectCode);

        if (exists)
        {
            return new ValidationResult("This subject code already exists.");
        }

        return ValidationResult.Success;
    }
}