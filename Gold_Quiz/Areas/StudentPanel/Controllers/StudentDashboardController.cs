using Microsoft.AspNetCore.Mvc;

namespace Gold_Quiz.Areas.StudentPanel.Controllers
{
    [Area("StudentPanel")]
    public class StudentDashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
