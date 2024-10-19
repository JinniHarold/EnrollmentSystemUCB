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

            
            if (subject == null)
            {
                return RedirectToAction("ListSubject", "Subject");
            }

            var viewModel = new AddSubjectScheduleViewModel
            {
                SubjectCode = subject.SubjectCode,
                SUbjectDescription = subject.SubjectDescription
            };

            return View(viewModel); 
        }
        [HttpPost]
        public async Task<IActionResult> AddSubjectSchedule(AddSubjectScheduleViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var subjectSched = new SubjectSchedule
            {
                SubjectEDPCode = viewModel.SubjectEDPCode,
                SubjectCode = viewModel.SubjectCode,
                SUbjectDescription = viewModel.SUbjectDescription,
                SubjectTimeStart = viewModel.SubjectTimeStart,
                SubjectTimeEnd = viewModel.SubjectTimeEnd,
                SubjectDays = viewModel.SubjectDays,
                SubjectSection = viewModel.SubjectSection,
                SubjectSY = viewModel.SubjectSY
            };

            await dbContext.SubjectSchedules.AddAsync(subjectSched);
            await dbContext.SaveChangesAsync();

            return RedirectToAction("ListSubject", "Subject");
        }
    }
}
