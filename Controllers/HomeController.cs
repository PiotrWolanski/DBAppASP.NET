using Microsoft.AspNetCore.Mvc;

namespace AspNetUserManagement.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() => View();
    }
}