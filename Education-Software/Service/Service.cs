using Education_Software.Context;
using Education_Software.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Operations;
using System;
using System.Diagnostics;
using System.Linq;
using System.Net;

namespace Education_Software.Service
{

    public class Service
    {
        private readonly DatabaseContext _context;

        public Service(DatabaseContext context)
        {
            _context = context;
        }

        public List<SubjectModel> getSubjects()
        {
            var subjects = (from s in _context.subjects
                            select s).ToList();
            return subjects;
        }

        public SubjectModel getSubjectDetails(string title) {
            var subject = _context.subjects.FirstOrDefault(x=>x.title==title);
            return subject;
        
        }

        public SubjectModel getNextSubjectDetails(string title)
        {
            var subject = _context.subjects.ToList();
            var nextsubject = subject[0];
            //var ind = subject.IndexOf(x => x.title == title);
            //var nextsubject = subject.[ind];
            return nextsubject;

        }

        public SubjectModel RecordReading(string title)
        {
            var subject = _context.subjects.FirstOrDefault(x=>x.title==title);
            subject.reading += 1;
            _context.SaveChanges();
            return subject;
        }

        public List<QuestionModel> SubjectTest(SubjectModel subjectmodel)
        {
            var question = _context.questions.Where(x => x.sub_id == subjectmodel.sub_id).ToList();
            return question;
        }

        public List<QuestionModel> getAllQuestions()
        {
            List<QuestionModel> q = _context.questions.ToList();
            return q;
        }

        public List<QuestionModel> getRandomQuestions(List<QuestionModel> model, string test_type)
        {
            @Random r = new Random((int)DateTime.Now.Ticks);
            List<QuestionModel> quest = new List<QuestionModel>();
            if (test_type == "A")
            {
                var q1 = model.Where(x => x.q_type == "multiple_choice").OrderBy(h => r.Next()).Take(1).First();
                var q2 = model.Where(x => x.q_type == "true/false").OrderBy(h => r.Next()).Take(1).First();
                var q3 = model.Where(x => x.q_type == "completion").OrderBy(h => r.Next()).Take(1).First();
                //var q4 = model.Where(x => x.q_type == "matching").OrderBy(h => r.Next()).Take(1).First();
                quest.Add(q1);
                quest.Add(q2);
                quest.Add(q3);
                //quest.Add(q4);
            }
            else
            {
                var q1 = model.Where(x=> x.sub_id == "CS0001").OrderBy(h => r.Next()).Take(1).First();
                var q2 = model.Where(x => x.sub_id == "CS0001").OrderBy(h => r.Next()).Take(1).First();
                var q3 = model.Where(x => x.sub_id == "CS0001").OrderBy(h => r.Next()).Take(1).First();
                var q4 = model.Where(x => x.sub_id == "CS0001").OrderBy(h => r.Next()).Take(1).First();
                var q5 = model.Where(x => x.sub_id == "CS0002").OrderBy(h => r.Next()).Take(1).First();
                var q6 = model.Where(x => x.sub_id == "CS0002").OrderBy(h => r.Next()).Take(1).First();
                var q7 = model.Where(x => x.sub_id == "CS0002").OrderBy(h => r.Next()).Take(1).First();
                quest.Add(q1);
                quest.Add(q2);
                quest.Add(q3);
                quest.Add(q4);
                quest.Add(q5);
                quest.Add(q6);
                quest.Add(q7);
            }
            return quest;
        }
        
        public List<QuestionModel> SplitQuestions(List<QuestionModel> model, string test_type)
        {
            List<QuestionModel> questions = getRandomQuestions(model, test_type);
            //List<Tuple<string, string, string, List<string>>> q = new List<Tuple<string, string, string, List<string>>>();
            /*
            foreach (QuestionModel question in questions)
            {
                string body = "";
                List<string> possible_answers = new List<string>();
                if (question.q_type == "multiple_choice")
                {
                    string[] s = question.question.Split("•");
                    Debug.WriteLine(s);
                    body = s[0];
                    possible_answers = s.Skip(1).ToList();
                }
                else if (question.q_type == "true/false")
                {
                    string[] s = question.question.Split("•");
                    body = s[0];
                    possible_answers = s.Skip(1).ToList();
                }
                else if (question.q_type == "ordering")
                {
                    string[] s = question.question.Split("•");
                    body = s[0];
                    possible_answers = s.Skip(1).ToList();
                }
                else if (question.q_type == "completion")
                {
                    string[] s = question.question.Split("•");
                    body = s[0];
                    possible_answers = s.Skip(1).ToList();
                }
                else if (question.q_type == "matching")
                {
                    string[] s = question.question.Split("•");
                    body = s[0];
                    possible_answers = s.Skip(1).ToList();
                }

                Tuple<string, string, string, List<string>> el = new Tuple<string, string, string, List<string>>(question.q_id, question.q_type, body, possible_answers);
                q.Add(el);
            }*/
            return questions;
        }

