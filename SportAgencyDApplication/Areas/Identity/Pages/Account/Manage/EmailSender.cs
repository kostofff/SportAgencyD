using Microsoft.AspNetCore.Identity.UI.Services;
using System.Threading.Tasks;

namespace SportAgencyDApplication.Services
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            // TODO: Тук можеш да сложиш реална логика за изпращане на имейли
            // Засега просто връщаме изпълнена задача, за да няма грешки
            return Task.CompletedTask;
        }
    }
}