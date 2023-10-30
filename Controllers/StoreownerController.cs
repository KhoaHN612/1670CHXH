using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ASMProject.Models;
using Microsoft.EntityFrameworkCore;

namespace ASMProject.Controllers;

public class StoreownerController : Controller
{
    private readonly Db1670asmContext _context;

    public StoreownerController(Db1670asmContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
