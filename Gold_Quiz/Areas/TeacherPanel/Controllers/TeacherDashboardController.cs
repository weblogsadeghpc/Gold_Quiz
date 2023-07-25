using Microsoft.AspNetCore.Mvc;

namespace Gold_Quiz.Areas.TeacherPanel.Controllers
{
    [Area("TeacherPanel")]
    public class TeacherDashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
