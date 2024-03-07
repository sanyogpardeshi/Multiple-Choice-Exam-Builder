namespace ExamApplication.Models
{
    public class Student : BaseUser
    {
        public List<Exam>? Exams { get; set; }
        public List<ExamResult>? ExamResults { get; set; }

    }
}
