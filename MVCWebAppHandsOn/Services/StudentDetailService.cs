using MVCWebAppHandsOn.Data;
using MVCWebAppHandsOn.Models;
using Microsoft.EntityFrameworkCore;
using NuGet.Common;

namespace MVCWebAppHandsOn.Services
{
    public class StudentDetailService: IStudentDetailService
    {
        private readonly StudentContext _context;

        public StudentDetailService(StudentContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Student>> GetAllStudents(string searchString)
        {
            if (!String.IsNullOrEmpty(searchString))
            {
                return await _context.Students.Where(x => x.FirstName.Contains(searchString) ||
                         x.LastName.Contains(searchString) || x.Email.Contains(searchString)).AsNoTracking().ToListAsync();
            }
            var student = await _context.Students.AsNoTracking().ToListAsync();
            return student;
        }

        public async Task<Student> CreateStudent(Student student)
        {
            _context.Add(student);
            await _context.SaveChangesAsync();
            return student;
        }

        public async Task<Student> FindStudent(int? id)
        {
            return await _context.Students.FindAsync(id);
        }

        public async Task<Student> UpdateStudent(int id, Student student)
        {
            try
            {
                _context.Update(student);
                await _context.SaveChangesAsync();
                return student;
            }
            catch (DbUpdateConcurrencyException)
            {
                if ((_context.Students?.Any(x => x.Id == id)).GetValueOrDefault())
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task<Student> DeleteStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student != null)
            {
                _context.Students.Remove(student);
                await _context.SaveChangesAsync();
                return student;
            } else { return null; }
        }
    }
}
