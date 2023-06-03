using Education_Software.Context;
using Education_Software.Models;
using Microsoft.AspNetCore.Mvc;
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
        
        public List<Tuple<string, string, List<string>, List<string>>> SplitQuestions(List<QuestionModel> model)
        {
            List<QuestionModel> questions = getRandomQuestions(model);
            List<Tuple<string, string, List<string>, List<string>>> q = new List<Tuple<string, string, List<string>, List<string>>>();
            foreach (QuestionModel question in questions)
            {
                List<string> body = new List<string>();
                List<string> possible_answers = new List<string>();
                if (question.q_type == "multiple_choice")
                {
                    string[] s = question.question.Split("•");
                    Debug.WriteLine(s);
                    body.Add(s[0]);
                    possible_answers = s.Skip(1).ToList();
                }
                else if (question.q_type == "true/false")
                {
                    string[] s = question.question.Split("•");
                    body.Add(s[0]);
                    possible_answers = s.Skip(1).ToList();
                }
                else if (question.q_type == "ordering")
                {
                    string[] s = question.question.Split("•");
                    body.Add(s[0]);
                    possible_answers = s.Skip(1).ToList();
                }
                else if (question.q_type == "completion")
                {
                    string[] s = question.question.Split("•");
                    body.Add(s[0]);
                    possible_answers = s.Skip(1).ToList();
                }
                else if (question.q_type == "matching")
                {
                    string[] s = question.question.Split("•");
                    body.Add(s[0]);
                    int half = (s.Length-1) / 2;
                    body = s.Skip(1).Where((e, i) => i < half).ToList();
                    possible_answers = s.Skip(half).ToList();
                }

                Tuple<string, string, List<string>, List<string>> el = new Tuple<string, string, List<string>, List<string>>(question.q_id, question.q_type, body, possible_answers);
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
                test.test_id = "";
                test.q_id = q_id;
                test.test_type = test_type;
                test.score = result;
            }
            _context.tests.Add(test);
            return results;
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
