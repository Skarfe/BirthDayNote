using DomainLayer.Entities;
using DomainLayer.Interfaces;
using DomainLayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer
{
    public class BirthdayService
    {
        private readonly IBirthdayRepository _repository;
        public BirthdayService(IBirthdayRepository repository)
        {
            _repository = repository;
        }
        //Загрузить ближайшие события
        public List<BirthdayViewModel> GetUpcommingBirthdays()
        {
            List<Birthday> birthdays = _repository.GetUpcommingBirthdays();
            //Перевод сущностей в модели
            List<BirthdayViewModel> birthdaysVM = birthdays
                .Select(b => new BirthdayViewModel
                {
                    Id = b.Id,
                    BirthdayDate = b.BirthdayDate,
                    PersoneName = b.PersoneName,
                    ImageName = b.ImageName,
                    Email = b.Email,
                })
                .ToList();
            //Сортировка (по дате)
            birthdaysVM.Sort();
            return birthdaysVM;
        }
        //Загрузить все события
        public List<BirthdayViewModel> GetAllBirthdays()
        {
            List<Birthday> birthdays = _repository.GetBirthdays();
            //Перевод сущностей в модели
            List<BirthdayViewModel> birthdaysVM = birthdays
                .Select(b => new BirthdayViewModel
                {
                    Id = b.Id,
                    BirthdayDate = b.BirthdayDate,
                    PersoneName = b.PersoneName,
                    ImageName = b.ImageName,
                    Email = b.Email,
                })
                .ToList();
            //Сортировка (по дате)
            birthdaysVM.Sort();
            return birthdaysVM;
        }
        //Получить текущие события
        public List<Birthday> GetTodayBirthdaysNotNotificaded()
        {
            return _repository.GetTodayBirthdaysNotNotificated();
        }
        //Получить событие по Id
        public BirthdayViewModel GetBirthday(int Id)
        {
            Birthday birthday = _repository.GetBirthday(Id);

            BirthdayViewModel birthdayVM = new BirthdayViewModel()
            {
                Id = birthday.Id,
                BirthdayDate = birthday.BirthdayDate,
                PersoneName = birthday.PersoneName,
                ImageName = birthday.ImageName,
                Email = birthday.Email
            };
            return birthdayVM;
        }
        //Создать событие и вернуть его Id
        public int CreateBirthday(BirthdayViewModel birthdayViewModel)
        {
            Birthday birthday = new Birthday();
            {
                birthday.Id = birthdayViewModel.Id;
                birthday.BirthdayDate = birthdayViewModel.BirthdayDate;
                birthday.PersoneName = birthdayViewModel.PersoneName;
                birthday.ImageName = birthdayViewModel.ImageName;
                birthday.Email = birthdayViewModel.Email;
            }
            _repository.CreateBirthday(birthday);
            return birthday.Id;
        }
        //Сохронить изменения события с помощью модели
        public void UpdateBirthday(BirthdayViewModel birthdayViewModel)
        {
            Birthday birthday = _repository.GetBirthday(birthdayViewModel.Id);

            {
                birthday.Id = birthdayViewModel.Id;
                birthday.BirthdayDate = birthdayViewModel.BirthdayDate;
                birthday.PersoneName = birthdayViewModel.PersoneName;
                birthday.ImageName = birthdayViewModel.ImageName;
                birthday.Email = birthdayViewModel.Email;
            }
            _repository.UpdateBirthday(birthday);
        }
        //Сохронить изменения события
        public void UpdateBirthday(Birthday birthday)
        {
            _repository.UpdateBirthday(birthday);
        }
        //Удалиь событие из БД
        public void RemoveBirthday(int Id)
        {
            _repository.RemoveBirthday(Id);
        }
    }
}
