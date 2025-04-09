using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web_Music.Models;
using Web_Music.Repositories;
using Microsoft.EntityFrameworkCore; 

namespace Web_Music.Controllers
{
    public class AlbumController : Controller
    {
        private readonly IAlbumRepository _albumRepository;
        private readonly ICasiRepository _caSiRepository; 

        public AlbumController(IAlbumRepository albumRepository, ICasiRepository caSiRepository)
        {
            _albumRepository = albumRepository;
            _caSiRepository = caSiRepository; 
        }

        // Hiển thị danh sách album
        public async Task<IActionResult> Index()
        {
            var albums = await _albumRepository.GetAllAsync();
            return View(albums); // Trả về View danh sách album
        }

        // Hiển thị chi tiết album
        public async Task<IActionResult> Details(int id)
        {
            var album = await _albumRepository.GetByIdAsync(id); // Lấy album theo id
            if (album == null)
            {
                return NotFound(); // Nếu album không tồn tại, trả về lỗi
            }

            return View(album); // Trả về View chi tiết album
        }

        // Hiển thị form thêm album
        public async Task<IActionResult> Add()
        {
            // Lấy danh sách ca sĩ và truyền vào ViewBag
            var caSiList = await _caSiRepository.GetAllAsync(); // Lấy danh sách ca sĩ
            ViewBag.MaCaSi = new SelectList(caSiList, "MaCaSi", "TenCaSi");
            return View();
        }

        // Xử lý thêm album mới
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(Album album)
        {
            if (ModelState.IsValid)
            {
                // Tìm CaSi từ MaCaSi
                var caSi = await _caSiRepository.GetByIdAsync(album.MaCaSi);
                if (caSi == null)
                {
                    ModelState.AddModelError("MaCaSi", "Ca sĩ không tồn tại.");
                    return View(album); // Trả về nếu không tìm thấy ca sĩ
                }

                // Gán MaCaSiNavigation cho album
                album.MaCaSiNavigation = caSi;

                // Thêm album vào cơ sở dữ liệu
                await _albumRepository.AddAsync(album);
                return RedirectToAction(nameof(Index));
            }

            // Trả lại danh sách ca sĩ nếu model không hợp lệ
            var caSiList = await _caSiRepository.GetAllAsync();
            ViewBag.MaCaSi = new SelectList(caSiList, "MaCaSi", "TenCaSi");
            return View(album);
        }

        // Hiển thị form cập nhật album
        public async Task<IActionResult> Update(int id)
        {
            var album = await _albumRepository.GetByIdAsync(id);
            if (album == null)
            {
                return NotFound();
            }

            // Lấy danh sách ca sĩ và truyền vào ViewBag
            var caSiList = await _caSiRepository.GetAllAsync();
            ViewBag.MaCaSi = new SelectList(caSiList, "MaCaSi", "TenCaSi", album.MaCaSi);

            return View(album);
        }

        // Xử lý cập nhật album
        [HttpPost]
        public async Task<IActionResult> Update(int id, Album album)
        {
            if (id != album.MaAlbum)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var existingAlbum = await _albumRepository.GetByIdAsync(id);

                if (existingAlbum == null)
                {
                    return NotFound();
                }

                // Tìm CaSi từ MaCaSi và gán vào MaCaSiNavigation
                var caSi = await _caSiRepository.GetByIdAsync(album.MaCaSi);
                if (caSi == null)
                {
                    ModelState.AddModelError("MaCaSi", "Ca sĩ không tồn tại.");
                    return View(album); // Trả về nếu không tìm thấy ca sĩ
                }

                // Gán MaCaSiNavigation cho album
                existingAlbum.MaCaSiNavigation = caSi;

                // Cập nhật các thông tin khác của album
                existingAlbum.TenAlbum = album.TenAlbum;
                existingAlbum.AnhBia = album.AnhBia;
                existingAlbum.NgayPhatHanh = album.NgayPhatHanh;
                existingAlbum.MaCaSi = album.MaCaSi;

                // Cập nhật vào cơ sở dữ liệu
                await _albumRepository.UpdateAsync(existingAlbum);

                return RedirectToAction(nameof(Index));
            }

            // Trả lại danh sách ca sĩ vào ViewBag nếu model không hợp lệ
            var caSiList = await _caSiRepository.GetAllAsync();
            ViewBag.MaCaSi = new SelectList(caSiList, "MaCaSi", "TenCaSi", album.MaCaSi);
            return View(album);
        }

        // Hiển thị form xóa album
        public async Task<IActionResult> Delete(int id)
        {
            var album = await _albumRepository.GetByIdAsync(id);
            if (album == null)
            {
                return NotFound();
            }
            return View(album);
        }

        // Xử lý xóa album
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int MaAlbum)
        {
            var album = await _albumRepository.GetByIdAsync(MaAlbum);
            if (album == null)
            {
                return NotFound();
            }
            await _albumRepository.DeleteAsync(MaAlbum);
            return RedirectToAction(nameof(Index));
        }
    }
}
