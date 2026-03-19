using InventoryApp.Dto;
using InventoryApp.Models;
using InventoryApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InventoryApp.Services
{
    public class StockService : IStockService
    {
        private readonly StockDbContext _context;

        public StockService(StockDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Stock>> GetStocksAsync()
        {
            return await _context.Stocks.ToListAsync();
        }

        public async Task<Stock?> GetStockByIdAsync(int id)
        {
            return await _context.Stocks.FindAsync(id);
        }

        public async Task<Stock> CreateStockAsync(StockDto stockDto)
        {
            var stock = new Stock
            {
                StockName = stockDto.Name,
                Category = stockDto.Category,
                CurrentPrice = stockDto.CurrentPrice,
                Actualprice = stockDto.ActualPrice
            };
            _context.Stocks.Add(stock);
            await _context.SaveChangesAsync();
            return stock;
        }

        public async Task<bool> UpdateStockAsync(int id, StockDto stockDto)
        {
            var stock = await _context.Stocks.FindAsync(id);
            if (stock == null) return false;

            stock.StockName = stockDto.Name;
            stock.Category = stockDto.Category;
            stock.CurrentPrice = stockDto.CurrentPrice;
            stock.Actualprice = stockDto.ActualPrice;

            _context.Entry(stock).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteStockAsync(int id)
        {
            var stock = await _context.Stocks.FindAsync(id);
            if (stock == null) return false;

            _context.Stocks.Remove(stock);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
