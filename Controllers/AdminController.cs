using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ASMProject.Models;
using Microsoft.EntityFrameworkCore;

namespace ASMProject.Controllers;

public class AdminController : Controller
{
    private readonly Db1670asmContext _context;

    public AdminController(Db1670asmContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }

    public async Task<IActionResult> Category()
    {
        return _context.Categories != null ? 
                    View(await _context.Categories.ToListAsync()) :
                    Problem("Entity set 'Db1670asmContext.Categories'  is null.");
    }

    public async Task<IActionResult> ProcessStatus(int? id, byte status)
    {
        if (id == null || _context.Categories == null)
        {
            return NotFound();
        }

        var category = await _context.Categories.FindAsync(id);
        if (category == null)
        {
            return NotFound();
        }
        category.Status = status;
        _context.Update(category);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(category));
    }

    public async Task<IActionResult> DeleteStatus(int? id)
    {
        var category = await _context.Categories.FindAsync(id);
        if (category != null)
        {
            _context.Categories.Remove(category);
        }
        
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(category));
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
