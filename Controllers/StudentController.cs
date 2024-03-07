using ExamApplication.Models;
using ExamApplication.Repository;
using ExamApplication.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;

namespace ExamApplication.Controllers
{
    public class StudentController : Controller
    {
        public IStudent _student { get; set; }
        public IExam _exam { get; set; }
        public IQuestion _question { get; set; }

        public IExamResult _examResult { get; set; }         
        public StudentController(IStudent student, IExam exam, IQuestion question, IExamResult examResult)
        {
            _student = student;
            _exam = exam;
            _question = question;
            _examResult = examResult;
        }

        public IActionResult Index()
        {
            var student = (Student)_student.getStudentByEmail(User.Identity.Name);

            if (student == null)
            {
                return RedirectToAction("CreateProfile");
            }
            return View(student);
        }

        [HttpGet]
        public IActionResult CreateProfile()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateProfile(UserViewModel userViewModel)
        {
            if (ModelState.IsValid)
            {
                var student = new Student
                {
                    Address = userViewModel.Address,
                    City = userViewModel.City,
                    Department = userViewModel.Department,
                    Email = userViewModel.Email,
                    Exams = null,
                    Facebook = userViewModel.Facebook,
                    Gender = userViewModel.Gender,
                    Instagram = userViewModel.Instagram,
                    LinkedIn = userViewModel.LinkedIn,
                    Name = userViewModel.Name,
                    Phone = userViewModel.Phone,
                    Surname = userViewModel.Surname,
                    Twitter = userViewModel.Twitter,
                };
                _student.insertStudent(student);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult EditProfile(int studentId)
        {
            var student = _student.getStudentById(studentId);

            var model = new UserViewModel
            {
                Address = student.Address,
                City = student.City,
                Department = student.Department,
                Email = student.Email,
                Facebook = student.Facebook,
                Gender = student.Gender,
                Instagram = student.Instagram,
                LinkedIn = student.LinkedIn,
                Name = student.Name,
                Phone = student.Phone,
                Surname = student.Surname,
                Twitter = student.Twitter,
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult EditProfile(UserViewModel model)
        {
            var student = (Student)_student.getStudentByEmail(User.Identity.Name);


            student.Twitter = model.Twitter;
            student.Instagram = model.Instagram;
            student.Surname = model.Surname;
            student.Address = model.Address;
            student.City = model.City;
            student.Department = model.Department;
            student.Facebook = model.Facebook;
            student.Gender = model.Gender;
            student.Name = model.Name;
            student.Phone = model.Phone;
            student.LinkedIn = model.LinkedIn;

            _student.updateStudent(student);
            return RedirectToAction("Index");

        }

        [HttpGet]
        public IActionResult TakeExam()
        {
            var student = (Student)_student.getStudentByEmail(User.Identity.Name);
            var studentDepartment = student.Department;

            var exams = _exam.getExams();
            var filteredExams = exams.Where(exam => exam.ExamTopic.Equals(studentDepartment)).ToList();

            var takeExamViewModels = new List<TakeExamViewModel>();

            foreach (var exam in filteredExams)
            {
                takeExamViewModels.Add(new TakeExamViewModel
                {
                    Id = exam.id,
                    Name = exam.Name,
                    QuestionCount = exam.QuestionCount,
                    ExamTopic = exam.ExamTopic,
                    InstructorName = exam.Instructor.Name + exam.Instructor.Surname
                });
            }
            return View(takeExamViewModels);
        }

        [HttpPost]
        public IActionResult TakeExam(int id)
        {

            return RedirectToAction("ExamPaper", new { id = id });
        }

        [HttpGet]
        public IActionResult ExamPaper(int id)
        {

            var exam = _exam.getExamById(id);
            var examPaperViewModel = new ExamPaperViewModel
            {
                Name = exam.Name,
                Questions = exam.Questions
            };
            TempData["ExamName"] = exam.Name;
            return View(examPaperViewModel);
        }

        [HttpPost]
        public IActionResult ExamPaper(ExamPaperViewModel examPaperViewModel)
        {
            try
            {
                var selectedOptions = examPaperViewModel.SelectedOptions.Split("-");
                var student = _student.getStudentByEmail(User.Identity.Name);
                List<int> selectedIds = new List<int>();

                for (int i = 0; i < selectedOptions.Length; i++)
                {
                    selectedIds.Add(Convert.ToInt32(selectedOptions[i]));
                }
                var trueAnsweredOptions = new List<int>();
                var falseAnsweredOptions = new List<int>();
                var exam = _exam.getExamByName(TempData["ExamName"] as string);

                foreach (var question in exam.Questions)
                {
                    // Get the correct option id for the current question
                    var correctOptionId = question.Options.FirstOrDefault(o => o.AnswerOrNot)?.Id;

                    // Check if the selected option id matches the correct option id
                    if (selectedIds.Contains((int)correctOptionId))
                    {
                        trueAnsweredOptions.Add(question.Id);
                    }
                    else
                    {
                        falseAnsweredOptions.Add(question.Id);
                    }
                }

                ExamResult result = new ExamResult
                {
                    RightAnswer = trueAnsweredOptions.Count,
                    WrongAnswer = falseAnsweredOptions.Count,
                    Mark = (100 / (trueAnsweredOptions.Count + falseAnsweredOptions.Count) * trueAnsweredOptions.Count),
                    Student = student,
                    Exam = exam

                };
                _examResult.insertExamResult(result);
                student.ExamResults.Add(result);
                _student.updateStudent(student);
            }
            catch(Exception ex)
            {

            }
          
            return RedirectToAction("StudentResults");
        }

        public IActionResult StudentResults()
        {
            var student = _student.getStudentByEmail(User.Identity.Name);
            var studentExamResults = (List<ExamResult>)student.ExamResults;
            return View(studentExamResults);
        }
    }

}
