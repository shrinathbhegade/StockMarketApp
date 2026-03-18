namespace InventoryApp.Dto
{
    public class TransactionDto
    {
        public int TransactionId { get; set; }

        public int StockId { get; set; }

        public DateTime BuyDate { get; set; }

        public DateTime SellDate { get; set; }

        public decimal Quantity { get; set; }

        public decimal PriceAtTransaction { get; set; }
    }
}
