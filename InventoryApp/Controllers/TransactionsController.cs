using InventoryApp.Dto;
using InventoryApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InventoryApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionsService _transactionsService;

        public TransactionsController(ITransactionsService transactionsService)
        {
            _transactionsService = transactionsService;
        }

        [HttpPost("buy")]
        public async Task<IActionResult> BuyStock(BuyTransactionDto buy1)
        {
            var transaction = await _transactionsService.BuyStockAsync(buy1);
            if (transaction == null)
                return NotFound("User or Stock not found");
            return Ok(transaction);
        }

        [HttpPost("sell")]
        public async Task<IActionResult> SellStock(SellTransactionDto sell1)
        {
            var transaction = await _transactionsService.SellStockAsync(sell1);
            if (transaction == null)
                return NotFound("Transaction not found");
            return Ok(transaction);
        }

        [HttpGet("user/{id}")]
        public async Task<IActionResult> GetUserTransaction(int id)
        {
            var transactions = await _transactionsService.GetUserTransactionsAsync(id);
            return Ok(transactions);
        }
    }
}
