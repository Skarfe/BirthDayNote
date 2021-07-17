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
        public async void DoWork(object state)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var birthdayService = scope.ServiceProvider.GetRequiredService<BirthdayService>();
                List<Birthday> list = birthdayService.GetTodayBirthdaysNotNotificaded();
                foreach (Birthday birthday in list)
                {
                    try
                    {
                        await _sender.SendEmailAsync(birthday.Email, "Happy Birthday!", birthday.PersoneName + "! Happy birthday!");
                        birthday.LastTimeEmailSent = DateTime.Now;
                        birthdayService.UpdateBirthday(birthday);
                    } catch
                    {
                        //something went wrong
                    }
                }
            }
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(DoWork, null, TimeSpan.Zero,
            TimeSpan.FromMinutes(15));

            return Task.CompletedTask;
        }
        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }
        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
