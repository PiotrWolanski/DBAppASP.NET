// filepath: /c:/Users/pwola/OneDrive/Pulpit/AspNetUserManagement/Controllers/UserController.cs
using Microsoft.AspNetCore.Mvc;
using AspNetUserManagement.Models;
using AspNetUserManagement.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;

namespace AspNetUserManagement.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;
        
        public UserController(ApplicationDbContext context) => _context = context;

        public async Task<IActionResult> Index() => View(await _context.Users.ToListAsync());

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(User user)
        {
            if (!ModelState.IsValid) return View(user);

            if (_context.Users.Any(u => u.Email == user.Email))
            {
                ModelState.AddModelError("Email", "Email address already in use.");
                return View(user);
            }
            
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return NotFound();
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(User user)
        {
            if (!ModelState.IsValid) return View(user);

            if (_context.Users.Any(u => u.Email == user.Email && u.Id != user.Id))
            {
                ModelState.AddModelError("Email", "Email address already in use.");
                return View(user);
            }
            
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return NotFound();
            
            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return NotFound();
            
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index));
        }
    }
}