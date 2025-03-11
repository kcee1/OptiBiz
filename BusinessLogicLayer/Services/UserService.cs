using AutoMapper;
using BusinessLogicLayer.IServices;
using DataAccessLayer.IRepositories;
using DataAccessLayer.UnitOfWorkFolder;
using Domain.DTO;
using Domain.Models;

namespace BusinessLogicLayer.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Tenant> _tenantRepository;
        private readonly IUnitOfWork _iUnitOfWork;
        private readonly IUserRepository userRepository;
        IMapper mapper;
        private static readonly Random _random = new Random();


        public UserService(IUnitOfWork unitOfWork,
            IMapper mapper,
            IUserRepository userRepository)
        {
            _iUnitOfWork = unitOfWork;
            _userRepository = _iUnitOfWork.GetRepository<User>();
            _tenantRepository = _iUnitOfWork.GetRepository<Tenant>();
            this.mapper = mapper;
            this.userRepository = userRepository;
        }

        public async Task<bool> AssignUserToRole(string id, string roleName)
        {
            User? result = await userRepository.user(id);

            if(result != null)
            {
                bool finalResult = await userRepository.AddUserToRole(result, roleName);

                return finalResult;
            }

            return false;
        }


        public async Task<(GetUserDto?, string message)> CreateUser(CreateUserDto createUserDto)
        {
            if (string.IsNullOrWhiteSpace(createUserDto.TenantDTO.BusinessType))
            {
                return (null, "Business Type is required");
            }

            if (string.IsNullOrWhiteSpace(createUserDto.TenantDTO.BusinessNumber))
            {
                return (null, "Business Number is required");
            }

            if (string.IsNullOrWhiteSpace(createUserDto.TenantDTO.TaxIdentificationNumber))
            {
                return (null, "Tax Identification Number is required");
            }

            if (string.IsNullOrWhiteSpace(createUserDto.TenantDTO.BankVerificationCode) 
                || createUserDto.TenantDTO.BankVerificationCode.Length != 11)
            {
                return (null, "Bvn is required and must be 11 digits");
            }

            if (string.IsNullOrWhiteSpace(createUserDto.FirstName))
            {
                return (null, "First Name is required");
            }

          
            if (string.IsNullOrWhiteSpace(createUserDto.LastName))
            {
                return (null, "Last Name is required");
            }

            if (string.IsNullOrWhiteSpace(createUserDto.Email))
            {
                return (null, "Email is required");
            }

            if (string.IsNullOrWhiteSpace(createUserDto.Password))
            {
                return (null, "Password is required");
            }

            Tenant resultOfTenantMapping = mapper.Map<Tenant>(createUserDto.TenantDTO);
            User resultOfMapping = new User();


            Tenant resultOfCreatingTenant = _tenantRepository.Add(resultOfTenantMapping);
            _tenantRepository.Save();

            resultOfMapping.FirstName = createUserDto.FirstName;
            resultOfMapping.LastName = createUserDto.LastName;
            resultOfMapping.Email = createUserDto.Email;
            resultOfMapping.PasswordHash = createUserDto.Password;
            resultOfMapping.UserName = createUserDto.Email;
            resultOfMapping.CreatedAt = DateTime.UtcNow;
            resultOfMapping.AccountBalance = 0;
            resultOfMapping.IsEmailVerified = false;
            resultOfMapping.AccountNumber = _random.Next(1000000000, 1999999999).ToString();
            resultOfMapping.TenantId = resultOfCreatingTenant.Id;
            resultOfMapping.Id = Guid.NewGuid().ToString();
            User? outcome = await userRepository.createUser(resultOfMapping);
            
            _tenantRepository.Save();

            if (outcome is null)
            {
                return (null, "Unable to create user");

            }

            await AssignUserToRole(outcome.Id, createUserDto.Role);

            GetUserDto result = mapper.Map<GetUserDto>(outcome);
            result.Role = createUserDto.Role;
            return (result, "Created successfully");
        }


        public async Task<bool> RemoveUserFromRole(string id, string roleName)
        {
            User? result = await userRepository.user(id);

            if (result != null)
            {
                bool finalResult = await userRepository.RemoveUserFromRole(result, roleName);

                return finalResult;
            }

            return false;
        }


        public async Task<(bool, string message)> UpdateUser(string id)
        {
            User? theUser = await userRepository.UpdateUser(id);
            if (theUser == null)
            {
                return (false, "Invalid user Id");
            }

            return (true, "Updated successfully");

        }


        public async Task<GetUserDto> User(string id)
        {
            return mapper.Map<GetUserDto>(await userRepository.user(id));

        }

        public async Task<GetUserDto> GetUserbyEmail(string email)
        {
            return mapper.Map<GetUserDto>(await userRepository.getUserbyUserName(email));

        }

        public List<GetUserDto> Users()
        {
            return mapper.Map<List<GetUserDto>>(userRepository.users());
        }

        public async Task<IList<GetTenantDto>> GetAllTenants()
        {
            return mapper.Map<IList<GetTenantDto>>(await _tenantRepository.GetAllAsync());
        }


        public async Task<IList<GetUserDto>> GetTenantUser(int tenantId)
        {
            return mapper.Map<IList<GetUserDto>>(await _userRepository.GetSingleByAsync(q => q.TenantId == tenantId));
        }



    }
}
