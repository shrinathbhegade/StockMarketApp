using InventoryApp.Dto;

namespace InventoryApp.Services.Interfaces
{
    public interface ITransactionsService
    {
        Task<Models.Transaction?> BuyStockAsync(BuyTransactionDto buyDto);
        Task<Models.Transaction?> SellStockAsync(SellTransactionDto sellDto);
        Task<IEnumerable<TransactionDto>> GetUserTransactionsAsync(int userId);
    }
}
