using ExamApplication.Models;

namespace ExamApplication.Repository
{
    public interface IQuestion
    {
        void insertQuestion(Question question);
        void updateQuestion(Question question);
        void deleteQuestion(Question question); 
        List<Question> getQuestions();   
        Question getQuestionById(int id);
    }
}
