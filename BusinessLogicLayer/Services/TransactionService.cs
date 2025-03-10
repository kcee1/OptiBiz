using AutoMapper;
using BusinessLogicLayer.Helper;
using BusinessLogicLayer.IServices;
using DataAccessLayer.IRepositories;
using DataAccessLayer.UnitOfWorkFolder;
using Domain.DTO;
using Domain.Models;

namespace BusinessLogicLayer.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IRepository<Transaction> _transactionRepository;
        private readonly IUnitOfWork _iUnitOfWork;
        IMapper mapper;

        public TransactionService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _iUnitOfWork = unitOfWork;
            _transactionRepository = _iUnitOfWork.GetRepository<Transaction>();
            this.mapper = mapper;
        }

        public async Task<(GetTransactionDto?,string message)> createTransactions(CreateTransactionDto transaction)
        {
            if (string.IsNullOrWhiteSpace(transaction.Type))
            {
                
                return (null, "Transaction Type is required");
            }

            foreach (var item in transaction.Beneficiaries)
            {
                if (string.IsNullOrWhiteSpace(item.BeneficiaryName))
                {
                    return (null, "Beneficiary Name is required");
                }

                if (item.BeneficiaryAccountNumber.Length != 10)
                {
                    return (null, "Invalid account number");
                }

                if (item.Amount <= 0)
                {
                    return (null, "Invalid account number");
                }
            }

            //Fetch USer Details and verify he has money
            //Verify Tenant is valid And Get Tenant Details
            //Verify Initiators Name

            Random rnd = new Random(222222);
            

            Transaction mappedResult = mapper.Map<Transaction>(transaction);
            mappedResult.Status = TransactionStatus.Pending;
            mappedResult.CreatedAt = DateTime.UtcNow;
            mappedResult.ReferenceNumber = "OPTIBIZ" + rnd;

            return (mapper.Map<GetTransactionDto>(await _transactionRepository.AddAsync(mappedResult)), "Transaction Initiated Successfully");

        }

      

        public async Task<GetTransactionDto?> transaction(int id)
        {
            return mapper.Map<GetTransactionDto>(await _transactionRepository.GetByIdAsync(id));

        }

        public async Task<IList<GetTransactionDto>> transactions()
        {
            return mapper.Map<IList<GetTransactionDto>>(await _transactionRepository.GetAllAsync());
        }

        public Task<GetTransactionDto> updatedTransaction(int id)
        {
            throw new NotImplementedException();
        }
    }
}
