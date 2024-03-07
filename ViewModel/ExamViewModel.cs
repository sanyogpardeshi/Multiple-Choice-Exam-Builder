using ExamApplication.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ExamApplication.ViewModel
{
    [Keyless]
    public class ExamViewModel
    {
        public ExamViewModel()
        {
            AllQuestions = new List<Question>();
            SelectedQuestions = "";
        }
        [Display(Name = "Exam Name")]
        public string Name { get; set; }

        [Display(Name = "Exam Department")]
        public string ExamTopic { get; set; }
        public string SelectedQuestions { get; set; }
        public List<Question>? AllQuestions { get; set; }

    }

}
