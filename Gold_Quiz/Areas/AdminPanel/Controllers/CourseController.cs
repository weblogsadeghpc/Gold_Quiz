using AutoMapper;
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
        private readonly IMapper _mapper;
        // shenasaii kodom user dare az system estefade mikone
        public readonly UserManager<ApplicationUsers> _userManager;

        public CourseController(IUnitOfWork context, UserManager<ApplicationUsers> userManager
            , IMapper mapper)
        {
            _context = context;
            _userManager = userManager;
            _mapper = mapper;
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
        [ValidateAntiForgeryToken]
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

        [HttpGet]
        // bayad id reccord ke mikhaim edit konim ro begirim 
        public IActionResult Edit(int CourseId)
        {
            if (CourseId == 0)
            {
                return Redirect("Home/Error");
            }
            // id ro bayad dar database peyda koni va etelaat oon ro befresti be view 
            var mapModel = _mapper.Map<CourseViewModel>(_context.coursesUW.GetById(CourseId)); // <destination>  (model)
            // course id ro behesh midim be ma model ro mide
            // bayad az mapper estefade konim 
            return View(mapModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // baraye amniate // baes mishe token taghir nakone -> age taghir kard motavajeh mishe 
        public IActionResult Edit(CourseViewModel model)
        {
            if (ModelState.IsValid)
            {
                //ویرایش
                // bayad az mapper estefade konim chon faghat courseview ro darim dar sorati ke mikhaim az Course estefade konim 
                var mapModel = _mapper.Map<Courses>(model);
                _context.coursesUW.Update(mapModel);
                _context.Save();
                return RedirectToAction("Index"); // vaghti karet tamom shod boro be index
            }
            else
            {
                return View(model);
            }

        }
    }
}
