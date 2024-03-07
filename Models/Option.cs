using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ExamApplication.Models
{
    public class Option
    {
        [Key]
        public int Id { get; set; }

        public string? OptionText { get; set; }

        [JsonIgnore]
        public Question? Question { get; set; }

        public bool AnswerOrNot { get; set; }

    }
}
