using Microsoft.AspNetCore.Mvc;

namespace ExamApplication.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
