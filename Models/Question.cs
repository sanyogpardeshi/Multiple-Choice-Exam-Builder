using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ExamApplication.Models
{
    public class Question
    {
        [Key]
        public int Id { get; set; }

        public int OptionCount { get; set; }

        public string? QuestionText { get; set; }

        [JsonIgnore]
        public Exam? Exam { get; set; }

        [JsonIgnore]
        public List<Option>? Options { get; set; }
    }
}
