using InventoryApp.Dto;
using InventoryApp.Models;
using InventoryApp.Repositories.Interfaces;
using InventoryApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InventoryApp.Services
{
    public class TransactionsService : ITransactionsService
    {
        private readonly IUserRepository _userRepo;
        private readonly IStockRepository _stockRepo;
        private readonly ITransactionRepository _transactionRepo;

        public TransactionsService(
            IUserRepository userRepo,
            IStockRepository stockRepo,
            ITransactionRepository transactionRepo)
        {
            _userRepo = userRepo;
            _stockRepo = stockRepo;
            _transactionRepo = transactionRepo;
        }

        public async Task<Transaction?> BuyStockAsync(BuyTransactionDto buyDto)
        {
            var user = await _userRepo.GetByIdAsync(buyDto.UserId); //_context.Users.FirstOrDefaultAsync(u => u.UserId == buyDto.UserId);
            var stock = await _stockRepo.GetByIdAsync(buyDto.StockId); //_context.Stocks.FirstOrDefaultAsync(s => s.StockId == buyDto.StockId);
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
            await _transactionRepo.AddAsync(transaction);
            return transaction;
        }

        public async Task<Transaction?> SellStockAsync(SellTransactionDto sellDto)
        {
            var transaction = await _transactionRepo.GetByIdAsync(sellDto.TransactionId);

            if (transaction == null)
                return null;

            transaction.SellDate = DateTime.UtcNow;
            var user = await _userRepo.GetByIdAsync(transaction.UserId);
            if (user != null)
            {
                user.Balance += transaction.Quantity * transaction.Stock.CurrentPrice;
                await _userRepo.UpdateAsync(user);
            }

            await _transactionRepo.UpdateAsync(transaction);
            return transaction;
        }

        public async Task<IEnumerable<TransactionDto>> GetUserTransactionsAsync(int userId)
        {
            var transactions = await _transactionRepo.GetByUserIdAsync(userId);
            return transactions.Select(t => new TransactionDto
            {
                TransactionId = t.TransactionId,
                StockId = t.StockId,
                BuyDate = t.BuyDate,
                SellDate = t.SellDate,
                Quantity = t.Quantity,
                PriceAtTransaction = t.PriceAtTransaction
            });
        }
    }
}
