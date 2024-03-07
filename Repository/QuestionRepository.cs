using ExamApplication.Data;
using ExamApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace ExamApplication.Repository
{
    public class QuestionRepository : IQuestion
    {

        private readonly ExamAppDbContext _context;

        public QuestionRepository(ExamAppDbContext _context)
        {
            this._context = _context;
        }
        public void deleteQuestion(Question question)
        {
           _context.Questions.Remove(question);
           _context.SaveChanges();      
        }

        public Question getQuestionById(int id)
        {
            return _context.Questions.Include(question => question.Options).SingleOrDefault(question => question.Id == id);
        }
        public List<Question> getQuestions()
        {

            return _context.Questions.Include(question => question.Options).Include(question => question.Exam).ToList();

        }

        public void insertQuestion(Question question)
        {

                _context.Questions.Add(question);
                _context.SaveChanges();
            
        }

        public void updateQuestion(Question question)
        {

                _context.Entry(question).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _context.SaveChanges();
            
        }
    }
}
