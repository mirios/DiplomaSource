using Microsoft.AspNetCore.Mvc;

namespace verifyPlatform.Controllers
{
    public class Error : Controller
    {
        public IActionResult Error404()
        {
            return View();
        }
    }
}
