using System.ComponentModel.DataAnnotations;

namespace Task_2.Model
{
    public class Employee
    {
        [Required]
        public int Id { get; set; }

        [MinLength(3),MaxLength(30)]
        public string Name { get; set; }
        public string Department { get; set; }
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Mobile No must be 10 digits")]
        public string MobileNo { get; set; }
        [EmailAddress]
        public string Email { get; set; }
    }
}
