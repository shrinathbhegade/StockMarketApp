namespace InventoryApp.Models
{
    public class Stock
    {
        public int StockId { get; set; }

        public string StockName { get; set; }

        public String Category { get; set; }

        public decimal CurrentPrice { get; set; }

        public decimal Actualprice { get; set; }

        public ICollection<Transaction> Transactions { get; set; }
    }
}