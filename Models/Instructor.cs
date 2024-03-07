namespace ExamApplication.Models
{
    public class Instructor : BaseUser
    {
        public List<Exam>? Exams { get; set; }
    }
}
