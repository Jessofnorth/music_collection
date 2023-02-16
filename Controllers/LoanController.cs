using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using music_collection.Data;
using music_collection.Models;

namespace music_collection.Controllers
{
    public class LoanController : Controller
    {
        private readonly MusicContext _context;

        public LoanController(MusicContext context)
        {
            _context = context;
        }

        // GET: Loan
        public async Task<IActionResult> Index()
        {
            var musicContext = _context.Loan.Include(l => l.Album).Include(l => l.Friend);
            return View(await musicContext.ToListAsync());
        }

        // GET: Loan/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Loan == null)
            {
                return NotFound();
            }

            var loan = await _context.Loan
                .Include(l => l.Album)
                .Include(l => l.Friend)
                .FirstOrDefaultAsync(m => m.LoanId == id);
            if (loan == null)
            {
                return NotFound();
            }

            return View(loan);
        }

        // GET: Loan/Create
        public IActionResult Create()
        {
            ViewData["AlbumId"] = new SelectList(_context.Albums, "AlbumId", "AlbumName");
            ViewData["FriendId"] = new SelectList(_context.Friend, "FriendId", "Email");
            return View();
        }

        // POST: Loan/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LoanId,AlbumId,FriendId,LoanDate")] Loan loan)
        {
            if (ModelState.IsValid)
            {
                _context.Add(loan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AlbumId"] = new SelectList(_context.Albums, "AlbumId", "AlbumName", loan.AlbumId);
            ViewData["FriendId"] = new SelectList(_context.Friend, "FriendId", "Email", loan.FriendId);
            return View(loan);
        }

        // GET: Loan/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Loan == null)
            {
                return NotFound();
            }

            var loan = await _context.Loan.FindAsync(id);
            if (loan == null)
            {
                return NotFound();
            }
            ViewData["AlbumId"] = new SelectList(_context.Albums, "AlbumId", "AlbumName", loan.AlbumId);
            ViewData["FriendId"] = new SelectList(_context.Friend, "FriendId", "Email", loan.FriendId);
            return View(loan);
        }

        // POST: Loan/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LoanId,AlbumId,FriendId,LoanDate")] Loan loan)
        {
            if (id != loan.LoanId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(loan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LoanExists(loan.LoanId))
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
            ViewData["AlbumId"] = new SelectList(_context.Albums, "AlbumId", "AlbumName", loan.AlbumId);
            ViewData["FriendId"] = new SelectList(_context.Friend, "FriendId", "Email", loan.FriendId);
            return View(loan);
        }

        // GET: Loan/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Loan == null)
            {
                return NotFound();
            }

            var loan = await _context.Loan
                .Include(l => l.Album)
                .Include(l => l.Friend)
                .FirstOrDefaultAsync(m => m.LoanId == id);
            if (loan == null)
            {
                return NotFound();
            }

            return View(loan);
        }

        // POST: Loan/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Loan == null)
            {
                return Problem("Entity set 'MusicContext.Loan'  is null.");
            }
            var loan = await _context.Loan.FindAsync(id);
            if (loan != null)
            {
                _context.Loan.Remove(loan);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LoanExists(int id)
        {
          return (_context.Loan?.Any(e => e.LoanId == id)).GetValueOrDefault();
        }
    }
}
