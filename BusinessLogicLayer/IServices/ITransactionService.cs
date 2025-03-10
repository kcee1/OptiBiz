using Domain.DTO;

namespace BusinessLogicLayer.IServices
{
    public interface ITransactionService
    {
        Task<IList<GetTransactionDto>> transactions();
        Task<(GetTransactionDto?, string message)> createTransactions(CreateTransactionDto transaction);
        Task<GetTransactionDto> transaction(int id);
        Task<GetTransactionDto?> updatedTransaction(int id);


    }
}
