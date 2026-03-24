using InventoryApp.Models;

namespace InventoryApp.Repositories.Interfaces
{
    public interface IStockRepository
    {
        Task<Stock?> GetByIdAsync(int id);
        Task<IEnumerable<Stock>> GetAllAsync();
        Task AddAsync(Stock stock);
        Task UpdateAsync(Stock stock);
        Task DeleteAsync(int id);
    }
}
