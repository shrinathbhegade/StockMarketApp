using InventoryApp.Dto;
using InventoryApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly StockDbContext _context;

        public TransactionsController(StockDbContext context)
        {
            _context = context;
        }

        [HttpPost("buy")]
        public IActionResult BuyStock(BuyTransactionDto buy1)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserId == buy1.UserId);
            var stock = _context.Stocks.FirstOrDefault(s => s.StockId == buy1.StockId);
            if (user == null || stock == null)
            {
                return NotFound("User or Stock not found");
            }

            var transaction = new Transaction
            {
                UserId = user.UserId,
                StockId = stock.StockId,
                BuyDate = DateTime.UtcNow,
                Quantity = buy1.Quantity,
                PriceAtTransaction = stock.CurrentPrice,
            };

            user.Balance -= transaction.Quantity * transaction.PriceAtTransaction;
            _context.Transactions.Add(transaction);
            _context.SaveChanges();
            return Ok(transaction);
        }

        [HttpPost("sell")]
        public IActionResult SellStock(SellTransactionDto sell1)
        {
            var transaction = _context.Transactions.FirstOrDefault(u => u.TransactionId == sell1.TransactionId);

            if (transaction == null)
            {
                return NotFound("Transaction not found");
            }

            transaction.SellDate = DateTime.UtcNow;
            transaction.User.Balance += transaction.Quantity * transaction.Stock.CurrentPrice;

            _context.SaveChanges();
            return Ok(transaction);
        }

        [HttpGet("user/{id}")]
        public IActionResult GetUserTransaction(int id)
        {
            var transactions = _context.Transactions
                .Where(t => t.UserId == id)
                .Select(t => new TransactionDto
                {
                    TransactionId = t.TransactionId,
                    StockId= t.StockId,
                    BuyDate = t.BuyDate,
                    SellDate = t.SellDate,
                    Quantity = t.Quantity,
                    PriceAtTransaction = t.PriceAtTransaction
                }).ToList();
            return Ok(transactions);
        }

        public class BuyTransactionDto
        {
            public int UserId { get; set; }
            public int StockId { get; set; }
            public decimal Quantity { get; set; }
        }

        public class SellTransactionDto : BuyTransactionDto
        {
            public int TransactionId { get; set; }
        }
    }
}
