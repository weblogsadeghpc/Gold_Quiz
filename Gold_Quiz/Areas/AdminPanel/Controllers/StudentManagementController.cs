using AutoMapper;
using Gold_Quiz.DataModel.Entities;
using Gold_Quiz.DataModel.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Gold_Quiz.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    [Authorize(Roles = "Admin")]
    public class StudentManagementController : Controller
    {
        private readonly IUnitOfWork _context;
        private readonly UserManager<ApplicationUsers> _userManager;

        public StudentManagementController(IUnitOfWork context, UserManager<ApplicationUsers> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            var model = _context.centerUsersUW.
                Get(c => c.CenterAdminID == _userManager.GetUserId(HttpContext.User) && c.Users_ST.UserType == 3, "Users_ST");
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
    }
}
