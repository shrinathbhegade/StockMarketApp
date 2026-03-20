namespace InventoryApp.Dto
{
    public class BuyTransactionDto
    {
        public int UserId { get; set; }
        public int StockId { get; set; }
        public decimal Quantity { get; set; }
    }
}
