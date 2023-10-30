using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ASMProject.Models;
using Microsoft.EntityFrameworkCore;

namespace ASMProject.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    private readonly Db1670asmContext _context;

    public HomeController(ILogger<HomeController> logger, Db1670asmContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        switch (User.Identity.IsAuthenticated, User.IsInRole("StoreOwner"), User.IsInRole("Admin"))
        {
            case (true, true, false):
                return RedirectToAction("Index", "Storeowner");
            case (true, false, true):
                return RedirectToAction("Index", "Admin");
            default:
                var books = await _context.Books.ToListAsync();
                if (books == null)
                {
                    return NotFound();
                }
                return View(books);
        }
    }

    public IActionResult Search()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult About()
    {
        return View();
    }

    public IActionResult Contact()
    {
        return View();
    }

    public IActionResult Login()
    {
        return View();
    }
    public IActionResult Register()
    {
        return View();
    }
    public IActionResult ProductDetail()
    {
        return View();
    }

    public IActionResult Products()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
