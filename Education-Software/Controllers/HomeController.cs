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
            return View("Subjects",subjectmodel);

        }

        public IActionResult Subject(string username,string subject)
        {
            ViewBag.username = username;
            ViewBag.subject = subject;
            SubjectModel subjectmodel = _Service.getSubjectDetails(subject);           
            return View("Subject", subjectmodel); 
        }

        public IActionResult NextSubject(string username,string subject)
        {
            ViewBag.username = username;
            ViewBag.subject = subject;
            SubjectModel subjectmodel = _Service.getNextSubjectDetails(subject);           
            return View("Subject", subjectmodel); 
        }

        [HttpPost]
        public IActionResult ReadSubject(string username,string subject)
        {
            TempData["Username"] = username;
            TempData["Subject"] = subject;
            SubjectModel subjectmodel = _Service.RecordReading(subject);
            TempData["Id"] = subjectmodel.sub_id;
            List<QuestionModel> questionmodel = _Service.SubjectTest(subjectmodel);
            List<Tuple<string, string, string, List<string>>> questions = _Service.SplitQuestions(questionmodel);
            //TempData["Questions"] = questions;
            ViewBag.questions = questions;
            TempData["Submitted"] = false;
            TestModel testmodel = new TestModel();
            testmodel.test_id = Guid.NewGuid().ToString();
            return View("Test", testmodel);
        }

        //[HttpPost]
        public IActionResult SubmitTest(string username, List<Tuple<string,string>> resp, string test_type)
        {
            List<Tuple<string,string>> responses = resp;
            List<bool> results = _Service.GetTestAnswers(username, responses, test_type);
            int count = results.Count;
            List<bool> correct = results.Where(x => x.Equals(true)).ToList();
            int corr = correct.Count;
            int percentage = (corr/count)*100;
            TempData["Submitted"] = true;
            TempData["Percentage"] = percentage;
            return View("Test");
        }

        public IActionResult SubmitSimpleQuestion(TestModel test, string username, string q_id , string response, string test_type, bool last = false)
        {
            if(last)
            {
                bool result = _Service.AddAnswer(test, username, q_id, response, test_type);
                TempData["Result"] = result;
                return View("Test");

            }
            else
            {
                bool result = _Service.AddAnswer(test, username, q_id, response, test_type);
                TempData["Result"] = result;
                return View("Test");
            }
        }

        public IActionResult EvaluationTest(string username)
        {
            return View("Evaluation");
        }

        public IActionResult Progress(string username)
        {
            ViewBag.username = username;
            //_progressService will be like _subjectService but will retrieve the students progress to each subject and present it in the progress page
            //var progressmodel = _subjectService.getSubjects();
            return View();

        }

        public IActionResult Grades(string username)
        {
            ViewBag.username = username;
            List<SubjectModel> subjectmodels = _Service.getSubjects();
            Dictionary<string,string> subjects = new Dictionary<string,string>();
            List<GradesModel> gradesmodels = new List<GradesModel>();
            foreach (var subject in subjectmodels)
            {
                subjects.Add(subject.sub_id, subject.title);
                GradesModel model = new GradesModel();
                model.username = username;
                model.sub_id = subject.sub_id;
                model.grade = null;
                gradesmodels.Add(model);               
            }
            ViewBag.subjects = subjects;
            return View("Grades", gradesmodels);
        }

        public IActionResult SubmitGrades(string username, List<string> subjects, List<string> grades)
        {
            ViewBag.username = username;
            //GradesModel gradesmodel = _Service.addGrades(username, subjects, grades);
            return View("Grades");
        }

        public IActionResult Questionnaire(string username)
        {
            ViewBag.username = username;
            List<QuestionnaireModel> questionnairemodel = _Service.getRecommendationQuestions();
            return View("Questionnaire", questionnairemodel);
        }

        public IActionResult SubmitQuestionnaire(string username, string q_id1, string answer1, string q_id2, string answer2, string q_id3, string answer3)
        {
            RecommendationModel recommendation = _Service.getRecommendations(username, q_id1, answer1, q_id2, answer2, q_id3, answer3);
            return View("Recommendations", recommendation);
        }

        public IActionResult Recommendation(string username)
        {
            RecommendationModel r = _Service.getRecommendationModel(username);
            if(r.recommendation == null)
            {
                ViewBag.recommendation = false;
            }
            else
            {
                ViewBag.recommendation = true;
            }
            return View("Recommendation", r);
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }
}