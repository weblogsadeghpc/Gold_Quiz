using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Gold_Quiz.Areas.TeacherPanel.Controllers
{
    [Area("TeacherPanel")]
    [Authorize(Roles = "Teacher")]
    public class TeacherDashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
