using ExamApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace ExamApplication.ViewModel
{
    [Keyless]
    public class ExamPaperViewModel
    {
        public List<Question> Questions { get; set; }
        public string Name { get; set; }

        public string? SelectedOptions { get; set; }

    }
}
