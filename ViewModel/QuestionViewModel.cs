using ExamApplication.Models;
using System.ComponentModel.DataAnnotations;

namespace ExamApplication.ViewModel
{
    public class QuestionViewModel
    {
        public int? OptionCount { get; set; }

        [Required(ErrorMessage = "Question Text is required.")]
        [Display(Name ="Question Text")]
        [DataType(DataType.Text)]
        public string QuestionText { get; set; }

        public Exam? Exam { get; set; }

        public List<OptionViewModel>? OptionViewModels { get; set; }
    }
}
