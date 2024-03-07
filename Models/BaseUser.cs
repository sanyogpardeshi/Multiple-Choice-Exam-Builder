using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ExamApplication.Models
{
    public class BaseUser
    {
        [Key]    
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Department { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }

        public string Phone { get; set; }
        public string Address { get; set; }
        public string City { get; set; }

        public string? LinkedIn { get; set; }
        public string? Facebook { get; set; }
        public string? Twitter { get; set; }
        public string? Instagram { get; set; }
    }
}
