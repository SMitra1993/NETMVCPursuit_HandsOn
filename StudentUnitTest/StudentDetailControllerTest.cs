using Microsoft.AspNetCore.Mvc;
using Moq;
using MVCWebAppHandsOn.Models;
using MVCWebAppHandsOn.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;
using System.Linq;
using System.Threading.Tasks;

namespace MVCWebAppHandsOn.Controllers
{
    public class StudentDetailControllerTests
    {
        private readonly Mock<IStudentDetailService> _mockStudentService;
        private readonly StudentDetailController _controller;

        public StudentDetailControllerTests()
        {
            _mockStudentService = new Mock<IStudentDetailService>();
            _controller = new StudentDetailController(_mockStudentService.Object);
        }

        [Fact]
        public async Task Index_ReturnsViewResult_WithSingleListOfStudent()
        {
            // Arrange
            var students = new List<Student> { new Student { 
                Id = 1, 
                FirstName = "Souvik", 
                LastName = "Mitra", 
                Email = "msouvik38@gmail.com", 
                Phone = "8013404464" } };
            _mockStudentService.Setup(service => service.GetAllStudents("")).ReturnsAsync(students);

            // Act
            var result = await _controller.Index("");

            // Assert
            var viewResult = Xunit.Assert.IsType<ViewResult>(result);
            var model = Xunit.Assert.IsAssignableFrom<IEnumerable<Student>>(viewResult.ViewData.Model);
            Xunit.Assert.Single(model);
        }

        [Fact]
        public async Task Index_ReturnsViewResult_WithSingleMultipleListOfStudents()
        {
            // Arrange
            var students = new List<Student> { new Student {
                Id = 1,
                FirstName = "Souvik",
                LastName = "Mitra",
                Email = "msouvik38@gmail.com",
                Phone = "8013404464" },  new Student {
                Id = 2,
                FirstName = "Shreya",
                LastName = "Mitra",
                Email = "shreya0111@gmail.com",
                Phone = "9330377332" } };
            _mockStudentService.Setup(service => service.GetAllStudents("")).ReturnsAsync(students);

            // Act
            var result = await _controller.Index("");

            // Assert
            var viewResult = Xunit.Assert.IsType<ViewResult>(result);
            var model = Xunit.Assert.IsAssignableFrom<IEnumerable<Student>>(viewResult.ViewData.Model);
            Xunit.Assert.Equal(students, model);
        }

        [Fact]
        public async Task Create_Post_ReturnsViewResult_WhenModelIsInvalid()
        {
            _controller.ModelState.AddModelError("FirstName", "Required");
            var student = new Student { LastName = "Mitra" };

            var result = await _controller.Create(student);

            var viewResult = Xunit.Assert.IsType<ViewResult>(result);
            var model = Xunit.Assert.IsAssignableFrom<Student>(viewResult.ViewData.Model);
            Xunit.Assert.Equal(student, model);
        }

        [Fact]
        public async Task Details_ReturnsNotFoundResult_WhenStudentNotFound()
        {
            // Arrange
            _mockStudentService.Setup(service => service.FindStudent(It.IsAny<int>())).ReturnsAsync((Student)null);

            // Act
            var result = await _controller.Details(1);

            // Assert
            Xunit.Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Edit_ReturnsNotFoundResult_WhenStudentNotFound()
        {
            // Arrange
            _mockStudentService.Setup(service => service.FindStudent(It.IsAny<int>())).ReturnsAsync((Student)null);

            // Act
            var result = await _controller.Edit(1);

            // Assert
            Xunit.Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Edit_Post_ReturnsNotFoundResult_WhenStudentNotFound()
        {
            var student = new Student
            {
                Id = 2,
                FirstName = "Shreya",
                LastName = "Mitra",
                Email = "shreya0111@gmail.com",
                Phone = "9330377332"
            };

            var result = await _controller.Edit(1, student);

            //var viewResult = Xunit.Assert.IsType<ViewResult>(result);
            //var model = Xunit.Assert.IsAssignableFrom<Student>(viewResult.ViewData.Model);
            //Xunit.Assert.Equal(student, model);
            Xunit.Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task DeleteConfirmed_ReturnsNotFound_WhenStudentDoesNotExist()
        {
            // Arrange
            int nonExistentStudentId = 999;
            _mockStudentService.Setup(service => service.FindStudent(nonExistentStudentId)).ReturnsAsync((Student)null);

            // Act
            var result = await _controller.Delete(nonExistentStudentId);

            // Assert
            Xunit.Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task DeleteConfirmed_DeletesStudent_AndRedirectsToIndex()
        {
            // Arrange
            int studentId = 1;
            var student = new Student { Id = studentId, FirstName = "John" };
            _mockStudentService.Setup(service => service.FindStudent(studentId)).ReturnsAsync(student);
            _mockStudentService.Setup(service => service.DeleteStudent(studentId)).ReturnsAsync(student);

            // Act
            var result = await _controller.DeleteConfirmed(studentId);

            // Assert
            Xunit.Assert.IsType<RedirectToActionResult>(result);
            var redirectResult = result as RedirectToActionResult;
            Xunit.Assert.Equal("Index", redirectResult.ActionName);
            _mockStudentService.Verify(service => service.DeleteStudent(studentId), Times.Once);
        }
    }
}
