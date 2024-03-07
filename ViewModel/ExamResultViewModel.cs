using ExamApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace ExamApplication.ViewModel
{
    [Keyless]
    public class ExamResultViewModel
    {
        public int RightAnswer { get; set; }
        public int WrongAnswer { get; set; }
        public string ExamName { get; set; }
        public DateTime ExamDate { get; set; }
        public int Mark { get; set; }
    }
}
