using ExamApplication.Models;

namespace ExamApplication.Repository
{
    public interface IExam
    {
        void insertExam(Exam exam);
        void updateExam(Exam exam);
        void deleteExam(Exam exam);
        List<Exam> getExams();
        Exam getExamById(int id);
        Exam getExamByName(String name);
    }
}
