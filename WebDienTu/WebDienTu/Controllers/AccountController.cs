using Microsoft.AspNetCore.Mvc;
using WebDienTu.Models;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace WebDienTu.Controllers
{
    public class AccountController : Controller
    {
        private readonly DienTuStoreContext _context;

        public AccountController(DienTuStoreContext context)
        {
            _context = context;
        }

        // Hiển thị form đăng nhập
        [HttpGet]
        public IActionResult Login()
        {
            // Nếu đã đăng nhập thì redirect đúng vai trò
            if (HttpContext.Session.GetString("Email") != null)
            {
                var vaitro = HttpContext.Session.GetInt32("VaiTro");
                if (vaitro == 1)
                    return RedirectToAction("Index", "Home", new { area = "Admin" });
                else
                    return RedirectToAction("Index", "Home", new { area = "User" });
            }

            return View();
        }

        // Xử lý đăng nhập
        [HttpPost]
        public IActionResult Login(string email, string matkhau)
        {
            ViewBag.Email = email; // Giữ lại email khi form reload

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(matkhau))
            {
                ViewBag.Error = "Vui lòng nhập đầy đủ thông tin!";
                return View();
            }

            var user = _context.NguoiDungs
                        .FirstOrDefault(u => u.Email == email && u.MatKhau == matkhau);

            if (user == null)
            {
                ViewBag.Error = "Email hoặc mật khẩu không đúng!";
                return View();
            }

            // Lưu session
            HttpContext.Session.SetString("Email", user.Email);
            HttpContext.Session.SetInt32("VaiTro", user.VaiTro ?? 0);
            HttpContext.Session.SetString("HoTen", user.HoTen);

            // Chuyển trang theo vai trò
            if (user.VaiTro == 1)
                return RedirectToAction("Index", "Home", new { area = "Admin" });
            else
                return RedirectToAction("Index", "Home", new { area = "User" });
        }

        // Đăng xuất
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
