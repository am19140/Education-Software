using Education_Software.Context;
using Education_Software.Models;
using Education_Software.Service;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System.Diagnostics;
using System.Linq;

namespace Education_Software.Controllers
{
    public class TestController : Controller
    {
        // private readonly ILogger<TestController> _logger;
        private readonly SubjectService _subjectService;
        private readonly TestService _testService;


        public TestController(SubjectService subjectService, TestService testService)
        {
            //_logger = logger;
            _subjectService = subjectService;
            _testService = testService;
        }

        public IActionResult Index(string sub_id)
        {
            List<QuestionModel> questions = _testService.getRandomQuestions(sub_id);
            List<Tuple<string, string, List<string>, List<string>>> q = new List<Tuple<string, string, List<string>, List<string>>>();
            foreach (QuestionModel question in questions)
            {
                List<string> body = new List<string>();
                List<string> possible_answers = new List<string>();
                if (question.q_type == "multiple_choice")
                {
                    string[] s = question.question.Split("•");
                    body.Add(s[0]);
                    possible_answers = s.Skip(1).ToList();
                }
                else if (question.q_type == "true/false")
                {
                    possible_answers = question.question.Split("•").ToList();
                }
                else if (question.q_type == "ordering")
                {
                    possible_answers = question.question.Split("•").ToList();
                }
                else if (question.q_type == "completion")
                {

                }
                else if (question.q_type == "matching")
                {
                    string[] s = question.question.Split("•");
                    int half = s.Length / 2;
                    body = s.Where((e, i) => i < half).ToList();
                    possible_answers = s.Skip(half).ToList();
                }

                Tuple<string, string, List<string>, List<string>> el = new Tuple<string, string, List<string>, List<string>>(question.q_id, question.q_type, body, possible_answers);
                q.Add(el);
            }
            //ViewBag.username = username;
            //ViewBag.subject = subject;
            ViewBag.questions = q;
            return View("Test", q);
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }
}