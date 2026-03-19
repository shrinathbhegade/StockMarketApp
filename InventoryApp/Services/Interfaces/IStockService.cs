using InventoryApp.Dto;
using InventoryApp.Models;

namespace InventoryApp.Services.Interfaces
{
    public interface IStockService
    {
        Task<IEnumerable<Stock>> GetStocksAsync();
        Task<Stock?> GetStockByIdAsync(int id);
        Task<Stock> CreateStockAsync(StockDto stockDto);
        Task<bool> UpdateStockAsync(int id, StockDto stockDto);
        Task<bool> DeleteStockAsync(int id);
    }
}
