using ExamApplication.Data;
using ExamApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace ExamApplication.Repository
{
    public class ExamResultRepository : IExamResult
    {
        private readonly ExamAppDbContext _context;

        public ExamResultRepository(ExamAppDbContext context)
        {
            _context = context;
        }
        public ExamResult getExamResultById(int id)
        {
            return _context.ExamResults
                    .Include(examResult => examResult.Exam)
                    .ThenInclude(question => question.Questions)
                    .ThenInclude(options => options.Options)
                    .Include(examResult => examResult.Student)
                    .SingleOrDefault(examResult => examResult.Id == id);
        }

        public List<ExamResult> getExamResults()
        {
            return _context.ExamResults
                   .Include(examResult => examResult.Exam)
                   .ThenInclude(question => question.Questions)
                   .ThenInclude(options => options.Options)
                   .Include(examResult => examResult.Student)
                   .ToList();
        }

        public void insertExamResult(ExamResult examResult)
        {
            _context.ExamResults.Add(examResult);
            _context.SaveChanges();
        }
    }
}