        public List<bool> GetTestAnswers(string username, Dictionary<string,string> responses, string test_type)
        {
            List<bool> results = new List<bool>();
            bool result = false;
            TestModel test = new TestModel();
            test.test_id = Guid.NewGuid().ToString();
            foreach (var (key,value) in responses) 
            {
                string q_id = key;
                string answer = value;
                var question = _context.questions.FirstOrDefault(x => x.q_id == q_id);
                if (question.answer == answer)
                {
                        result = true;
                        results.Add(true);
                }
                else
                {
                        result = false;
                        results.Add(false);
                }
                test.username = username;
                test.q_id = q_id;
                test.test_type = test_type;
                test.score = result;
                _context.tests.Add(test);
                _context.SaveChanges();
            }
            return results;
        }

        public bool AddAnswer(TestModel test, string username, string q_id, string response, string test_type)
        {
            bool result = false;
            var question = _context.questions.FirstOrDefault(x => x.q_id == q_id);
            if (question.answer == response)
            {
                result = true;
            }
            else
            {
                result = false;
            }
            test.username = username;
            test.q_id = q_id;
            test.test_type = test_type;
            test.score = result;
            return result;
        }

        public GradesModel getGrades(string username, string sub_id)
        {
            GradesModel? grades = _context.grades.FirstOrDefault(x => x.username == username && x.sub_id == sub_id);
            if (grades == null)
            {
                grades = new GradesModel();
                grades.username = username;
                grades.sub_id = sub_id;
                grades.grade = null;
                _context.grades.Add(grades);
                _context.SaveChanges();
            }
            return grades;
        }

        public void AddGrades(List<GradesModel> model, List<string> grades)
        {
            for (int i= 0; i< model.Count; i++)
            {
                if (grades[i] == "None")
                {
                    //model[i].grade = null;
                    _context.grades.Remove(model[i]);
                    model[i].grade = null;
                    _context.Add(model[i]);
                    _context.SaveChanges();
                }
                else
                {
                    //model[i].grade = int.Parse(grades[i]);
                    _context.grades.Remove(model[i]);
                    model[i].grade = int.Parse(grades[i]);
                    _context.Add(model[i]);
                    _context.SaveChanges();
                }
            }
        }

        public List<QuestionnaireModel> getRecommendationQuestions()
        {
            List<QuestionnaireModel> q = (from s in _context.questionnaire
                                    select s).ToList();
            return q;
        }

