using MVCWebAppHandsOn.Models;
using NuGet.Common;

namespace MVCWebAppHandsOn.Services
{
    public interface IStudentDetailService
    {
        Task<IEnumerable<Student>> GetAllStudents(string searchString);
        Task<Student> CreateStudent(Student student);
        Task<Student> FindStudent(int? id);
        Task<Student> UpdateStudent(int id, Student student);
        Task<Student> DeleteStudent(int id);
    }
}
