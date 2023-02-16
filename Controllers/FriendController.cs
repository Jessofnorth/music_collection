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
    public class FriendController : Controller
    {
        private readonly MusicContext _context;

        public FriendController(MusicContext context)
        {
            _context = context;
        }

        // GET: Friend
        public async Task<IActionResult> Index()
        {
              return _context.Friend != null ? 
                          View(await _context.Friend.ToListAsync()) :
                          Problem("Entity set 'MusicContext.Friend'  is null.");
        }

        // GET: Friend/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Friend == null)
            {
                return NotFound();
            }

            var friend = await _context.Friend
                .FirstOrDefaultAsync(m => m.FriendId == id);
            if (friend == null)
            {
                return NotFound();
            }

            return View(friend);
        }

        // GET: Friend/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Friend/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FriendId,FriendName,Email,Phonenumber")] Friend friend)
        {
            if (ModelState.IsValid)
            {
                _context.Add(friend);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(friend);
        }

        // GET: Friend/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Friend == null)
            {
                return NotFound();
            }

            var friend = await _context.Friend.FindAsync(id);
            if (friend == null)
            {
                return NotFound();
            }
            return View(friend);
        }

        // POST: Friend/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FriendId,FriendName,Email,Phonenumber")] Friend friend)
        {
            if (id != friend.FriendId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(friend);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FriendExists(friend.FriendId))
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
            return View(friend);
        }

        // GET: Friend/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Friend == null)
            {
                return NotFound();
            }

            var friend = await _context.Friend
                .FirstOrDefaultAsync(m => m.FriendId == id);
            if (friend == null)
            {
                return NotFound();
            }

            return View(friend);
        }

        // POST: Friend/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Friend == null)
            {
                return Problem("Entity set 'MusicContext.Friend'  is null.");
            }
            var friend = await _context.Friend.FindAsync(id);
            if (friend != null)
            {
                _context.Friend.Remove(friend);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FriendExists(int id)
        {
          return (_context.Friend?.Any(e => e.FriendId == id)).GetValueOrDefault();
        }
    }
}
