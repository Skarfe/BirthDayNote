using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Interfaces
{
    //Интерфейс отправци сообщений
    public interface IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string message);
    }
}
