using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCWebAppHandsOn.Data;
using MVCWebAppHandsOn.Models;

namespace MVCWebAppHandsOn.Controllers
{
    public class StudentDetailController : Controller
    {
        private readonly StudentContext _context;

        public StudentDetailController(StudentContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(string searchString)
        {
            ViewData["CurrentFilter"] = searchString;
            if (!String.IsNullOrEmpty(searchString))
            {
                return _context.Students != null ?
                         View(await _context.Students.Where(x => x.FirstName.Contains(searchString) || 
                         x.LastName.Contains(searchString) || x.Email.Contains(searchString)).ToListAsync()) :
                         Problem("Entity set 'StudentContext.Students'  is null.");
            }

            return _context.Students != null ?
                        View(await _context.Students.ToListAsync()) :
                        Problem("Entity set 'StudentContext.Students'  is null.");
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id, FirstName, LastName, Email, Phone")] Student student)
        {
            if(ModelState.IsValid)
            {
                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var student = await _context.Students.FindAsync(id);
            if(student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id, FirstName, LastName, Email, Phone")] Student student)
        {
            if(id != student.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if((_context.Students?.Any(x => x.Id == id)).GetValueOrDefault())
                    {
                        return NotFound();
                    } else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if(id == null  || _context.Students == null)
            {
                return NotFound();
            }

            var student = _context.Students.FirstOrDefault(x => x.Id == id);
            if(student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var student = await _context.Students.FirstOrDefaultAsync(x => x.Id == id);
            if(student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            if (_context.Students == null)
            {
                return Problem("Entity set 'StudentContext.Students'  is null.");
            }
            var student = await _context.Students.FindAsync(id);
            if (student != null)
            {
                _context.Students.Remove(student);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
