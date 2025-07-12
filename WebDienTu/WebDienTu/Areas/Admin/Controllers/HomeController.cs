using Microsoft.AspNetCore.Mvc;

namespace WebDienTu.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            // Kiểm tra phân quyền ở đây nếu cần
            if (HttpContext.Session.GetInt32("VaiTro") != 1)
            {
                return RedirectToAction("Login", "Account", new { area = "" });
            }

            ViewData["Title"] = "Trang Quản Trị";
            return View();
        }
    }
}
