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
    public class OrderController : Controller
    {
        private readonly Db1670asmContext _context;

        public OrderController(Db1670asmContext context)
        {
            _context = context;
        }

        // GET: Order
        public async Task<IActionResult> Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home"); 
            }
            return _context.Orders != null ? 
                        View(await _context.Orders.Include(o => o.OrderItems).Where(o=>o.CusId == User.Identity.Name).ToListAsync()) :
                        Problem("Entity set 'Db1670asmContext.Orders'  is null.");
        }

        // GET: Order/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(b=>b.Book)
                .FirstOrDefaultAsync(m => m.OrId == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Order/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Order/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrId,CusId,OrTime,Address,Status")] Order order)
        {
            if (ModelState.IsValid)
            {
                var userId = User.Identity.Name;
                var cartItems = await _context.Carts
                            .Where(c => c.Uid == userId)
                            .Include(c => c.Book)
                            .ToListAsync();

                if (cartItems.Any())
                {
                    order.CusId = userId;
                    order.OrTime = DateTime.Now;
                    order.Status = "Waiting";
                    _context.Add(order);
                    await _context.SaveChangesAsync();

                    foreach (var cartItem in cartItems)
                    {
                        var orderItem = new OrderItem
                        {
                            OrId = order.OrId,
                            BookId = cartItem.BookId,
                            Quantity = cartItem.Quantity,
                            Price = cartItem.Price
                        };
                        _context.Add(orderItem);

                    }

                    await _context.SaveChangesAsync();

                    _context.Carts.RemoveRange(cartItems);
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }     
                return View(order);  
            }
            return View(order);
        }

        // GET: Order/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        // POST: Order/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrId,CusId,OrTime,Address,Status")] Order order)
        {
            if (id != order.OrId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.OrId))
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
            return View(order);
        }

        // GET: Order/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .FirstOrDefaultAsync(m => m.OrId == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Order/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Orders == null)
            {
                return Problem("Entity set 'Db1670asmContext.Orders'  is null.");
            }
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
          return (_context.Orders?.Any(e => e.OrId == id)).GetValueOrDefault();
        }
    }
}
