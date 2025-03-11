using Domain.Models;

namespace BusinessLogicLayer.IServices
{
    public interface IUserVerificationService
    {
        Task<(bool, string message)> CreateOtp(string userId, string email);
        Task<bool> VerifyOtp(string userId, string Otp);


    }
}
