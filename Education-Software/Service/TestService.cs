using Education_Software.Context;
using Education_Software.Models;
using System;
using System.Drawing.Printing;
// using System.Linq;

namespace Education_Software.Service
{

    public class TestService
    {
        private readonly DatabaseContext _context;

        public TestService(DatabaseContext context)
        {
            _context = context;
        }

        /*
         * public List<QuestionModel> getQuestions(int i)
        {
            var questions = (from question in _context.questions
                            select question).ToList();
            return questions;
        }
         */
        public QuestionModel SubjectTest(SubjectModel subjectmodel)
        {
            var question = _context.questions.FirstOrDefault(x => x.sub_id == subjectmodel.sub_id);
            return question;
        }

        public List<QuestionModel> getRandomQuestions(string subject)
        {
            @Random r = new Random((int)DateTime.Now.Ticks);
            List<QuestionModel> questions = _context.questions.Where(h => h.sub_id == subject).OrderBy(h => r.Next()).Take(2).ToList();
            return questions;
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
