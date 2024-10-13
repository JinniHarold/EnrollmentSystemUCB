using EnrollmentSystemUCB.Data;
using EnrollmentSystemUCB.Models;
using EnrollmentSystemUCB.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EnrollmentSystemUCB.Controllers
{
    public class SubjectController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public SubjectController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult AddSubject()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddSubject(AddSubjectViewModel viewModel)
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
        [HttpGet]
        public async Task<IActionResult> ListSubject()
        {
            var subjects = await dbContext.Subjects.ToListAsync();
            return View(subjects);
        }
        [HttpGet]
        public async Task<IActionResult> EditSubject(string SubjectCode)
        {
            var subject = await dbContext.Subjects.FindAsync(SubjectCode);

            return View(subject);
        }
        [HttpPost]
        public async Task<IActionResult> EditSubject(Subject viewModel)
        {
            var subject = await dbContext.Subjects.FindAsync(viewModel.SubjectCode);

            if (subject is not null)
            {
                subject.SubjectDescription = viewModel.SubjectDescription;
                subject.SubjectUnits = viewModel.SubjectUnits;
                subject.SubjectOffering = viewModel.SubjectOffering;
                subject.SubjectCategory = viewModel.SubjectCategory;
                subject.SubjectCourse = viewModel.SubjectCourse;
                subject.SubjectCurrYear = viewModel.SubjectCurrYear;
                subject.SubjectPre = viewModel.SubjectPre ?? " ";
                subject.SubjectCo = viewModel.SubjectCo ?? " ";

                await dbContext.SaveChangesAsync();
            }
            return RedirectToAction("ListSubject", "Subject");
        }
        [HttpPost]
        public async Task<IActionResult> DeleteSubject(Subject viewModel)
        {
            var subject = await dbContext.Subjects.AsNoTracking().FirstOrDefaultAsync(x => x.SubjectCode == viewModel.SubjectCode);

            if (subject is not null)
            {
                dbContext.Subjects.Remove(viewModel);
                await dbContext.SaveChangesAsync();
            }

            return RedirectToAction("ListSubject", "Subject");
        }
    }
}
