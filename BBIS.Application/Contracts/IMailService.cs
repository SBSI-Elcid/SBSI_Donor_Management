using BBIS.Application.DTOs.Mail;

namespace BBIS.Application.Contracts
{
    public interface IMailService
    {
        Task SendEmailAsync(MailRequest mailRequest);
    }
}
