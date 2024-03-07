using ExamApplication.Models;
using ExamApplication.Repository;
using ExamApplication.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace ExamApplication.Controllers
{
    public class InstructorController : Controller
    {
        public IQuestion _question { get; set; }
        public IInstructor _instructor { get; set; }

        public IExam _exam { get; set; }
        public InstructorController(IQuestion question, IInstructor instructor, IExam exam)
        {
            _question = question;
            _instructor = instructor;
            _exam = exam;
        }
        public IActionResult Index()
        {
            var instructor = (Instructor)_instructor.getInstructorByEmail(User.Identity.Name);

            if (instructor == null)
            {
                return RedirectToAction("CreateProfile");
            }
            return View(instructor);
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
                var instructor = new Instructor
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
                _instructor.insertInstructor(instructor);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult EditProfile(int instructorId)
        {
            var instructor = _instructor.getInstructorById(instructorId);

            var model = new UserViewModel
            {
                Address = instructor.Address,
                City = instructor.City,
                Department = instructor.Department,
                Email = instructor.Email,
                Facebook = instructor.Facebook,
                Gender = instructor.Gender,
                Instagram = instructor.Instagram,
                LinkedIn = instructor.LinkedIn,
                Name = instructor.Name,
                Phone = instructor.Phone,
                Surname = instructor.Surname,
                Twitter = instructor.Twitter,
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult EditProfile(UserViewModel model)
        {
            var instructor = (Instructor)_instructor.getInstructorByEmail(User.Identity.Name);


            instructor.Twitter = model.Twitter;
            instructor.Instagram = model.Instagram;
            instructor.Surname = model.Surname;
            instructor.Address = model.Address;
            instructor.City = model.City;
            instructor.Department = model.Department;
            instructor.Facebook = model.Facebook;
            instructor.Gender = model.Gender;
            instructor.Name = model.Name;
            instructor.Phone = model.Phone;
            instructor.LinkedIn = model.LinkedIn;

            _instructor.updateInstructor(instructor);
            return RedirectToAction("Index");

        }

        [HttpGet]
        public IActionResult CreateQuestion()
        {
            var questionModel = new QuestionViewModel
            {
                OptionViewModels = new List<OptionViewModel>
                {
                    new OptionViewModel(),
                     new OptionViewModel(),
                      new OptionViewModel(),
                       new OptionViewModel(),
                        new OptionViewModel()
                }
            };
            return View(questionModel);
        }

        [HttpPost]
        public IActionResult CreateQuestion(QuestionViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var optionList = new List<Option>();
                    for (int i = 0; i < model.OptionViewModels.Count; i++)
                    {
                        var option = new Option
                        {
                            AnswerOrNot = model.OptionViewModels[i].AnswerOrNot,
                            OptionText = model.OptionViewModels[i].OptionText,
                            Question = null
                        };
                        optionList.Add(option);
                    }

                    var question = new Question
                    {
                        Options = optionList,
                        QuestionText = model.QuestionText,
                        OptionCount = optionList.Count,
                        Exam = null
                    };
                    _question.insertQuestion(question);
                    return RedirectToAction("CreateQuestion");
                }
            }catch (Exception ex)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }

            return RedirectToAction("CreateQuestion");
        }

        public IActionResult ListQuestions()
        {
            var questions = (List<Question>) _question.getQuestions();
            return View(questions);
        }

        public IActionResult ListExams()
        {
            var exams = (List<Exam>)_exam.getExams();
            return View(exams);
        }

        [HttpGet]
        public IActionResult CreateExam()
        {
            var questions = (List<Question>)_question.getQuestions();

            var examViewModel = new ExamViewModel
            {
                AllQuestions = questions
            };
            return View(examViewModel);
        }

        [HttpPost]
        public IActionResult CreateExam(ExamViewModel examViewModel)
        {
            if (ModelState.IsValid)
            {
                var questions = (string)examViewModel.SelectedQuestions;
                var questionIds = questions.Split("-");
                var instructor = _instructor.getInstructorByEmail(User.Identity.Name);

                Exam createdExam = new Exam();
                List<Question> questionList = new List<Question>();
                createdExam.Name = examViewModel.Name;
                createdExam.ExamTopic = examViewModel.ExamTopic;
                createdExam.CreateDate = DateTime.Now;
                createdExam.QuestionCount = questionIds.Length;
                createdExam.Instructor = instructor;

                for(int i = 0;i< questionIds.Length; i++)
                {
                    var question = _question.getQuestionById(Convert.ToInt32(questionIds.GetValue(i)));
                    questionList.Add(question);
                }
                createdExam.Questions = questionList;

                _exam.insertExam(createdExam);

                return RedirectToAction("Index");
            }

            examViewModel.AllQuestions = (List<Question>)_question.getQuestions();

            return View(examViewModel);
        }
    }
}
