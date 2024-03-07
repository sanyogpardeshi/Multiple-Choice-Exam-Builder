using System.ComponentModel.DataAnnotations;

namespace ExamApplication.Models
{
    public class ExamResult
    {
        [Key]
        public int Id { get; set; }
        public int RightAnswer { get; set; }
        public int WrongAnswer { get; set; }
        public Exam Exam { get; set; }
        public Student Student { get; set; }
        public int Mark { get; set; }
    }
}
