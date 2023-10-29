using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ASMProject.Models;
using Microsoft.EntityFrameworkCore;

namespace ASMProject.Controllers;

public class CustomerController : Controller
{
    private readonly ILogger<CustomerController> _logger;

    private readonly Db1670asmContext _context;

    public CustomerController(ILogger<CustomerController> logger, Db1670asmContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var books =await _context.Books.ToListAsync();
        if(books == null){
            return NotFound();
        }
        return View(books);
    }

    public IActionResult Privacy()
    {
        return View();
    }
        public IActionResult Login()
    {
        return View();
    }
  

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
