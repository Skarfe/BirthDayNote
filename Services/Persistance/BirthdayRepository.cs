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

        public void CreateBirthday(Birthday birthday)
        {
            _context.Birthdays.Add(birthday);
            _context.SaveChanges();
        }

        public void UpdateBirthday(Birthday birthday)
        {
            _context.Birthdays.Update(birthday);
            _context.SaveChanges();
        }

        public void RemoveBirthday(Birthday birthday)
        {
            _context.Birthdays.Remove(birthday);
            _context.SaveChanges();
        }

        public List<Birthday> GetBirthdays()
        {
            List<Birthday> list = _context.Birthdays.ToList();
            return list;
        }

        public List<Birthday> GetUpcommingBirthdays()
        {
            List<Birthday> list = _context.Birthdays.Where(b => (b.BirthdayTime.Month == DateTime.Now.Month && b.BirthdayTime.Day >= DateTime.Now.Day) || 
                (b.BirthdayTime.Month == DateTime.Now.AddMonths(1).Month && b.BirthdayTime.Day <= DateTime.Now.Day)).ToList();
            return list;
        }
    }
}