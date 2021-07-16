using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.ViewModels
{
    public class BirthdayViewModel : IComparable<BirthdayViewModel>
    {
        
        public int Id { get; set; }
        [Required]
        [Display(Name = "Name")]
        [StringLength(50, MinimumLength = 3)]
        public String PersoneName { get; set; }
        [Required]
        [Display(Name = "Date of Birth")]
        public DateTime BirthdayDate { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Email address")]
        public String Email { get; set; }
        public String ImageName { get; set; }
        [Display(Name = "Load image")]
        public IFormFile ImageFile { get; set; }
        public int CompareTo(BirthdayViewModel b)
        {
            return this.BirthdayDate.CompareTo(b.BirthdayDate);
        }
    }
}
    