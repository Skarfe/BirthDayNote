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

        public List<Birthday> GetBirthdays()
        {
            List<Birthday> list = _context.Birthdays.ToList();

            return list;
        }
    }
}
