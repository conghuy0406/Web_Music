using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web_Music.Models;
using Web_Music.Repositories;

namespace Web_Music.Controllers
{
    public class BaiHatsController : Controller
    {
        private readonly IBaiHatRepository _baiHatRepository;
        private readonly ICasiRepository _caSiRepository;
        private readonly ITheLoaiRepository _theLoaiRepository;

        public BaiHatsController(IBaiHatRepository baiHatRepository, ICasiRepository caSiRepository, ITheLoaiRepository theLoaiRepository)
        {
            _baiHatRepository = baiHatRepository;
            _caSiRepository = caSiRepository;
            _theLoaiRepository = theLoaiRepository;
        }

        // GET: BaiHats
        public async Task<IActionResult> Index()
        {
            var baiHats = await _baiHatRepository.GetAllAsync(); // Dùng await để lấy dữ liệu
            return View(baiHats);
        }

        // GET: BaiHats/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var baiHat = await _baiHatRepository.GetByIdAsync(id.Value); // Dùng await để lấy thông tin chi tiết
            if (baiHat == null)
            {
                return NotFound();
            }

            return View(baiHat);
        }

        // GET: BaiHats/Create
        public async Task<IActionResult> Create()
        {
            var baiHats = await _baiHatRepository.GetAllAsync(); // Dùng await để lấy danh sách bài hát
            var caSiList = await _caSiRepository.GetAllAsync(); // Lấy danh sách ca sĩ
            var TheLoaiList = await _theLoaiRepository.GetAllAsync(); // Lấy danh sách ca sĩ
            ViewData["MaAlbum"] = new SelectList(baiHats, "MaAlbum", "MaAlbum");
            ViewData["MaCaSi"] = new SelectList(caSiList, "MaCaSi", "TenCaSi");
            ViewData["MaTheLoai"] = new SelectList(TheLoaiList, "MaTheLoai", "TenTheLoai");
            return View();
        }

        // POST: BaiHats/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaBaiHat,TenBaiHat,MaCaSi,MaAlbum,MaTheLoai,ThoiLuong,DuongDanFile,AnhBia,LuotNghe,NgayThem")] BaiHat baiHat)
        {
            if (ModelState.IsValid)
            {
                await _baiHatRepository.AddAsync(baiHat); // Dùng await khi thêm mới bài hát
                return RedirectToAction(nameof(Index));
            }

            var baiHats = await _baiHatRepository.GetAllAsync(); // Dùng await để lấy lại danh sách
            ViewData["MaAlbum"] = new SelectList(baiHats, "MaAlbum", "MaAlbum", baiHat.MaAlbum);
            ViewData["MaCaSi"] = new SelectList(baiHats, "MaCaSi", "MaCaSi", baiHat.MaCaSi);
            ViewData["MaTheLoai"] = new SelectList(baiHats, "MaTheLoai", "MaTheLoai", baiHat.MaTheLoai);
            return View(baiHat);
        }

        // GET: BaiHats/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var baiHat = await _baiHatRepository.GetByIdAsync(id.Value); // Dùng await để lấy thông tin bài hát
            if (baiHat == null)
            {
                return NotFound();
            }

            var baiHats = await _baiHatRepository.GetAllAsync(); // Dùng await để lấy danh sách bài hát
            ViewData["MaAlbum"] = new SelectList(baiHats, "MaAlbum", "MaAlbum", baiHat.MaAlbum);
            ViewData["MaCaSi"] = new SelectList(baiHats, "MaCaSi", "MaCaSi", baiHat.MaCaSi);
            ViewData["MaTheLoai"] = new SelectList(baiHats, "MaTheLoai", "MaTheLoai", baiHat.MaTheLoai);
            return View(baiHat);
        }

        // POST: BaiHats/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaBaiHat,TenBaiHat,MaCaSi,MaAlbum,MaTheLoai,ThoiLuong,DuongDanFile,AnhBia,LuotNghe,NgayThem")] BaiHat baiHat)
        {
            if (id != baiHat.MaBaiHat)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _baiHatRepository.UpdateAsync(baiHat); // Dùng await khi cập nhật bài hát
                }
                catch (Exception)
                {
                    if (!await BaiHatExists(baiHat.MaBaiHat))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            var baiHats = await _baiHatRepository.GetAllAsync(); // Dùng await để lấy danh sách bài hát
            ViewData["MaAlbum"] = new SelectList(baiHats, "MaAlbum", "MaAlbum", baiHat.MaAlbum);
            ViewData["MaCaSi"] = new SelectList(baiHats, "MaCaSi", "MaCaSi", baiHat.MaCaSi);
            ViewData["MaTheLoai"] = new SelectList(baiHats, "MaTheLoai", "MaTheLoai", baiHat.MaTheLoai);
            return View(baiHat);
        }

        // GET: BaiHats/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var baiHat = await _baiHatRepository.GetByIdAsync(id.Value); // Dùng await để lấy thông tin bài hát cần xóa
            if (baiHat == null)
            {
                return NotFound();
            }

            return View(baiHat);
        }

        // POST: BaiHats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _baiHatRepository.DeleteAsync(id); // Dùng await khi xóa bài hát
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> BaiHatExists(int id)
        {
            var baiHat = await _baiHatRepository.GetByIdAsync(id); // Dùng await để kiểm tra bài hát tồn tại hay không
            return baiHat != null;
        }
    }
}
