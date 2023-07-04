using Education_Software.Context;
using Education_Software.Models;
using Education_Software.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Npgsql;
using NuGet.Protocol;
using System.Diagnostics;
using System.Security.Policy;

namespace Education_Software.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly Service.Service _Service;


        public HomeController(ILogger<HomeController> logger, Service.Service Service)
        {
            _logger = logger;
            _Service = Service;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginModel loginModel)
        {

            string username = loginModel.username;
            var password = loginModel.password;
            var authentication = _Service.Login(username,password);
            if (authentication)
            {
                ViewBag.Username = loginModel.username;
                return View("Homepage", loginModel);
            }
            else
            {
                return View("Login");
            }
          
        }
        public IActionResult Subjects(string username) {
            ViewBag.username = username;

            var subjectmodel = _Service.getSubjects();
            Dictionary<string, int> scores = new Dictionary<string, int>();
            foreach(var subject in subjectmodel)
            {
                int score = _Service.getAverageScore(username, subject.sub_id);
                scores.Add(subject.sub_id, score);
            }
            ViewBag.scores = scores;
            return View("Subjects",subjectmodel);

        }

        public IActionResult Subject(string username,string subject)
        {
            if(subject == "Evaluation")
            {
                ViewBag.username = username;
                ViewBag.subject = subject;
                ViewBag.submitted = false;
                bool all_assessment_tests_done = _Service.evaluationCheck(username);
                if (all_assessment_tests_done)
                {
                    List<QuestionModel> q = _Service.getAllQuestions();
                    List<QuestionModel> questions = _Service.getRandomQuestions(q, "E");
                    return View("Evaluation", questions);
                }
                else
                {
                    return View("Evaluation", null);
                }
            }
            else
            {
                ViewBag.username = username;
                ViewBag.subject = subject;
                SubjectModel subjectmodel = _Service.getSubjectDetails(subject);
                return View("Subject", subjectmodel);
            }
        }

        public IActionResult NextSubject(string username, string subject)
        {
            ViewBag.username = username;
            SubjectModel subjectmodel = _Service.getNextSubjectDetails(subject);
            ViewBag.subject = subjectmodel.title;
            if (subjectmodel.sub_id != "CS1000")
            {
                return View("Subject", subjectmodel);
            }
            else
            {
                ViewBag.username = username;
                ViewBag.subject = subject;
                ViewBag.submitted = false;
                bool all_assessment_tests_done = _Service.evaluationCheck(username);
                if (all_assessment_tests_done)
                {
                    List<QuestionModel> q = _Service.getAllQuestions();
                    List<QuestionModel> questions = _Service.getRandomQuestions(q, "E");
                    return View("Evaluation", questions);
                }
                else
                {
                    return View("Evaluation", default);
                }
            }
        }

        [HttpPost]
        public IActionResult ReadSubject(string username,string subject)
        {
            ViewBag.username = username;
            ViewBag.subject = subject;
            SubjectModel subjectmodel = _Service.RecordReading(subject);
            List<QuestionModel> questionmodel = _Service.SubjectTest(subjectmodel);
            List<QuestionModel> questions = _Service.getRandomQuestions(questionmodel, "A");
            ViewBag.submitted = false;
            return View("Test", questions);
        }

        [HttpPost]
        public IActionResult SubmitTest(string username, string subject, string sub_id, string q_id1, string answer1, string q_id2, string answer2, string q_id3, string answer3, string q_id4, string answer4, string test_type)
        {
            Dictionary<string,string> responses = new Dictionary<string,string>();
            responses.Add(q_id1, answer1);
            responses.Add(q_id2, answer2);
            responses.Add(q_id3, answer3);
            responses.Add(q_id4, answer4);
            Dictionary<string,List<bool>> dict = _Service.GetTestAnswers(username, responses, test_type);
            string test_id = dict.Keys.First();
            List<bool> results = dict.Values.First();
            ViewBag.results = results;
            int percentage = _Service.UpdateProgress(username, subject, test_id, test_type, results);
            ViewBag.submitted = true;
            ViewBag.percentage = percentage;
            List<QuestionModel> model = _Service.getQuestions(responses.Keys.ToList());
            ViewBag.subject = subject;
            ViewBag.username = username;
            return View("Test", model);
        }

        public IActionResult EvaluationTest(string username)
        {
            ViewBag.username = username;
            ViewBag.submitted = false;
            bool all_assessment_tests_done = _Service.evaluationCheck(username);
            if(all_assessment_tests_done)
            {
                List<QuestionModel> q = _Service.getAllQuestions();
                List<QuestionModel> questions = _Service.getRandomQuestions(q, "E");
                return View("Evaluation", questions);
            }
            else
            {
                return View("Evaluation", default);
            }
        }

        public IActionResult SubmitEvaluationTest(string username, string subject, string q_id1, string answer1, string answerm1, string q_id2, string answer2, string q_id3, string answer3, string q_id4, string answer4, string q_id5, string answer5, string q_id6, string answer6, string q_id7, string answer7, string test_type)
        {
            ViewBag.username = username;
            Dictionary<string, string> responses = new Dictionary<string, string>();
            responses.Add(q_id1, answer1);
            responses.Add(q_id2, answer2);
            responses.Add(q_id3, answer3);
            responses.Add(q_id4, answer4);
            responses.Add(q_id5, answer5);
            responses.Add(q_id6, answer6);
            responses.Add(q_id7, answer7);
            Dictionary<string, List<bool>> dict = _Service.GetTestAnswers(username, responses, test_type);
            string test_id = dict.Keys.First();
            List<bool> results = dict.Values.First();
            ViewBag.results = results;
            int percentage = _Service.UpdateProgress(username, subject, test_id, test_type, results);
            ViewBag.submitted = true;
            ViewBag.percentage = percentage;
            List<QuestionModel> model = _Service.getQuestions(responses.Keys.ToList());
            ViewBag.subject = subject;
            return View("Evaluation", model);
        }

        public IActionResult Progress(string username)
        {
            ViewBag.username = username;
            ProgressViewModel model = _Service.getProgress(username);
            return View("Progress", model);

        }

        public IActionResult Grades(string username)
        {
            ViewBag.username = username;
            List<SubjectModel> subjectmodels = _Service.getSubjects();
            Debug.WriteLine(subjectmodels[0].title);
            Dictionary<string,string> subjects = new Dictionary<string,string>();
            var gradesmodels = new List<GradesModel>();
            foreach (var subject in subjectmodels)
            {
                subjects.Add(subject.sub_id, subject.title);
                GradesModel model = _Service.getGrades(username, subject.sub_id);
                Debug.WriteLine(username + subject.sub_id);
                gradesmodels.Add(model);               
            }
            ViewBag.subjects = subjects;
            Debug.WriteLine(gradesmodels);
            return View(gradesmodels);
        }

        [HttpPost]
        public IActionResult SubmitGrades(string grade1, string grade2, string username, string sub_id1, string sub_id2)
        {
            ViewBag.username = username;
            List<string> grades = new List<string>();
            grades.Add(grade1);
            grades.Add(grade2);
            _Service.AddGrades(username, sub_id1, sub_id2, grades);
            return View("Homepage");
        }

        public IActionResult Questionnaire(string username)
        {
            ViewBag.username = username;
            List<QuestionnaireModel> questionnairemodel = _Service.getRecommendationQuestions(username);
            return View("Questionnaire", questionnairemodel);
        }

        public IActionResult SubmitQuestionnaire(string username, string q_id1, string answer1, string q_id2, string answer2, string q_id3, string answer3)
        {
            ViewBag.username = username;
            RecommendationModel recommendation = _Service.getRecommendations(username, answer1, answer2, answer3);
            ViewBag.recommendation = true;
            return View("Recommendations", recommendation);
        }

        public IActionResult Recommendations(string username)
        {
            ViewBag.username = username;
            RecommendationModel r = _Service.getRecommendationModel(username);
            if(r == null)
            {
                ViewBag.recommendation = false;
            }
            else
            {
                ViewBag.recommendation = true;
            }
            return View("Recommendations", r);
        }

        public IActionResult Profile(string username)
        {
            ViewBag.username = username;
            var user = _Service.getInfo(username);
            return View("Profile",user);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Homepage(string username)
        {
            ViewBag.username = username;
            return View();
        }
        public IActionResult Registration()
        {
            return View("Registration");
        }

        [HttpPost]
        public IActionResult Registration(RegistrationModel registrationModel)
        {
            if (ModelState.IsValid)
            {
                _Service.RegisterUser(registrationModel);
                return View("Login");

            }
            else { return View("Registration"); }

            return View();
        }

        public string OpenModelPopup()
        {
            return "<h1>Help</h1>";
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }
}