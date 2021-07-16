using BirthdayNote.Filters;
using DomainLayer;
using DomainLayer.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace BirthdayNote.Controllers
{
    [ServiceFilter(typeof(MyExceptionFilter))]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly BirthdayService _birthdayService;

        public HomeController(BirthdayService birthdayService,
            ILogger<HomeController> logger,
            IHostingEnvironment environment)
        {
            _birthdayService = birthdayService;
            _logger = logger;
            _hostingEnvironment = environment;
        }

        public IActionResult Index()
        {
            List<BirthdayViewModel> birthdays = _birthdayService.GetUpcommingBirthdays();
            return View(birthdays);
        }
        public IActionResult Details(int Id)
        {
            BirthdayViewModel birthdayViewModel = _birthdayService.GetBirthday(Id);
            return View(birthdayViewModel);
        }
        public IActionResult Edit(int Id)
        {
            BirthdayViewModel birthdayViewModel = _birthdayService.GetBirthday(Id);
            return View(birthdayViewModel);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BirthdayViewModel birthdayViewModel)
        {
            if (ModelState.IsValid)
            {
                if (birthdayViewModel.ImageFile != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(birthdayViewModel.ImageFile.FileName)
                        + DateTime.Now.ToString("yyMMddHHMMssfff")
                        + Path.GetExtension(birthdayViewModel.ImageFile.FileName);
                    var filePath = Path.Combine(_hostingEnvironment.WebRootPath, "Images\\");
                    using (var fileStream = new FileStream(filePath + fileName, FileMode.Create))
                    {
                        await birthdayViewModel.ImageFile.CopyToAsync(fileStream);
                    }
                    birthdayViewModel.ImageName = fileName;
                }

                _birthdayService.CreateBirthday(birthdayViewModel);
            }
            return View(birthdayViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(BirthdayViewModel birthdayViewModel)
        {
            if (ModelState.IsValid)
            {
                if (birthdayViewModel.ImageFile != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(birthdayViewModel.ImageFile.FileName)
                        + DateTime.Now.ToString("yyMMddHHMMssfff")
                        + Path.GetExtension(birthdayViewModel.ImageFile.FileName);
                    var filePath = Path.Combine(_hostingEnvironment.WebRootPath, "Images\\");
                    using (var fileStream = new FileStream(filePath + fileName, FileMode.Create))
                    {
                        birthdayViewModel.ImageFile.CopyTo(fileStream);
                    }
                    birthdayViewModel.ImageName = fileName;
                }


                _birthdayService.UpdateBirthday(birthdayViewModel);
                return RedirectToAction("Index");
            }

            return View(birthdayViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RemoveBirthday(int Id)
        {
            _birthdayService.RemoveBirthday(Id);
            return RedirectToAction("Index");
        }
    }
}
