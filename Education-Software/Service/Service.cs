using Education_Software.Context;
using Education_Software.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.CodeAnalysis.Operations;
using System;
using System.Diagnostics;
using System.Drawing.Printing;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Numerics;

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
                var q4 = model.Where(x => x.q_type == "matching").OrderBy(h => r.Next()).Take(1).First();
                quest.Add(q1);
                quest.Add(q2);
                quest.Add(q3);
                quest.Add(q4);
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

        public Dictionary<string,List<bool>> GetTestAnswers(string username, Dictionary<string,string> responses, string test_type)
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
            Dictionary<string, List<bool>> d = new Dictionary<string, List<bool>>();
            d.Add(test.test_id, results);
            return d;
        }

        public List<QuestionModel> getQuestions(string q_id1, string q_id2, string q_id3, string q_id4)
        {
            List<QuestionModel> model = new List<QuestionModel>();
            var m1 = _context.questions.First(x => x.q_id == q_id1);
            var m2 = _context.questions.First(x => x.q_id == q_id2);
            var m3 = _context.questions.First(x => x.q_id == q_id3);
            var m4 = _context.questions.First(x => x.q_id == q_id4);
            model.Add(m1);
            model.Add(m2);
            model.Add(m3);
            model.Add(m4);
            return model;
        }

        public int UpdateProgress(string username, string subject, string test_id, string test_type, List<bool> results)
        {
            string sub_id = _context.subjects.Where(x => x.title == subject).First().sub_id;

            int count = results.Count;
            List<bool> correct = results.Where(x => x.Equals(true)).ToList();
            int corr = correct.Count;
            double perc = (double)corr / (double)count * 100;
            int percentage = (int)Math.Floor(perc);
            
            ProgressModel progress = new ProgressModel();
            progress.username = username;
            progress.sub_id = sub_id;
            progress.test_id = test_id;
            progress.test_type = test_type;
            progress.score = percentage; 
            progress.time = DateTime.Now.ToString();
            _context.progress.Add(progress);
            _context.SaveChanges();
            return percentage;
        }
        
        public StatisticsModel UpdateStatistics(string username)
        {
            List<TestModel> t = _context.tests.Where(x => x.username == username).ToList();
            List<QuestionModel> q = getAllQuestions();
            var join = from question in q 
                       join test in t 
                       on question.q_id equals test.q_id
                       select new
                       {
                           test.username,
                           test.q_id,
                           test.score,
                           question.q_type,
                           question.chapter
                       };
            var temp = join.Where(x => x.score == true);

            int correct_description = temp.Where(x => x.chapter == "description").ToList().Count;
            int count_description = join.Where(x => x.chapter == "description").ToList().Count;
            double? description_score = (count_description == 0) ? -1.0 : (correct_description / count_description) * 100;

            int correct_learning_outcomes = temp.Where(x => x.chapter == "learning_outcomes").ToList().Count;
            int count_learning_outcomes = join.Where(x => x.chapter == "learning_outcomes").ToList().Count;
            double? learning_outcomes_score = (count_learning_outcomes == 0) ? -1.0 : (correct_learning_outcomes / count_learning_outcomes) * 100;

            int correct_skills = temp.Where(x => x.chapter == "skills_acquired").ToList().Count;
            int count_skills = join.Where(x => x.chapter == "skills_acquired").ToList().Count;
            double? skills_score = (count_skills == 0) ? -1.0 : (correct_skills / count_skills) * 100;

            int correct_specialization = temp.Where(x => x.chapter == "specialization_link").ToList().Count;
            int count_specialization = join.Where(x => x.chapter == "specialization_link").ToList().Count;
            double? specialization_score = (count_specialization == 0) ? -1.0 : (correct_specialization / count_specialization) * 100;

            int correct_multiple_choice = temp.Where(x => x.q_type == "multiple_choice").ToList().Count;
            int count_multiple_choice = join.Where(x => x.q_type == "multiple_choice").ToList().Count;
            double? multiple_choice_score = (count_multiple_choice == 0) ? -1.0 : (correct_multiple_choice / count_multiple_choice) * 100;

            int correct_true_false = temp.Where(x => x.q_type == "true/false").ToList().Count;
            int count_true_false = join.Where(x => x.q_type == "true/false").ToList().Count;
            double? true_false_score = (count_true_false == 0) ? -1.0 : (correct_true_false / count_true_false) * 100;

            int correct_completion = temp.Where(x => x.q_type == "completion").ToList().Count;
            int count_completion = join.Where(x => x.q_type == "completion").ToList().Count;
            double? completion_score = (count_completion == 0) ? -1.0 : (correct_completion / count_completion) * 100;

            int correct_matching = temp.Where(x => x.q_type == "matching").ToList().Count;
            int count_matching = join.Where(x => x.q_type == "matching").ToList().Count;
            double? matching_score = (count_matching == 0) ? -1.0 : (correct_matching / count_matching) * 100;
            
            StatisticsModel s = new StatisticsModel();
            s.username = username;
            s.description_score = (int?) Math.Floor((decimal)description_score);
            s.learning_outcomes_score = (int?)Math.Floor((decimal)learning_outcomes_score);
            s.skills_acquired_score = (int?)Math.Floor((decimal)skills_score);
            s.specialization_link_score = (int?)Math.Floor((decimal)specialization_score);
            s.multiple_choice_score = (int?)Math.Floor((decimal)multiple_choice_score);
            s.true_false_score = (int?)Math.Floor((decimal)true_false_score);
            s.completion_score = (int?)Math.Floor((decimal)completion_score);
            s.matching_score = (int?)Math.Floor((decimal)matching_score);
            return s;
        }

        public ProgressViewModel getProgress(string username)
        {
            StatisticsModel stats = UpdateStatistics(username);
            List<ProgressModel> progress = _context.progress.Where(x => x.username == username).ToList();
            ProgressViewModel model = new ProgressViewModel();
            model.progress_table = progress;
            model.statistics_table = stats;
            return model;
        }

        public GradesModel getGrades(string username, string sub_id)
        {
            GradesModel? grades = _context.grades.FirstOrDefault(x => x.username == username && x.sub_id == sub_id);
            if (grades == default)
            {
                var grade = new GradesModel();
                grade.username = username;
                grade.sub_id = sub_id;
                grade.grade = null;
                _context.grades.Add(grade);
                _context.SaveChanges();
                grades = grade;
            }
            return grades;
        }

        public void AddGrades(string username, string sub_id1, string sub_id2, List<string> grades)
        {
            if (grades[0] == "None")
            {
                var model = _context.grades.FirstOrDefault(x => x.username == username && x.sub_id == sub_id1); 
                _context.grades.Remove(model);
                _context.SaveChanges();
                model.grade = null;
                _context.grades.Add(model);
                _context.SaveChanges();
            }
            else
            {
                var model = _context.grades.FirstOrDefault(x => x.username == username && x.sub_id == sub_id1);
                _context.grades.Remove(model);
                _context.SaveChanges();
                model.grade = Int32.Parse(grades[0]);
                _context.grades.Add(model);
                _context.SaveChanges();
            }
            if (grades[1] == "None")
            {
                var model = _context.grades.FirstOrDefault(x => x.username == username && x.sub_id == sub_id2);
                _context.grades.Remove(model);
                _context.SaveChanges();
                model.grade = null;
                _context.grades.Add(model);
                _context.SaveChanges();
            }
            else
            {
                var model = _context.grades.FirstOrDefault(x => x.username == username && x.sub_id == sub_id2);
                _context.grades.Remove(model);
                _context.SaveChanges();
                model.grade = Int32.Parse(grades[1]);
                _context.grades.Add(model);
                _context.SaveChanges();
            }
        }

        public List<QuestionnaireModel> getRecommendationQuestions()
        {
            List<QuestionnaireModel> q = (from s in _context.questionnaire
                                    select s).ToList();
            return q;
        }

        public RecommendationModel getRecommendations(string username, string answer1, string answer2, string answer3)
        {
            List<string> first = new List<string>();
            List<string> second = new List<string>();
            if(answer1 == "1")
            {
                first.Add(answer1);
            }
            else if (answer1 == "2")
            {
                second.Add(answer1);
            }
            if (answer2 == "1")
            {
                first.Add(answer2);
            }
            else if (answer2 == "2")
            {
                second.Add(answer2);
            }
            if (answer3 == "1")
            {
                first.Add(answer3);
            }
            else if (answer3 == "2")
            {
                second.Add(answer3);
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
                        "•MSc in Computer Science and Engineering - University of Piraeus" +
                        "•MSc in Human-Computer Interaction - National and Kapodistrian University of Athens (NKUA)" +
                        "•MSc in Game Design and Development - National and Kapodistrian University of Athens (NKUA)" +
                        "•MSc in Information Systems - Athens University of Economics and Business (AUEB)" +
                        "•MSc in Computer Science - University of Patras" +
                        "•MSc in Multimedia and Graphics - National and Kapodistrian University of Athens (NKUA)";
                }
                else if (dict.ElementAt(0).Value > 6)
                {
                    recommendation = "Your profile and preferences seem to associate you to front-end development, as you are more likely" +
                        "an artistic personality and like writing code in C#. Your Grades in subjects like Human-Computer Interaction are good, and you might prefer that subject among others." +
                        "Knowing all this, you can follow the Carreer Paths of UX Design, Web Development, Video Game Design or Software Engineering." +
                        "There are some fitting post-graduate programs available to you, like: " +
                        "•MSc in Computer Science and Engineering - University of Piraeus" +
                        "•MSc in Human-Computer Interaction - National and Kapodistrian University of Athens (NKUA)" +
                        "•MSc in Game Design and Development - National and Kapodistrian University of Athens (NKUA)" +
                        "•MSc in Information Systems - Athens University of Economics and Business (AUEB)" +
                        "•MSc in Computer Science - University of Patras" +
                        "•MSc in Multimedia and Graphics - National and Kapodistrian University of Athens (NKUA)";
                }
                else
                {
                    recommendation = "Your profile and preferences seem to associate you to front-end development, as you are more likely" +
                        "an artistic personality and like writing code in C#. However, your Grades in subjects like Human-Computer Interaction are not that good, even though you might prefer that subject among others, but with more experience in the topic you can become an expert later on." +
                        "Knowing all this, you can follow the Carreer Paths of UX Design, Web Development, Video Game Design or Software Engineering." +
                        "There are some fitting post-graduate programs available to you, like: " +
                        "•MSc in Computer Science and Engineering - University of Piraeus" +
                        "•MSc in Human-Computer Interaction - National and Kapodistrian University of Athens (NKUA)" +
                        "•MSc in Game Design and Development - National and Kapodistrian University of Athens (NKUA)" +
                        "•MSc in Information Systems - Athens University of Economics and Business (AUEB)" +
                        "•MSc in Computer Science - University of Patras" +
                        "•MSc in Multimedia and Graphics - National and Kapodistrian University of Athens (NKUA)";
                }
            }
            else if (first.Count < second.Count)
            {
                if (!dict.ElementAt(1).Value.HasValue)
                {
                    recommendation = "Your profile and preferences seem to associate you to computer hardware field, as you are more likely" + 
                        "a practical personality and like writing code in Assembly. You don't habve a grade yet in subjects like Computer Architecture, but this doesn't prevent you to start thinking about your career." + "Knowing all this, you can follow the Carreer Paths of Hardware Engineering or IT Service." + "There are some fitting post-graduate programs available to you, like:" + "•MSc in Electronic Computer Systems - University of Piraeus" + "•MSc in Computer Science and Engineering - University of Piraeus" + "•MSc in Electrical Engineering - National Technical University of Athens (NTUA)" + "•MSc in Electrical and Computer Engineering - Aristotle University of Thessaloniki" + "•MSc in Computer Engineering - University of Patras" + "•MSc in Information Systems and Services Engineering - Aristotle University of Thessaloniki";
                }
                else if (dict.ElementAt(1).Value > 7)
                {
                    recommendation = "Your profile and preferences seem to associate you to computer hardware field, as you are more likely" +
                        "a practical personality and like writing code in Assembly. Your Grades in subjects like Computer Architecture are good and you might prefer these subjects among others." +
                        "Knowing all this, you can follow the Carreer Paths of Hardware Engineering or IT Service." +
                        "There are some fitting post-graduate programs available to you, like:" +
                        "•MSc in Electronic Computer Systems - University of Piraeus" +
                        "•MSc in Computer Science and Engineering - University of Piraeus" +
                        "•MSc in Electrical Engineering - National Technical University of Athens (NTUA)" +
                        "•MSc in Electrical and Computer Engineering - Aristotle University of Thessaloniki" +
                        "•MSc in Computer Engineering - University of Patras" +
                        "•MSc in Information Systems and Services Engineering - Aristotle University of Thessaloniki";
                }
                else
                {
                    recommendation = "Your profile and preferences seem to associate you to computer hardware field, as you are more likely" +
                        "a practical personality and like writing code in Assembly. However, your Grades in subjects like Computer Architecture are not that good, even though you might prefer that subject among others, but with more experience in the topic you can become an expert later on." +
                        "Knowing all this, you can follow the Carreer Paths of Hardware Engineering or IT Service." +
                        "There are some fitting post-graduate programs available to you, like:" +
                        "•MSc in Electronic Computer Systems - University of Piraeus" +
                        "•MSc in Computer Science and Engineering - University of Piraeus" +
                        "•MSc in Electrical Engineering - National Technical University of Athens (NTUA)" +
                        "•MSc in Electrical and Computer Engineering - Aristotle University of Thessaloniki" +
                        "•MSc in Computer Engineering - University of Patras" +
                        "•MSc in Information Systems and Services Engineering - Aristotle University of Thessaloniki";
                }
            }
            RecommendationModel r = new RecommendationModel();
            r.username = username;
            r.recommendation = recommendation;
            var record = _context.recommendations.FirstOrDefault(x=>x.username == username);
            if (record != null)
            {
                record.recommendation = recommendation;
            }
            else
            {
                _context.recommendations.Add(r);
                _context.SaveChanges();
            }
            
            return r;
        }

        public RecommendationModel getRecommendationModel(string username)
        {
            var r = _context.recommendations.FirstOrDefault(x => x.username == username);
            return r;
        }
        public UserModel getInfo(string username)
        {
            var user = _context.users.FirstOrDefault(x => x.username == username);
            return user;
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
