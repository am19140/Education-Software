using Education_Software.Context;
using Education_Software.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Operations;
using System;
using System.Diagnostics;
using System.Linq;

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

        public List<QuestionModel> getRandomQuestions(List<QuestionModel> model)
        {
            @Random r = new Random((int)DateTime.Now.Ticks);
            List<QuestionModel> questions = model.OrderBy(h => r.Next()).Take(2).ToList();
            return questions;
        }
        
        public List<Tuple<string, string, string, List<string>>> SplitQuestions(List<QuestionModel> model)
        {
            List<QuestionModel> questions = getRandomQuestions(model);
            List<Tuple<string, string, string, List<string>>> q = new List<Tuple<string, string, string, List<string>>>();
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
            }
            return q;
        }

        public List<bool> GetTestAnswers(string username, List<Tuple<string,string>> responses, string test_type)
        {
            List<bool> results = new List<bool>();
            bool result = false;
            TestModel test = new TestModel();
            foreach (Tuple<string,string> response in responses) 
            {
                string q_id = response.Item1;
                string answer = response.Item2;
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
                test.test_id = Guid.NewGuid().ToString();
                test.q_id = q_id;
                test.test_type = test_type;
                test.score = result;
            }
            _context.tests.Add(test);
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

        public void AddGrades(string username, List<string> subjects, List<string> grades)
        {
            //foreach()
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
