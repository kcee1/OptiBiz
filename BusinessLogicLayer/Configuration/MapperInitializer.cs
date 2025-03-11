using AutoMapper;
using Domain.DTO;
using Domain.Models;

namespace BusinessLogicLayer.Configuration
{
    public class MapperInitializer : Profile
    {
        public MapperInitializer()
        {
            CreateMap<User, CreateUserDto>().ReverseMap();
            CreateMap<User, UpdateUserDto>().ReverseMap();
            CreateMap<User, GetUserDto>().ReverseMap();
            CreateMap<Role, GetRoleDto>().ReverseMap();
            CreateMap<Tenant, TenantDTO>().ReverseMap();
            CreateMap<Tenant, GetTenantDto>().ReverseMap();
            CreateMap<TransactionBeneficiary, CreateTransactionBeneficiaryDto>().ReverseMap();
            CreateMap<TransactionBeneficiary, GetTransactionBeneficiaryDto>().ReverseMap();
            CreateMap<Transaction, CreateTransactionDto>().ReverseMap();
            CreateMap<Transaction, UpdateTransactionDto>().ReverseMap();
            
        }
    }
}
