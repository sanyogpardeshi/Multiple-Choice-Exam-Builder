using ExamApplication.Models;

namespace ExamApplication.Repository
{
    public interface IExamResult
    {
        void insertExamResult(ExamResult examResult);
        List<ExamResult> getExamResults();
        ExamResult getExamResultById(int id);
    }
}
