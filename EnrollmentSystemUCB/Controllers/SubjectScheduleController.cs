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
            var subject = await dbContext.Subjects
                .Include(s => s.SubjectSchedules)
                .FirstOrDefaultAsync(s => s.SubjectCode == subjectCode);

            if (subject == null)
            {
                return RedirectToAction("ListSubject", "Subject");
            }

            var schedule = await dbContext.SubjectSchedules
                .FirstOrDefaultAsync(s => s.SubjectCode == subjectCode);

            var viewModel = new AddSubjectScheduleViewModel
            {
                SubjectCode = subject.SubjectCode,
                SUbjectDescription = subject.SubjectDescription,
                SubjectTimeStart = schedule?.SubjectTimeStart ?? TimeSpan.Zero,
                SubjectTimeEnd = schedule?.SubjectTimeEnd ?? TimeSpan.Zero,
                SubjectDays = schedule?.SubjectDays ?? "",
                SubjectSection = schedule?.SubjectSection ?? "",
                SubjectSY = schedule?.SubjectSY ?? "",
                SubjectEDPCode = schedule?.SubjectEDPCode ?? 00000
            };

            return View(viewModel);
        }


        [HttpPost]
        public async Task<IActionResult> AddSubjectSchedule(AddSubjectScheduleViewModel viewModel)
        {
            var subjectSched = await dbContext.SubjectSchedules
                                              .FirstOrDefaultAsync(s => s.SubjectCode == viewModel.SubjectCode && s.SubjectSection == viewModel.SubjectSection);

            if (subjectSched == null)
            {
                subjectSched = new SubjectSchedule
                {
                    // Generate a unique SubjectEDPCode
                    SubjectEDPCode = GenerateEDPCode(), // Assign the generated EDP code
                    SubjectCode = viewModel.SubjectCode,
                    SUbjectDescription = viewModel.SUbjectDescription,
                    SubjectTimeStart = viewModel.SubjectTimeStart,
                    SubjectTimeEnd = viewModel.SubjectTimeEnd,
                    SubjectDays = viewModel.SubjectDays,
                    SubjectSection = viewModel.SubjectSection,
                    SubjectSY = viewModel.SubjectSY
                };

                dbContext.SubjectSchedules.Add(subjectSched);
            }
            else
            {
                // Update existing schedule properties as needed
                subjectSched.SubjectCode = viewModel.SubjectCode;
                subjectSched.SUbjectDescription = viewModel.SUbjectDescription;
                subjectSched.SubjectTimeStart = viewModel.SubjectTimeStart;
                subjectSched.SubjectTimeEnd = viewModel.SubjectTimeEnd;
                subjectSched.SubjectDays = viewModel.SubjectDays;
                subjectSched.SubjectSection = viewModel.SubjectSection;
                subjectSched.SubjectSY = viewModel.SubjectSY;
            }

            await dbContext.SaveChangesAsync();

            return RedirectToAction("ListSubject", "Subject");
        }

        private int GenerateEDPCode()
        {
            Random random = new Random();
            int newEDPCode;
            do
            {
                // Generate a random 6-digit number
                newEDPCode = random.Next(10000, 99999);

            } while (dbContext.SubjectSchedules.Any(s => s.SubjectEDPCode == newEDPCode));

            return newEDPCode;
        }
    }
}   