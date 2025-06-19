using Microsoft.AspNetCore.Mvc;

namespace Frontend.Controllers
{
    public class TaskController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}