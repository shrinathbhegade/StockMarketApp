using InventoryApp.Dto;
using InventoryApp.Models;
using InventoryApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InventoryApp.Services
{
    public class TransactionsService : ITransactionsService
    {
        private readonly StockDbContext _context;

        public TransactionsService(StockDbContext context)
        {
            _context = context;
        }

        public async Task<Transaction?> BuyStockAsync(BuyTransactionDto buyDto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == buyDto.UserId);
            var stock = await _context.Stocks.FirstOrDefaultAsync(s => s.StockId == buyDto.StockId);
            if (user == null || stock == null)
                return null;

            var transaction = new Transaction
            {
                UserId = user.UserId,
                StockId = stock.StockId,
                BuyDate = DateTime.UtcNow,
                Quantity = buyDto.Quantity,
                PriceAtTransaction = stock.CurrentPrice,
            };

            user.Balance -= transaction.Quantity * transaction.PriceAtTransaction;
            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();
            return transaction;
        }

        public async Task<Transaction?> SellStockAsync(SellTransactionDto sellDto)
        {
            var transaction = await _context.Transactions
                .Include(t => t.User)
                .Include(t => t.Stock)
                .FirstOrDefaultAsync(t => t.TransactionId == sellDto.TransactionId);

            if (transaction == null)
                return null;

            transaction.SellDate = DateTime.UtcNow;
            transaction.User.Balance += transaction.Quantity * transaction.Stock.CurrentPrice;

            await _context.SaveChangesAsync();
            return transaction;
        }

        public async Task<IEnumerable<TransactionDto>> GetUserTransactionsAsync(int userId)
        {
            return await _context.Transactions
                .Where(t => t.UserId == userId)
                .Select(t => new TransactionDto
                {
                    TransactionId = t.TransactionId,
                    StockId = t.StockId,
                    BuyDate = t.BuyDate,
                    SellDate = t.SellDate,
                    Quantity = t.Quantity,
                    PriceAtTransaction = t.PriceAtTransaction
                }).ToListAsync();
        }
    }
}
