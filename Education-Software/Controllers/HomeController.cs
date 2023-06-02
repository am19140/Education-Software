using Education_Software.Context;
using Education_Software.Models;
using Education_Software.Service;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System.Diagnostics;

namespace Education_Software.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SubjectService _subjectService;
        private readonly TestService _testService;


        public HomeController(ILogger<HomeController> logger, SubjectService subjectService, TestService testService)
        {
            _logger = logger;
            _subjectService = subjectService;
            _testService = testService;
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
            var authentication = _subjectService.Login(username,password);
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

            var subjectmodel = _subjectService.getSubjects();            
            return View("Subjects",subjectmodel);

        }

        public IActionResult Subject(string username,string subject)
        {
            ViewBag.username = username;
            ViewBag.subject = subject;
            SubjectModel subjectmodel = _subjectService.getSubjectDetails(subject);           
            return View("Subject", subjectmodel); 
        }

        [HttpPost]
        public IActionResult ReadSubject(string username,string subject)
        {
            ViewBag.username = username;
            ViewBag.subject = subject;
            SubjectModel subjectmodel = _subjectService.RecordReading(subject);
            ViewBag.id = subjectmodel.sub_id;
            List<QuestionModel> questionmodel= _testService.SubjectTest(subjectmodel);
            return View("Test", questionmodel);

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
            //_progressService will be like _subjectService but will retrieve the students progress to each subject and present it in the progress page
            //var progressmodel = _subjectService.getSubjects();
            return View();

        }

        public IActionResult Recommendations(string username)
        {
            ViewBag.username = username;
            //_gradeService will be like _subjectService but will retrieve the students grades to continue in the recommendations 
            // var recommendationsmodel = _gradeService.getGrades();
            return View();

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