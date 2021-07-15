using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Entities
{
    public class Birthday
    {
        [Key]
        public int Id { get; set; }
        public DateTime BirthdayDate { get; set; }
        public String PersoneName { get; set; }
        public String PhotoPath { get; set; }
        public String Email { get; set; }
        public DateTime LastTimeEmailSent { get; set; }
    }
}
