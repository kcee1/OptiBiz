using AutoMapper;
using BusinessLogicLayer.IServices;
using DataAccessLayer.IRepositories;
using DataAccessLayer.Repositories;
using DataAccessLayer.UnitOfWorkFolder;
using Domain.Models;
using static System.Net.WebRequestMethods;

namespace BusinessLogicLayer.Services
{
    public class UserVerificationService : IUserVerificationService
    {
        private readonly IRepository<UserVerification> _userVerificationRepository;
        private readonly IUnitOfWork _iUnitOfWork;
        private readonly IUserRepository userRepository;
        private static readonly Random _random = new Random();
        private readonly IEmailSender _mail;
        IMapper mapper;

        public UserVerificationService(
            IUserRepository userRepository,
            IEmailSender mail, 
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _iUnitOfWork = unitOfWork;
            _userVerificationRepository = _iUnitOfWork.GetRepository<UserVerification>();
            this.userRepository = userRepository;
            _mail = mail;
            this.mapper = mapper;
        }

        public async Task<(bool, string message)> CreateOtp(string userId, string email)
        {
            User? theUser = await userRepository.user(userId);

            if(theUser == null)
            {
                return (false, "Invalid user id");
            }

            UserVerification userVerification = new UserVerification
            {
                IsUsed = false,
                OTP = _random.Next(1111, 9999).ToString(),
                UserId = userId,
                ExpiryTime = DateTime.UtcNow.AddMinutes(3)

            };

            await _userVerificationRepository.AddAsync(userVerification);
            await _userVerificationRepository.SaveAsync();

            // If emails are found, extract them into a list
            List<string> emailList = new List<string>
            {
                email
            };

            MailData mailData = new MailData(
                emailList,
             $"OptiBiz Otp", "Your OTP will expire in 3 minute" + userVerification.OTP);

            bool sendResult = _mail.SendAsync(mailData, new CancellationToken()).GetAwaiter().GetResult();
            if (sendResult)
            {
                return (true, "Otp sent successfully");
            }

            return (true, "Unable to send Otp");

        }

        public async Task<bool> VerifyOtp(string userId, string Otp)
        {
            UserVerification userVerification = await _userVerificationRepository.GetSingleByAsync(q => q.UserId == userId && q.OTP == Otp && q.ExpiryTime >= DateTime.UtcNow);
            
            if(userVerification is null)
            {
                return false;
            }

            if (userVerification.IsUsed)
            {
                return false;
            }

            userVerification.IsUsed = true;
            await _userVerificationRepository.UpdateAsync(userVerification);
            await _userVerificationRepository.SaveAsync();

            return true;
        
        }
    }
}
