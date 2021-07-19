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
            //Начальная страница, загрузка ближайших дней рождений (в течении следующего месяца)
            List<BirthdayViewModel> birthdays = _birthdayService.GetUpcommingBirthdays();
            return View(birthdays);
        }
        public IActionResult AllBirthdays()
        {
            //Начальная страница, все дни рождения
            List<List<BirthdayViewModel>> birthdays = _birthdayService.GetAllBirthdays();
            return View(birthdays);
        }
        public IActionResult Details(int Id)
        {
            //Страница просмотра деталей 
            BirthdayViewModel birthdayViewModel = _birthdayService.GetBirthday(Id);
            return View(birthdayViewModel);
        }
        public IActionResult Edit(int Id)
        {
            //Редактирования дня рождения
            BirthdayViewModel birthdayViewModel = _birthdayService.GetBirthday(Id);
            return View(birthdayViewModel);
        }
        public IActionResult Create()
        {
            //Добавление нового дня рождения
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BirthdayViewModel birthdayViewModel)
        {
            //Проверка данных на ошибки
            if (ModelState.IsValid)
            {
                //Была ли загружена фотография
                if (birthdayViewModel.ImageFile != null)
                {
                    //создание неповторяющегося имени фотографии
                    string fileName = Path.GetFileNameWithoutExtension(birthdayViewModel.ImageFile.FileName)
                        + DateTime.Now.ToString("yyMMddHHMMssfff")
                        + Path.GetExtension(birthdayViewModel.ImageFile.FileName);
                    var filePath = Path.Combine(_hostingEnvironment.WebRootPath, "Images\\");
                    using (var fileStream = new FileStream(filePath + fileName, FileMode.Create)) //Сохранение фотографии в дерикторию проекта
                    {
                        await birthdayViewModel.ImageFile.CopyToAsync(fileStream);
                    }
                    birthdayViewModel.ImageName = fileName;
                }
                //Сохранение дня рождения в БД
                _birthdayService.CreateBirthday(birthdayViewModel);
            }
            //Вернуться на начальную страницу
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(BirthdayViewModel birthdayViewModel)
        {
            //Проверка данных на ошибки
            if (ModelState.IsValid)
            {
                //Была ли загружена фотография
                if (birthdayViewModel.ImageFile != null)
                {
                    //создание неповторяющегося имени фотографии
                    string fileName = Path.GetFileNameWithoutExtension(birthdayViewModel.ImageFile.FileName)
                        + DateTime.Now.ToString("yyMMddHHMMssfff")
                        + Path.GetExtension(birthdayViewModel.ImageFile.FileName);
                    var filePath = Path.Combine(_hostingEnvironment.WebRootPath, "Images\\");
                    using (var fileStream = new FileStream(filePath + fileName, FileMode.Create)) //Сохранение фотографии в дерикторию проекта
                    {
                        birthdayViewModel.ImageFile.CopyTo(fileStream);
                    }
                    birthdayViewModel.ImageName = fileName;
                }

                //Обновление данных в БД
                _birthdayService.UpdateBirthday(birthdayViewModel);
                return RedirectToAction("Index");
            }

            return View(birthdayViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RemoveBirthday(int Id)
        {
            //Удаление дня рождения из БД
            _birthdayService.RemoveBirthday(Id);
            return RedirectToAction("Index");
        }
    }
}
