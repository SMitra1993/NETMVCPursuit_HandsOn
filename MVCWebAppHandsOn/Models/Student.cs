using System.ComponentModel.DataAnnotations;

namespace MVCWebAppHandsOn.Models
{
    public class Student
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "First Name is required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Email Id is required")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Phone is required")]
        public string Phone { get; set; }
    }
}
