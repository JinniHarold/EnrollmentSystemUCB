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

        public IActionResult AddSubjectSchedule()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddSubjectSchedule(AddSubjectScheduleViewModel viewModel)
        {

            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var subject = new Subject
            {
                SubjectCode = viewModel.SubjectCode,
                SubjectDescription = viewModel.SubjectDescription,
                SubjectUnits = viewModel.SubjectUnits,
                SubjectOffering = viewModel.SubjectOffering,
                SubjectCategory = viewModel.SubjectCategory,
                SubjectCourse = viewModel.SubjectCourse,
                SubjectCurrYear = viewModel.SubjectCurrYear,
                SubjectPre = viewModel.SubjectPre ?? " ",
                SubjectCo = viewModel.SubjectCo ?? " ",
            };

            await dbContext.Subjects.AddAsync(subject);
            await dbContext.SaveChangesAsync();

            return RedirectToAction("ListSubject", "Subject");
        }
    }
}
