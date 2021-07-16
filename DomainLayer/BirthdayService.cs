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
        public List<BirthdayViewModel> GetUpcommingBirthdays()
        {
            List<Birthday> birthdays = _repository.GetUpcommingBirthdays();

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
            birthdaysVM.Sort();
            return birthdaysVM;
        }
        public List<Birthday> GetTodayBirthdaysNotNotificaded()
        {
            return _repository.GetTodayBirthdaysNotNotificated();
        }
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
        public void UpdateBirthday(Birthday birthday)
        {
            _repository.UpdateBirthday(birthday);
        }
        public void RemoveBirthday(int Id)
        {
            _repository.RemoveBirthday(Id);
        }
    }
}
