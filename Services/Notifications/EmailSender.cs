using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainLayer.Interfaces;
using System.Threading.Tasks;

namespace Services.Notifications
{
    //Использование MailKit для отправки сообщений на почту
    public class EmailSender : IEmailSender
    {
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();
            //Установить имя и адрес отправителя
            emailMessage.From.Add(new MailboxAddress("Birthday Note", "birthday.note@yandex.ru"));
            //Установить имя и адрес получателя
            emailMessage.To.Add(new MailboxAddress("", email));
            //Тема сообщения
            emailMessage.Subject = subject;
            //Текст сообщения
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            using (var client = new SmtpClient())
            {
                //Использование почтового сервиса от Yandex
                await client.ConnectAsync("smtp.yandex.ru", 465, true);
                //Аутентификация в сервисе
                await client.AuthenticateAsync("birthday.note@yandex.ru", "birthday.note1234");
                //Отправить сообщение
                await client.SendAsync(emailMessage);
                //Отключиться от сервиса
                await client.DisconnectAsync(true);
            }
        }
    }
}
