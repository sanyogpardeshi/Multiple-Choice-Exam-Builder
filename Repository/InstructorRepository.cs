using ExamApplication.Data;
using ExamApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace ExamApplication.Repository
{
    public class InstructorRepository : IInstructor
    {

        private readonly ExamAppDbContext _context;

        public InstructorRepository(ExamAppDbContext context) {
            _context = context;
        }   
        public void deleteInstructor(Instructor instructor)
        {
            _context.Instructors.Remove(instructor);
            _context.SaveChanges();
        }

        public Instructor getInstructorByEmail(string email)
        {
            var instructor = _context.Instructors.FirstOrDefault(s => s.Email == email);
            return instructor;
        }

        public Instructor getInstructorById(int id)
        {
            return _context.Instructors.Find(id);
        }

        public List<Instructor> getInstructors()
        {
            return _context.Instructors.Include(instructor=> instructor.Exams).ToList();
        }

        public void insertInstructor(Instructor instructor)
        {
            _context.Instructors.Add(instructor);
            _context.SaveChanges();
        }

        public void updateInstructor(Instructor instructor)
        {
            _context.Entry(instructor).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
