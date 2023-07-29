using Gold_Quiz.DataModel.Entities;
using Gold_Quiz.DataModel.Models;
using Gold_Quiz.DataModel.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Gold_Quiz.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    [Authorize(Roles = "Admin")]

    public class CourseController : Controller
    {
        private readonly IUnitOfWork _context;
        // shenasaii kodom user dare az system estefade mikone
        public readonly UserManager<ApplicationUsers> _userManager;

        public CourseController(IUnitOfWork context, UserManager<ApplicationUsers> userManager)
        {
            _context = context;
            _userManager = userManager;
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
        [HttpPost]
        public IActionResult Create(CourseViewModel model)
        {
            if (ModelState.IsValid)
            {
                Courses C = new Courses
                {
                    // chon yedoone fild darim nemiaim az auto mapper estefade konim 
                    // vaghti auto mapper estefade mikonim ke hadeaghal 7-8 field dashte bashim 
                    CourseName = model.CourseName,
                    UserID = _userManager.GetUserId(HttpContext.User)
                    // id user ke dare ba systemm karmikone dar miarim 
                };
                _context.coursesUW.Create(C);
                _context.Save();
                return RedirectToAction("Index"); // boro be action index 
            }
            else
            {
                return View(model);
            }
        }
    }
}
