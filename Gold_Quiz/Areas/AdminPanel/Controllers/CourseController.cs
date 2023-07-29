using Gold_Quiz.DataModel.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Gold_Quiz.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    [Authorize(Roles = "Admin")]
    public class CourseController : Controller
    {
        private readonly IUnitOfWork _context;

        public CourseController(IUnitOfWork context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var model = _context.coursesUW.Get(); //list doros
            return View(model);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
    }
}
