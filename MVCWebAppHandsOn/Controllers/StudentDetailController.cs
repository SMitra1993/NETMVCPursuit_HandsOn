using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCWebAppHandsOn.Data;
using MVCWebAppHandsOn.Models;
using MVCWebAppHandsOn.Services;

namespace MVCWebAppHandsOn.Controllers
{
    public class StudentDetailController : Controller
    {
        private readonly IStudentDetailService _service;

        public StudentDetailController(IStudentDetailService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index(string searchString)
        {
            ViewData["CurrentFilter"] = searchString;
            var student = await _service.GetAllStudents(searchString);
            return View(student);
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
                await _service.CreateStudent(student);
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var student = await _service.FindStudent(id);
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
                var result = await _service.UpdateStudent(id, student);
                if (result != null)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return NotFound();
                }
            }
            return View(student);
        }

        public async Task<IActionResult> Details(int? id)
        {
            var student = await _service.FindStudent(id);
            if(student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var student = await _service.FindStudent(id);
            if(student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _service.DeleteStudent(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
