using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ASMProject.Models;

namespace ASMProject.Controllers
{
    public class BookController : Controller
    {
        private readonly Db1670asmContext _context;

        private readonly IWebHostEnvironment _webHostEnvironment;

        public BookController(Db1670asmContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        private string GetUniqueFileName(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            return Path.GetFileNameWithoutExtension(fileName)
                   + "_"
                   + Guid.NewGuid().ToString().Substring(0, 4)
                   + Path.GetExtension(fileName);
        }


        public IActionResult Show(string category)
        {
            var books = _context.Books.ToList();
            ViewBag.Categories = _context.Categories
                .Select(cat => cat.Name)
                .Distinct()
                .ToList();

            if (!string.IsNullOrEmpty(category))
            {
                books = _context.Books
                    .Where(book => book.Cat.Name == category)
                    .ToList();
            }

            return View(books);
        }

        [HttpGet]
        public async Task<IActionResult> Search(string searchkey)
        {
            ViewBag.key = searchkey;
            var book = from b in _context.Books select b;
            if (!string.IsNullOrEmpty(searchkey))
            {
                book = book.Where(b => b.Title!.Contains(searchkey));
            }
            return View(await book.ToListAsync());
        }

        // GET: Book
        public async Task<IActionResult> Index()
        {
            var db1670asmContext = _context.Books.Include(b => b.Cat);
            return View(await db1670asmContext.ToListAsync());
        }

        // GET: Book/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Books == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .Include(b => b.Cat)
                .FirstOrDefaultAsync(m => m.BookId == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Book/Create
        public IActionResult Create()
        {
            ViewData["CatId"] = new SelectList(_context.Categories.Where(cat => cat.Status == 2), "CatId", "Name");
            return View();
        }

        // POST: Book/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BookId,Title,Image,Description,CatId,Price,ImageFile")] Book book)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = GetUniqueFileName(book.ImageFile.FileName);
                string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await book.ImageFile.CopyToAsync(fileStream);
                }
                book.Image = uniqueFileName;

                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CatId"] = new SelectList(_context.Categories, "CatId", "Name", book.CatId);
            return View(book);
        }

        // GET: Book/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Books == null)
            {
                return NotFound();
            }

            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            ViewData["CatId"] = new SelectList(_context.Categories.Where(cat => cat.Status == 2), "CatId", "Name", book.CatId);
            return View(book);
        }

        // POST: Book/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BookId,Title,Image,Description,CatId,Price,ImageFile")] Book book)
        {
            if (id != book.BookId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (book.ImageFile != null)
                {
                    string uniqueFileName = GetUniqueFileName(book.ImageFile.FileName);
                    string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await book.ImageFile.CopyToAsync(fileStream);
                    }
                    book.Image = uniqueFileName;
                }

                try
                {
                    _context.Update(book);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.BookId))
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
            ViewData["CatId"] = new SelectList(_context.Categories, "CatId", "Name", book.CatId);
            return View(book);
        }

        // GET: Book/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Books == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .Include(b => b.Cat)
                .FirstOrDefaultAsync(m => m.BookId == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Book/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Books == null)
            {
                return Problem("Entity set 'Db1670asmContext.Books'  is null.");
            }
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                _context.Books.Remove(book);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
            return (_context.Books?.Any(e => e.BookId == id)).GetValueOrDefault();
        }
    }
}
