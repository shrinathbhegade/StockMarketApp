using InventoryApp.Dto;
using InventoryApp.Models;
using InventoryApp.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InventoryApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StocksController : ControllerBase
    {
        private readonly IStockService _stockService;

        public StocksController(IStockService stockService)
        {
            _stockService = stockService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Stock>>> GetStocks()
        {
            var stocks = await _stockService.GetStocksAsync();
            return Ok(stocks);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<Stock>> GetStock(int id)
        {
            var stock = await _stockService.GetStockByIdAsync(id);
            if (stock == null) return NotFound();
            return Ok(stock);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Stock>> PostStock(StockDto stockDto)
        {
            var stock = await _stockService.CreateStockAsync(stockDto);
            return CreatedAtAction(nameof(GetStock), new { id = stock.StockId }, stock);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutStock(int id, StockDto stockDto)
        {
            var updated = await _stockService.UpdateStockAsync(id, stockDto);
            if (!updated) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteStock(int id)
        {
            var deleted = await _stockService.DeleteStockAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
