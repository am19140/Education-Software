using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Education_Software.Context;
using Education_Software.Models;

namespace Education_Software
{
    public class UserModelsController : Controller
    {
        private readonly DatabaseContext _context;

        public UserModelsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: UserModels
        public async Task<IActionResult> Index()
        {
              return _context.users != null ? 
                          View(await _context.users.ToListAsync()) :
                          Problem("Entity set 'DatabaseContext.users'  is null.");
        }

        // GET: UserModels/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.users == null)
            {
                return NotFound();
            }

            var userModel = await _context.users
                .FirstOrDefaultAsync(m => m.username == id);
            if (userModel == null)
            {
                return NotFound();
            }

            return View(userModel);
        }

        // GET: UserModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UserModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("username,pass,first_name,last_name,student_state,gender,email,phone_number")] UserModel userModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(userModel);
        }

        // GET: UserModels/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.users == null)
            {
                return NotFound();
            }

            var userModel = await _context.users.FindAsync(id);
            if (userModel == null)
            {
                return NotFound();
            }
            return View(userModel);
        }

        // POST: UserModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("username,pass,first_name,last_name,student_state,gender,email,phone_number")] UserModel userModel)
        {
            if (id != userModel.username)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserModelExists(userModel.username))
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
            return View(userModel);
        }

        // GET: UserModels/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.users == null)
            {
                return NotFound();
            }

            var userModel = await _context.users
                .FirstOrDefaultAsync(m => m.username == id);
            if (userModel == null)
            {
                return NotFound();
            }

            return View(userModel);
        }

        // POST: UserModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.users == null)
            {
                return Problem("Entity set 'DatabaseContext.users'  is null.");
            }
            var userModel = await _context.users.FindAsync(id);
            if (userModel != null)
            {
                _context.users.Remove(userModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserModelExists(string id)
        {
          return (_context.users?.Any(e => e.username == id)).GetValueOrDefault();
        }
    }
}
