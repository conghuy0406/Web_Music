using Microsoft.AspNetCore.Mvc;
using Web_Music.Models;
using Web_Music.Repositories;

namespace Web_Music.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserRepositories _nguoiDungRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AccountController(IUserRepositories nguoiDungRepository, IHttpContextAccessor httpContextAccessor)
        {
            _nguoiDungRepository = nguoiDungRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        // Hiển thị danh sách người dùng
        public async Task<IActionResult> Index()
        {
            var nguoiDungs = await _nguoiDungRepository.GetAllAsync();
            return View(nguoiDungs);
        }

        // Hiển thị thông tin chi tiết người dùng theo ID
        public async Task<IActionResult> Display(int id)
        {
            var nguoiDung = await _nguoiDungRepository.GetByIdAsync(id);
            if (nguoiDung == null)
            {
                return NotFound();
            }
            return View(nguoiDung);
        }

        // Hiển thị form thêm người dùng mới
        public IActionResult Add()
        {
            return View();
        }

        // Xử lý thêm người dùng mới
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(NguoiDung nguoiDung)
        {

            if (ModelState.IsValid)
            {
                await _nguoiDungRepository.AddAsync(nguoiDung);
                return RedirectToAction(nameof(Index));
            }
            return View(nguoiDung);
        }

        // Hiển thị form cập nhật thông tin người dùng
        public async Task<IActionResult> Update(int id)
        {
            var nguoiDung = await _nguoiDungRepository.GetByIdAsync(id);
            if (nguoiDung == null)
            {
                return NotFound();
            }
            return View(nguoiDung);
        }

        // Xử lý cập nhật thông tin người dùng
        [HttpPost]
        public async Task<IActionResult> Update(int id, NguoiDung nguoiDung)
        {
            if (id != nguoiDung.MaNguoiDung)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _nguoiDungRepository.UpdateAsync(nguoiDung);
                return RedirectToAction(nameof(Index));
            }
            return View(nguoiDung);
        }

        // Xác nhận xóa người dùng
        public async Task<IActionResult> Delete(int id)
        {
            var nguoiDung = await _nguoiDungRepository.GetByIdAsync(id);
            if (nguoiDung == null)
            {
                return NotFound();
            }
            return View(nguoiDung);
        }

        // Xử lý xóa người dùng
        [HttpPost, ActionName("DeleteConfirmed")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var nguoiDung = await _nguoiDungRepository.GetByIdAsync(id);
            if (nguoiDung != null)
            {
                await _nguoiDungRepository.DeleteAsync(id);
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string TenDangNhap, string MatKhau)
        {
            var nguoiDungs = await _nguoiDungRepository.GetAllAsync();
            var user = nguoiDungs.FirstOrDefault(u => u.TenDangNhap == TenDangNhap);

            // So sánh mật khẩu đã mã hóa
            if (user != null && VerifyPassword(MatKhau, user.MatKhau))
            {
                // Lưu thông tin vào Session
                _httpContextAccessor.HttpContext.Session.SetString("Email", user.Email);
                _httpContextAccessor.HttpContext.Session.SetInt32("UserId", user.MaNguoiDung);
                _httpContextAccessor.HttpContext.Session.SetInt32("Role", user.VaiTro ?? 0);

                // Điều hướng đến trang admin hoặc trang người dùng
                if (user.VaiTro == 1)
                {
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Tài khoản hoặc mật khẩu không chính xác.");
                return View();
            }
        }

        // Phương thức đăng ký
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(string TenDangNhap, string Email, string MatKhau)
        {
            var existingUser = await _nguoiDungRepository.GetAllAsync();
            if (existingUser.Any(u => u.TenDangNhap == TenDangNhap || u.Email == Email))
            {
                ModelState.AddModelError(string.Empty, "Tên đăng nhập hoặc Email đã tồn tại.");
                return View();
            }

            // Mã hóa mật khẩu trước khi lưu
            string hashedPassword = HashPassword(MatKhau);

            var nguoiDung = new NguoiDung
            {
                TenDangNhap = TenDangNhap,
                Email = Email,
                MatKhau = hashedPassword,
                VaiTro = 2, // Gán vai trò mặc định là admin
                NgayTao = DateTime.UtcNow
            };

            await _nguoiDungRepository.AddAsync(nguoiDung);
            return RedirectToAction("Login", "Account");
        }

        // Phương thức mã hóa mật khẩu
        private string HashPassword(string password)
        {
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                var hash = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hash);
            }
        }

        // Phương thức so sánh mật khẩu đã mã hóa
        private bool VerifyPassword(string enteredPassword, string storedPassword)
        {
            var enteredPasswordHash = HashPassword(enteredPassword);
            return enteredPasswordHash == storedPassword;
        }

        // Phương thức đăng xuất
        public IActionResult Logout()
        {
            // Xóa thông tin khỏi session
            _httpContextAccessor.HttpContext.Session.Clear();
            return RedirectToAction("Login", "Account");
        }


    }
}
