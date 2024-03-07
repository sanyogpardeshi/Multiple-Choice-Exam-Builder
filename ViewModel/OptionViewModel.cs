using ExamApplication.Models;
using System.ComponentModel.DataAnnotations;

namespace ExamApplication.ViewModel
{
    public class OptionViewModel
    {
        [Required(ErrorMessage = "Option Text is required.")]
        [Display(Name = "Option Text")]
        [DataType(DataType.Text)]
        public string OptionText { get; set; }

        public QuestionViewModel? QuestionViewModel { get; set; }

        public bool AnswerOrNot { get; set; }
    }
}
