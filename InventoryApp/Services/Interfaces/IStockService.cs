using InventoryApp.Models;

namespace InventoryApp.Services.Interfaces
{
    public interface IStockService
    {
        Task<IEnumerable<Stock>> GetAllAsync();
        Task<Stock> GetByIdAsync(int id);
        Task AddAsync(Stock stock);
        Task UpdateAsync(Stock stock);
        Task DeleteAsync(int id);
    }
}
