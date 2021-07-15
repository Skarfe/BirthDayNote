using DomainLayer.Entities;
using DomainLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Persistance
{
    public class BirthdayRepository : IBirthdayRepository
    {
        private readonly BirthdayContext _context;

        public BirthdayRepository(BirthdayContext context)
        {
            _context = context;
        }

        public int CreateBirthday(Birthday birthday)
        {
            _context.Birthdays.Add(birthday);
            _context.SaveChanges();
            return birthday.Id;
        }
        public Birthday GetBirthday(int Id)
        {            
            return _context.Birthdays.FirstOrDefault(b => b.Id == Id);
        }

        public void UpdateBirthday(Birthday birthday)
        {
            _context.Birthdays.Update(birthday);
            _context.SaveChanges();
        }

        public void RemoveBirthday(int Id)
        {
            _context.Birthdays.Remove(_context.Birthdays.FirstOrDefault(b => b.Id == Id));
            _context.SaveChanges();
        }

        public List<Birthday> GetBirthdays()
        {
            List<Birthday> list = _context.Birthdays.ToList();
            return list;
        }

        public List<Birthday> GetUpcommingBirthdays()
        {
            List<Birthday> list = _context.Birthdays.Where(b => (b.BirthdayDate.Month == DateTime.Now.Month && b.BirthdayDate.Day >= DateTime.Now.Day) || 
                (b.BirthdayDate.Month == DateTime.Now.AddMonths(1).Month && b.BirthdayDate.Day <= DateTime.Now.Day)).ToList();
            return list;
        }
    }
}