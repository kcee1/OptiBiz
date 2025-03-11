using BusinessLogicLayer.IServices;
using Domain.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OptiBizApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "BasicAuthentication")]
    public class TransactionController : ControllerBase
    {
        ITransactionService transactionService;
        public TransactionController(ITransactionService transactionService)
        {
            this.transactionService = transactionService;
        }


        /// <summary>
        /// Gets all transaction
        /// </summary>
        /// <returns>List of transactions</returns>
        [HttpGet] 
        public async Task<IActionResult> Get()
        {
            return Ok(await transactionService.transactions());
        }


        /// <summary>
        /// Gets all transaction
        /// </summary>
        /// <returns>List of transactions</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllUsersForTenant(int tenantId)
        {
            return Ok(await transactionService.transactions());
        }


        /// <summary>
        /// Get transaction
        /// </summary>
        /// <returns>Transactions</returns>
        [HttpGet("id")] 
        public async Task<IActionResult> Get(int id)
        {
            GetTransactionDto? result = await transactionService.transaction(id);
            
            if(result is null)
            {
                return NotFound("Transaction not found");

            }

            return Ok(result);
        }

        /// <summary>
        /// Get transaction
        /// </summary>
        /// <returns>Transactions</returns>
        [HttpPost] 
        public async Task<IActionResult> Create([FromBody] CreateTransactionDto createTransactionDto)
        {
            (GetTransactionDto?, string message) result = await transactionService.createTransactions(createTransactionDto);
            
            if(result.Item1 is null)
            {
                return NotFound(result.message);

            }

            return Ok(result.Item1);
        }





    }
}
