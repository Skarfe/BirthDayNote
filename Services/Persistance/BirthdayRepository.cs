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

        //Сохранить событие и вернуть его Id
        public int CreateBirthday(Birthday birthday)
        {
            _context.Birthdays.Add(birthday);
            _context.SaveChanges();
            return birthday.Id;
        }
        //Загрузить событие
        public Birthday GetBirthday(int Id)
        {            
            return _context.Birthdays.FirstOrDefault(b => b.Id == Id);
        }

        //Сохранить изменения события
        public void UpdateBirthday(Birthday birthday)
        {
            _context.Birthdays.Update(birthday);
            _context.SaveChanges();
        }

        //Удалить событие
        public void RemoveBirthday(int Id)
        {
            _context.Birthdays.Remove(_context.Birthdays.FirstOrDefault(b => b.Id == Id));
            _context.SaveChanges();
        }

        //Получить все дни рождения
        public List<Birthday> GetBirthdays()
        {
            List<Birthday> list = _context.Birthdays.ToList();
            return list;
        }

        //Получить дни рождения от сегоднешнего дня до этого же числа следующего месяца
        public List<Birthday> GetUpcommingBirthdays()
        {
            List<Birthday> list = _context.Birthdays.Where(b => (b.BirthdayDate.Month == DateTime.Now.Month && b.BirthdayDate.Day >= DateTime.Now.Day) ||
                (b.BirthdayDate.Month == DateTime.Now.AddMonths(1).Month && b.BirthdayDate.Day <= DateTime.Now.Day)).ToList();
            return list;
        }
        //Получить список сегодняшних не оповещённых событий (в этом году = сегодня)
        public List<Birthday> GetTodayBirthdaysNotNotificated()
        {
                List<Birthday> list = _context.Birthdays.Where(b => (
                (b.BirthdayDate.DayOfYear == DateTime.Now.DayOfYear && b.BirthdayDate.Day == 29 && b.BirthdayDate.Month == 2)
                || (b.BirthdayDate.Day == DateTime.Now.Day && b.BirthdayDate.Month == DateTime.Now.Month && b.LastTimeEmailSent.Year != DateTime.Now.Year))).ToList();
            return list;
        }
    }
}