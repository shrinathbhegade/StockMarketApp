using InventoryApp.Models;
using InventoryApp.Repositories.Interfaces;
using InventoryApp.Services.Interfaces;

namespace InventoryApp.Services
{
    public class StockService : IStockService
    {
        private readonly IStockRepository _stockRepository;

        public StockService(IStockRepository stockRepository)
        {
            _stockRepository = stockRepository;
        }

        public async Task<IEnumerable<Stock>> GetAllAsync()
        {
            return await _stockRepository.GetAllAsync();
        }

        public async Task<Stock> GetByIdAsync(int id)
        {
            return await _stockRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(Stock stock)
        {
            await _stockRepository.AddAsync(stock);
        }

        public async Task UpdateAsync(Stock stock)
        {
            await _stockRepository.UpdateAsync(stock);
        }

        public async Task DeleteAsync(int id)
        {
            await _stockRepository.DeleteAsync(id);
        }
    }