        public RecommendationModel getRecommendations(string username, string q_id1, string answer1, string q_id2, string answer2, string q_id3, string answer3)
        {
            List<string> first = new List<string>();
            List<string> second = new List<string>();
            if(answer1 == "1")
            {
                first.Append(answer1);
            }
            else if (answer1 == "2")
            {
                second.Append(answer1);
            }
            if (answer2 == "1")
            {
                first.Append(answer2);
            }
            else if (answer2 == "2")
            {
                second.Append(answer2);
            }
            if (answer3 == "1")
            {
                first.Append(answer3);
            }
            else if (answer3 == "2")
            {
                second.Append(answer3);
            }

            List<GradesModel> grades = _context.grades.Where(x => x.username == username).ToList();
            Dictionary<string, int?> dict = new Dictionary<string, int?>(); 
            foreach (var gr in grades)
            {
                dict[gr.sub_id] = gr.grade;
            }
            string recommendation = "";
            if (first.Count > second.Count)
            {
                if (!dict.ElementAt(0).Value.HasValue)
                {
                    recommendation = "Your profile and preferences seem to associate you to front-end development, as you are more likely" +
                        "an artistic personality and like writing code in C#. You don't habve a grade yet in subjects like Human-Computer Interaction, but this doesn't prevent you to start thinking about your career." +
                        "Knowing all this, you can follow the Carreer Paths of UX Design, Web Development, Video Game Design or Software Engineering." +
                        "There are some fitting post-graduate programs available to you, like: " +
                        "\r\nMSc in Computer Science and Engineering - University of Piraeus" +
                        "\r\nMSc in Human-Computer Interaction - National and Kapodistrian University of Athens (NKUA)" +
                        "\r\nMSc in Game Design and Development - National and Kapodistrian University of Athens (NKUA)" +
                        "\r\nMSc in Information Systems - Athens University of Economics and Business (AUEB)" +
                        "\r\nMSc in Computer Science - University of Patras" +
                        "\r\nMSc in Multimedia and Graphics - National and Kapodistrian University of Athens (NKUA)";
                }
                else if (dict.ElementAt(0).Value > 6)
                {
                    recommendation = "Your profile and preferences seem to associate you to front-end development, as you are more likely" +
                        "an artistic personality and like writing code in C#. Your Grades in subjects like Human-Computer Interaction are good, and you might prefer that subject among others." +
                        "Knowing all this, you can follow the Carreer Paths of UX Design, Web Development, Video Game Design or Software Engineering." +
                        "There are some fitting post-graduate programs available to you, like: " +
                        "\r\nMSc in Computer Science and Engineering - University of Piraeus" +
                        "\r\nMSc in Human-Computer Interaction - National and Kapodistrian University of Athens (NKUA)" +
                        "\r\nMSc in Game Design and Development - National and Kapodistrian University of Athens (NKUA)" +
                        "\r\nMSc in Information Systems - Athens University of Economics and Business (AUEB)" +
                        "\r\nMSc in Computer Science - University of Patras" +
                        "\r\nMSc in Multimedia and Graphics - National and Kapodistrian University of Athens (NKUA)";
                }
                else
                {
                    recommendation = "Your profile and preferences seem to associate you to front-end development, as you are more likely" +
                        "an artistic personality and like writing code in C#. However, your Grades in subjects like Human-Computer Interaction are not that good, even though you might prefer that subject among others, but with more experience in the topic you can become an expert later on." +
                        "Knowing all this, you can follow the Carreer Paths of UX Design, Web Development, Video Game Design or Software Engineering." +
                        "There are some fitting post-graduate programs available to you, like: " +
                        "\r\nMSc in Computer Science and Engineering - University of Piraeus" +
                        "\r\nMSc in Human-Computer Interaction - National and Kapodistrian University of Athens (NKUA)" +
                        "\r\nMSc in Game Design and Development - National and Kapodistrian University of Athens (NKUA)" +
                        "\r\nMSc in Information Systems - Athens University of Economics and Business (AUEB)" +
                        "\r\nMSc in Computer Science - University of Patras" +
                        "\r\nMSc in Multimedia and Graphics - National and Kapodistrian University of Athens (NKUA)";
                }
            }
            else if (first.Count < second.Count)
            {
                if (!dict.ElementAt(1).Value.HasValue)
                {
                    recommendation = "Your profile and preferences seem to associate you to computer hardware field, as you are more likely" +
                        "a practical personality and like writing code in Assembly. You don't habve a grade yet in subjects like Computer Architecture, but this doesn't prevent you to start thinking about your career." +
                        "Knowing all this, you can follow the Carreer Paths of Hardware Engineering or IT Service." +
                        "There are some fitting post-graduate programs available to you, like:" +
                        "\r\nMSc in Electronic Computer Systems - University of Piraeus" +
                        "\r\nMSc in Computer Science and Engineering - University of Piraeus" +
                        "\r\nMSc in Electrical Engineering - National Technical University of Athens (NTUA)" +
                        "\r\nMSc in Electrical and Computer Engineering - Aristotle University of Thessaloniki" +
                        "\r\nMSc in Computer Engineering - University of Patras" +
                        "\r\nMSc in Information Systems and Services Engineering - Aristotle University of Thessaloniki";
                }
                else if (dict.ElementAt(1).Value > 7)
                {
                    recommendation = "Your profile and preferences seem to associate you to computer hardware field, as you are more likely" +
                        "a practical personality and like writing code in Assembly. Your Grades in subjects like Computer Architecture are good and you might prefer these subjects among others." +
                        "Knowing all this, you can follow the Carreer Paths of Hardware Engineering or IT Service." +
                        "There are some fitting post-graduate programs available to you, like:" +
                        "\r\nMSc in Electronic Computer Systems - University of Piraeus" +
                        "\r\nMSc in Computer Science and Engineering - University of Piraeus" +
                        "\r\nMSc in Electrical Engineering - National Technical University of Athens (NTUA)" +
                        "\r\nMSc in Electrical and Computer Engineering - Aristotle University of Thessaloniki" +
                        "\r\nMSc in Computer Engineering - University of Patras" +
                        "\r\nMSc in Information Systems and Services Engineering - Aristotle University of Thessaloniki";
                }
                else
                {
                    recommendation = "Your profile and preferences seem to associate you to computer hardware field, as you are more likely" +
                        "a practical personality and like writing code in Assembly. However, your Grades in subjects like Computer Architecture are not that good, even though you might prefer that subject among others, but with more experience in the topic you can become an expert later on." +
                        "Knowing all this, you can follow the Carreer Paths of Hardware Engineering or IT Service." +
                        "There are some fitting post-graduate programs available to you, like:" +
                        "\r\nMSc in Electronic Computer Systems - University of Piraeus" +
                        "\r\nMSc in Computer Science and Engineering - University of Piraeus" +
                        "\r\nMSc in Electrical Engineering - National Technical University of Athens (NTUA)" +
                        "\r\nMSc in Electrical and Computer Engineering - Aristotle University of Thessaloniki" +
                        "\r\nMSc in Computer Engineering - University of Patras" +
                        "\r\nMSc in Information Systems and Services Engineering - Aristotle University of Thessaloniki";
                }
            }
            RecommendationModel r = new RecommendationModel();
            r.username = username;
            r.recommendation = recommendation;
            return r;
        }

        public RecommendationModel getRecommendationModel(string username)
        {
            var r = _context.recommendations.FirstOrDefault(x => x.username == username);
            return r;
        }

        public bool Login(string username,string password)
        {
            var login = _context.users.FirstOrDefault(x => x.username == username && x.pass==password);
            if (login == null)
            {
                return false;
            }
            else
            {
                return true;
            }
            
        }


    }
}
