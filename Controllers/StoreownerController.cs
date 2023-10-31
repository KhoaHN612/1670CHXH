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

    // public async Task<IActionResult> IndexAsync()
    // {
    //     ViewBag.total = _context.OrderItems.Sum(o => o.Price);
    //     return _context.Orders != null ?
    //                 View(await _context.Orders.Include(o => o.OrderItems).ThenInclude(oi => oi.Book).ToListAsync()) :
    //                 Problem("Entity set 'Db1670asmContext.Orders'  is null.");
    // }

    public async Task<IActionResult> Index()
    {
        // var books = await _context.Books.ToListAsync();
        // if (books == null)
        // {
        //     return NotFound();
        // }
        // return View(books);
        ViewBag.total = _context.OrderItems.Sum(o => o.Price);
        return _context.Orders != null ?
                    View(await _context.Orders.Include(o => o.OrderItems).ThenInclude(oi => oi.Book).ToListAsync()) :
                    Problem("Entity set 'Db1670asmContext.Orders'  is null.");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
