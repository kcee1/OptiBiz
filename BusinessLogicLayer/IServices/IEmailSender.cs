using Domain.Models;

namespace BusinessLogicLayer.IServices
{
    public interface IEmailSender
    {
        Task<bool> SendAsync(MailData mailData, CancellationToken ct);
    }
}
