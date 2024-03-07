using Microsoft.EntityFrameworkCore;

namespace ExamApplication.ViewModel
{
    [Keyless]
    public class TakeExamViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int QuestionCount { get; set; }
        public string ExamTopic { get; set; }
        public string InstructorName { get; set; }
    }
}
