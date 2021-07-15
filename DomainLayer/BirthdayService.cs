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
                    PhotoPath = b.PhotoPath,
                    Email = b.Email,
                })
                .ToList();
            birthdaysVM.Sort();
            return birthdaysVM;
        }
        public BirthdayViewModel GetBirthday(int Id)
        {
            Birthday birthday = _repository.GetBirthday(Id);

            BirthdayViewModel birthdayVM = new BirthdayViewModel()
            {
                Id = birthday.Id,
                BirthdayDate = birthday.BirthdayDate,
                PersoneName = birthday.PersoneName,
                PhotoPath = birthday.PhotoPath,
                Email = birthday.Email
            };
            return birthdayVM;
        }
        public void CreateBirthday(BirthdayViewModel birthdayViewModel)
        {
            Birthday birthday = new Birthday();
            {
                birthday.Id = birthdayViewModel.Id;
                birthday.BirthdayDate = birthdayViewModel.BirthdayDate;
                birthday.PersoneName = birthdayViewModel.PersoneName;
                birthday.PhotoPath = birthdayViewModel.PhotoPath;
                birthday.Email = birthdayViewModel.Email;
            }
            _repository.CreateBirthday(birthday);
        }
        public void UpdateBirthday(BirthdayViewModel birthdayViewModel)
        {
            Birthday birthday = _repository.GetBirthday(birthdayViewModel.Id);

            {
                birthday.Id = birthdayViewModel.Id;
                birthday.BirthdayDate = birthdayViewModel.BirthdayDate;
                birthday.PersoneName = birthdayViewModel.PersoneName;
                birthday.PhotoPath = birthdayViewModel.PhotoPath;
                birthday.Email = birthdayViewModel.Email;
            }
            _repository.UpdateBirthday(birthday);
        }
        public void RemoveBirthday(int Id)
        {
            _repository.RemoveBirthday(Id);
        }
    }
}
