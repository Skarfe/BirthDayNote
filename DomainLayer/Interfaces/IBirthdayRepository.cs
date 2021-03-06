using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Entities;

namespace DomainLayer.Interfaces
{
    //Интерфейс для работы с сохранением информации
    public interface IBirthdayRepository
    {
        public Birthday GetBirthday(int id);
        public int CreateBirthday(Birthday birthday);
        public void UpdateBirthday(Birthday birthday);
        public void RemoveBirthday(int id);
        public List<Birthday> GetBirthdays();
        public List<Birthday> GetUpcommingBirthdays();
        public List<Birthday> GetTodayBirthdaysNotNotificated();
    }
}