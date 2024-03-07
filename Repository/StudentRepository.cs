using ExamApplication.Data;
using ExamApplication.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace ExamApplication.Repository
{
    public class StudentRepository : IStudent
    {
        private readonly ExamAppDbContext _context;

        public StudentRepository(ExamAppDbContext context) {
            _context = context;
        }
        public void deleteStudent(Student student)
        {
            _context.Students.Remove(student);
            _context.SaveChanges();
        }

        public Student getStudentByEmail(string email)
        {
            var student = _context.Students.Include(student => student.ExamResults).ThenInclude(examResult=> examResult.Exam).FirstOrDefault(s => s.Email == email);
            return student;
        }

        public Student getStudentById(int id)
        {
            return _context.Students.Find(id);
        }

        public List<Student> getStudents()
        {
            return _context.Students.Include(student=> student.ExamResults).Include(student=> student.Exams).ToList();
        }

        public void insertStudent(Student student)
        {
            _context.Students.Add(student);
            _context.SaveChanges();
        }

        public void updateStudent(Student student)
        {
            _context.Entry(student).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
