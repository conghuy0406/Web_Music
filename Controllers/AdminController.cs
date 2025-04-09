using Microsoft.AspNetCore.Mvc;

namespace Web_Music.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            // Kiểm tra vai trò từ session
            var userRole = HttpContext.Session.GetInt32("Role");
            if (userRole != 1) // Vai trò 1 là Admin
            {
                return RedirectToAction("Index", "Home"); // Chuyển hướng về trang chính nếu không phải Admin
            }

            return View();
        }
    }
}
