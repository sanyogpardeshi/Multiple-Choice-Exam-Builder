using ExamApplication.Data;
using ExamApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace ExamApplication.Repository
{
    public class ExamRepository : IExam
    {
        private readonly ExamAppDbContext _context;

        public ExamRepository(ExamAppDbContext context)
        {
            _context = context;
        }
        public void deleteExam(Exam exam)
        {
            _context.Exams.Remove(exam);
            _context.SaveChanges();
        }

        public Exam getExamById(int id)
        {
            return _context.Exams
                    .Include(exam => exam.Instructor)
                    .Include(exam => exam.Students)
                    .Include(exam => exam.Questions)
                    .ThenInclude(question => question.Options)
                    .SingleOrDefault(exam => exam.id == id);
        }

        public Exam getExamByName(string name)
        {
            var exam = _context.Exams
                    .Include(exam => exam.Instructor)
                    .Include(exam => exam.Students)
                    .Include(exam => exam.Questions)
                    .ThenInclude(question => question.Options)
                    .SingleOrDefault(exam => exam.Name == name);
            return exam;
        }

        public List<Exam> getExams()
        {
            return _context.Exams.Include(exam => exam.Instructor).Include(exam=> exam.Students).Include(exam => exam.Questions).ToList();
        }

        public void insertExam(Exam exam)
        {
            _context.Exams.Add(exam);
            _context.SaveChanges();
        }

        public void updateExam(Exam exam)
        {
            _context.Entry(exam).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
