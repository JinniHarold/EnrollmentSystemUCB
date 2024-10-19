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
        public IActionResult AddSubjectSchedule()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddSubjectSchedule(AddSubjectScheduleViewModel viewModel, AddSubjectViewModel subjModel)
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
