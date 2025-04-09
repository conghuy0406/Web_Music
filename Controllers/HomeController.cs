using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using Web_Music.Models;

namespace Web_Music.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _context;

    public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context; 
    }

    public IActionResult Index()
    {
        var model = new MusicViewModel1
        {
            Albums = _context.Albums.ToList(),
            BaiHats = _context.BaiHats.ToList(),
            CaSis = _context.CaSis.ToList(),
            NguoiDungs = _context.NguoiDungs.ToList(),
            TheLoais = _context.TheLoais.ToList()
        };

        return View(model);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    // Albums Store View
    public IActionResult AlbumsStore()
    {
        var model = new MusicViewModel1
        {
            Albums = _context.Albums.ToList(),
            BaiHats = _context.BaiHats.ToList(),
            CaSis = _context.CaSis.ToList(),
            NguoiDungs = _context.NguoiDungs.ToList(),
            TheLoais = _context.TheLoais.ToList()
        };
        return View(model);
    }

    // Blog View
    public IActionResult Blog()
    {
        return View();
    }

    // Contact View
    public IActionResult Contact()
    {
        return View();
    }

    // Elements View
    public IActionResult Elements()
    {
        return View();
    }

    // Event View
    public IActionResult Event()
    {
        return View();
    }

    // Login View
    public IActionResult Login()
    {
        return View("~/Views/Account/login.cshtml");
    }

    // Privacy View
    public IActionResult Privacy()
    {
        return View();
    }

    // Register View
    public IActionResult Register()
    {
        return View("~/Views/Account/register.cshtml");
    }

    [Route("Home/[action]/{id?}")]
    public IActionResult Display(int id)
    {
        var model = new MusicViewModel1
        {
            Albums = _context.Albums.ToList(),
            BaiHats = _context.BaiHats.Where(b => b.MaAlbum == id).ToList(),
            CaSis = _context.CaSis.ToList(),
            NguoiDungs = _context.NguoiDungs.ToList(),
            TheLoais = _context.TheLoais.ToList()
        };
        
        return View(model);
    }

}
