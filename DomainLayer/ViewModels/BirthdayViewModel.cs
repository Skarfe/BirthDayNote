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
        //Id-ключ
        [Key]
        public int Id { get; set; }
        //Имя обязательно, от 3 до 50 символов
        [Required]
        [Display(Name = "Name")]
        [StringLength(50, MinimumLength = 3)]
        public String PersoneName { get; set; }
        //Дата обязательна, без времени
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date of Birth")]
        public DateTime BirthdayDate { get; set; }
        //Почта обязательна, проверка на соответствие адресу почты 
        [Required]
        [EmailAddress]
        [Display(Name = "Email address")]
        public String Email { get; set; }
        //Имя фотографии
        public String ImageName { get; set; }
        //Загрузка фоторафии
        [Display(Name = "Load image")]
        public IFormFile ImageFile { get; set; }
        //Сортировка по дате
        public int CompareTo(BirthdayViewModel b)
        {
            return this.BirthdayDate.CompareTo(b.BirthdayDate);
        }
    }
}
    