using ExamApplication.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ExamApplication.ViewModel;

namespace ExamApplication.Data
{
    public class ExamAppDbContext : IdentityDbContext<ApplicationUser>
    {

        public ExamAppDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Exam> Exams { get; set; }
        public DbSet<ExamResult> ExamResults { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Option> Options { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<ExamApplication.ViewModel.UserViewModel>? UserViewModel { get; set; }

    }
}
