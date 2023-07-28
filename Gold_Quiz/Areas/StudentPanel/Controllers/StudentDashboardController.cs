using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gold_Quiz.Areas.StudentPanel.Controllers
{
    [Area("StudentPanel")]
    [Authorize(Roles = "Student")] // kasani dastresi dashete bashand ke role student dashte bashand
    public class StudentDashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
