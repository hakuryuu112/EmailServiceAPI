using Microsoft.AspNetCore.Mvc;

namespace EmailServiceAPI.Controllers
{
    public class MainController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
