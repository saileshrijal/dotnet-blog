using Microsoft.AspNetCore.Mvc;

namespace AliveBlog.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}