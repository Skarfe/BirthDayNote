using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.ViewModels
{
    public class BirthdayViewModel : IComparable<BirthdayViewModel>
    {
        
        public int Id { get; set; }
        public DateTime BirthdayDate { get; set; }
        public String PersoneName { get; set; }
        public String PhotoPath { get; set; }
        public String Email { get; set; }
        public IFormFile ImageFile { get; set; }
        public int CompareTo(BirthdayViewModel b)
        {
            return this.BirthdayDate.CompareTo(b.BirthdayDate);
        }
    }
}
