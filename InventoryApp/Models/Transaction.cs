namespace InventoryApp.Models
{
    public class Transaction
    {
        public int TransactionId { get; set; }

        public int UserId {  get; set; }

        public int StockId { get; set; }

        public DateTime BuyDate { get; set; }

        public DateTime SellDate { get; set; }

        public decimal Quantity { get; set; }

        public decimal PriceAtTransaction { get; set; }

        public User User { get; set; }

        public Stock Stock { get; set; }
    }
}
