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
        //Id - ключ
        [Key]
        public int Id { get; set; }
        //Дата дня рождения
        public DateTime BirthdayDate { get; set; }
        //Имя
        public String PersoneName { get; set; }
        //Имя фотографии
        public String ImageName { get; set; }
        //Почта
        public String Email { get; set; }
        //Когда был оповещён в последний раз (используется только год при проверке)
        public DateTime LastTimeEmailSent { get; set; }
    }
}
