using Education_Software.Context;
using Education_Software.Models;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System.Diagnostics;

namespace Education_Software.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
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

            NpgsqlConnection connection = Database.Database.Connection();
            DatabaseContext databaseContext = new DatabaseContext();
            NpgsqlDataReader output = Database.Database.ExecuteQuery(string.Format("select *" +
                " from users where username ='{0}'", username), connection);
            if (output.Read())
            {
                string correct_pass = output.GetString(1);
                connection.Close();
                if (correct_pass == password)
                {
                    ViewBag.Username = loginModel.username;
                    return View("~/Views/Home/Homepage.cshtml", loginModel);
                }
            }
            loginModel.isLoginConfirmed = false;
            return View("Login", loginModel);

        }
        public IActionResult Subjects(string username) {
            ViewBag.username = username;
            List<SubjectModel> subjects = new List<SubjectModel>();
            NpgsqlConnection connection = Database.Database.Connection();
            NpgsqlDataReader output = Database.Database.ExecuteQuery("SELECT sub_id, " +
                "title,semester from subjects",connection);
            while (output.Read())
            {
                SubjectModel model = new SubjectModel();
                model.sub_id = output.GetString(0);
                model.title = output.GetString(1);
                model.semester = output.GetInt32(2);
                subjects.Add(model);


            }
            connection.Close();
            return View("Subjects",subjects);

        }

        public IActionResult Subject(string username,string subject)
        {
            ViewBag.username = username;
            ViewBag.subject = subject;
            SubjectModel subjectmodel = new SubjectModel();
            NpgsqlConnection connection = Database.Database.Connection();
            NpgsqlDataReader output = Database.Database.ExecuteQuery(string.Format("SELECT * " +
                "from subjects WHERE " +
                "title='{0}'",subject), connection);
            while (output.Read())
            {
                subjectmodel.sub_id = output.GetString(0);
                subjectmodel.title = output.GetString(1);
                subjectmodel.semester = output.GetInt32(2);
                subjectmodel.sub_type= output.GetString(3);
                subjectmodel.description= output.GetString(4);
                subjectmodel.description_reading = output.GetInt32(5);
                subjectmodel.learning_outcomes= output.GetString(6);
                subjectmodel.learning_outcomes_reading = output.GetInt32(7);
                subjectmodel.skills_acquired= output.GetString(8);
                subjectmodel.skills_acquired_reading = output.GetInt32(9);
                subjectmodel.specialization_link= output.GetString(10);
                subjectmodel.specialization_link_reading = output.GetInt32(11);






            }
            connection.Close();
            return View("Subject", subjectmodel);

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