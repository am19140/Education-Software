using Education_Software.Context;
using Education_Software.Models;
using System;
using System.Linq;

namespace Education_Software.Service
{

    public class SubjectService
    {
        private readonly DatabaseContext _context;

        public SubjectService(DatabaseContext context)
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
