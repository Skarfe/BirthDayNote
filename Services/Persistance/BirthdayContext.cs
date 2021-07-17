using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DomainLayer.Entities;

namespace Services.Persistance
{
    public class BirthdayContext : DbContext
    {
        public DbSet<Birthday> Birthdays { get; set; }

        public BirthdayContext(DbContextOptions<BirthdayContext> options)
            : base(options)
        {
        }
    }
}
