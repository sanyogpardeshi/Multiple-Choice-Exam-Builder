using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ExamApplication.Models
{
    public class Exam
    {
        [Key]
        public int id  { get; set; }
        public string Name { get; set; }
        public int QuestionCount { get; set; }
        public string ExamTopic { get; set; }

        [JsonIgnore]
        public List<Question> Questions { get; set; }

        public List<Student>? Students { get; set; }
        public List<ExamResult>? ExamResults { get; set; }
        public Instructor Instructor { get; set; }
        public DateTime CreateDate { get; set; }

    }
}
