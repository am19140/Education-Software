using Education_Software.Context;
using Education_Software.Models;
using Education_Software.Service;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System.Diagnostics;

namespace Education_Software.Controllers
{
    public class TestController : Controller
    {
        private readonly ILogger<TestController> _logger;
        private readonly SubjectService _subjectService;


        public TestController(ILogger<HomeController> logger, SubjectService subjectService)
        {
            _logger = logger;
            _subjectService = subjectService;
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
            var subjectmodel = _subjectService.getSubjectDetails(subject);           
            return View("Subject", subjectmodel);
        }

        [HttpPost]
        public IActionResult ReadSubject(string username,string subject)
        {
            ViewBag.username = username;
            ViewBag.subject = subject;
            var subjectmodel = _subjectService.RecordReading(subject);
            return View("Subject",subjectmodel);

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