using Microsoft.AspNetCore.Mvc;

namespace Gold_Quiz.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
    }
}
