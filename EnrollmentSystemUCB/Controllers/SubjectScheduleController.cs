using EnrollmentSystemUCB.Data;
using EnrollmentSystemUCB.Models;
using EnrollmentSystemUCB.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EnrollmentSystemUCB.Controllers
{
    public class SubjectScheduleController: Controller
    {
        private readonly ApplicationDbContext dbContext;

        public SubjectScheduleController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
        public async Task<IActionResult> AddSubjectSchedule(string subjectCode)
        {
            // Find the subject with the provided subjectCode from the Subjects table
            var subject = await dbContext.Subjects.FirstOrDefaultAsync(s => s.SubjectCode == subjectCode);

            // If the subject doesn't exist, return to the subject list or handle error
            if (subject == null)
            {
                return RedirectToAction("ListSubject", "Subject"); // Redirect back to list if not found
            }

            // Create the view model with the subject's code and description pre-filled
            var viewModel = new AddSubjectScheduleViewModel
            {
                SubjectCode = subject.SubjectCode,
                SUbjectDescription = subject.SubjectDescription
            };

            return View(viewModel); // Pass the view model to the view
        }
        [HttpPost]
        public async Task<IActionResult> AddSubjectSchedule(AddSubjectScheduleViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            // Create a new SubjectSchedule entity from the view model
            var subjectSched = new SubjectSchedule
            {
                SubjectEDPCode = viewModel.SubjectEDPCode, // This should be filled if necessary
                SubjectCode = viewModel.SubjectCode,
                SUbjectDescription = viewModel.SUbjectDescription,
                SubjectTimeStart = viewModel.SubjectTimeStart,
                SubjectTimeEnd = viewModel.SubjectTimeEnd,
                SubjectDays = viewModel.SubjectDays,
                SubjectSection = viewModel.SubjectSection,
                SubjectSY = viewModel.SubjectSY
            };

            // Save the SubjectSchedule entity to the database
            await dbContext.SubjectSchedules.AddAsync(subjectSched);
            await dbContext.SaveChangesAsync();

            // Redirect to the list of subjects or any other relevant page after saving
            return RedirectToAction("ListSubject", "Subject");
        }
    }
}
