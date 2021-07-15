using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BirthdayNote.Models;
using DomainLayer;
using DomainLayer.ViewModels;

namespace BirthdayNote.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly BirthdayService _service;


        public HomeController(BirthdayService service,
            ILogger<HomeController> logger)
        {
            _service = service;
            _logger = logger;
        }

        public IActionResult Index()
        {
            List<BirthdayViewModel> birthdays = _service.GetUpcommingBirthdays();
            return View(birthdays);
        }

        public IActionResult Details(int Id)
        {

            BirthdayViewModel birthdayViewModel = _service.GetBirthday(Id);

            return View(birthdayViewModel);
        }
        public IActionResult Edit(int Id)
        {
            BirthdayViewModel birthdayViewModel = _service.GetBirthday(Id);
            return View(birthdayViewModel);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(BirthdayViewModel birthdayViewModel)
        {
            if (ModelState.IsValid)
            {
                _service.CreateBirthday(birthdayViewModel);
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(BirthdayViewModel birthdayViewModel)
        {
            if (ModelState.IsValid)
            {
                _service.UpdateBirthday(birthdayViewModel);
                return RedirectToAction("Index");
            }

            return View(birthdayViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RemoveBirthday(int Id)
        {
            _service.RemoveBirthday(Id);
            return RedirectToAction("Index");
        }

    }
}
