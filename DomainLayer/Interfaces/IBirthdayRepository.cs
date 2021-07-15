using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Entities;

namespace DomainLayer.Interfaces
{
    public interface IBirthdayRepository
    {
        public void CreateBirthday(Birthday birthday);
        public void UpdateBirthday(Birthday birthday);
        public void RemoveBirthday(Birthday birthday);
        public List<Birthday> GetBirthdays();
        public List<Birthday> GetUpcommingBirthdays();
    }
}