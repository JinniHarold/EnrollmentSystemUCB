using EnrollmentSystemUCB.Models;
using Microsoft.AspNetCore.Mvc;

namespace EnrollmentSystemUCB.Controllers
{
    public class StudentController : Controller
    {
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(AddStudentViewModel viewModel)
        {
            return View();
        }
    }
}
