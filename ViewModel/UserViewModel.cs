using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ExamApplication.ViewModel 
{ 

    [Keyless]
    public class UserViewModel
    {
        [Required(ErrorMessage = "Name is required.")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Surname is required.")]
        [Display(Name = "Surname")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Department is required.")]
        [Display(Name = "Department")]
        public string Department { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Gender is required.")]
        [Display(Name = "Gender")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Phone is required.")]
        [Display(Name = "Phone")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        [Display(Name = "Address")]
        public string Address { get; set; }

        [Required(ErrorMessage = "City is required.")]
        [Display(Name = "City")]
        public string City { get; set; }

        public string? LinkedIn { get; set; }
        public string? Facebook { get; set; }
        public string? Twitter { get; set; }
        public string? Instagram { get; set; }
    }
}
