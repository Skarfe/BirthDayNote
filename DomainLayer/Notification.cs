using DomainLayer.Entities;
using DomainLayer.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DomainLayer
{    public class Notification : IHostedService, IDisposable
    {
        private readonly IEmailSender _sender;
        private Timer _timer;
        private readonly IServiceScopeFactory _scopeFactory;

        public Notification(IEmailSender sender, IServiceScopeFactory scopeFactory)
        {
            _sender = sender;
            _scopeFactory = scopeFactory;
        }
        //Функция оповещения
        public async void DoWork(object state)
        {
            //Создать службу работы с базой данных
            using (var scope = _scopeFactory.CreateScope())
            {
                var birthdayService = scope.ServiceProvider.GetRequiredService<BirthdayService>();
                //Загрузить из базы данных не оповещённые, сегодняшние дни рождения 
                List<Birthday> list = birthdayService.GetTodayBirthdaysNotNotificaded();
                //Для каждого события
                foreach (Birthday birthday in list)
                {
                    try
                    {
                        //Попытаться отправить сообщение
                        await _sender.SendEmailAsync(birthday.Email, "Happy Birthday!", birthday.PersoneName + "! Happy birthday!");
                        //Установить текущую дату в свойство события(LastTimeEmailSent)
                        birthday.LastTimeEmailSent = DateTime.Now;
                        //Сохранить дату оповещения
                        birthdayService.UpdateBirthday(birthday);
                    } catch (Exception e)
                    {
                        Console.WriteLine(e.Message);//Сообщение об ошибке в консоль
                    }
                }
            }
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            //Создать таймер, запускать проверку и оповещение событий каждые 15 минут
            _timer = new Timer(DoWork, null, TimeSpan.Zero,
            TimeSpan.FromMinutes(15));

            return Task.CompletedTask;
        }
        public Task StopAsync(CancellationToken cancellationToken)
        {
            //Остановить оповещение
            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }
        public void Dispose()
        {
            //Удалить таймер
            _timer?.Dispose();
        }
    }
}
