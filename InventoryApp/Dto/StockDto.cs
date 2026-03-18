namespace InventoryApp.Dto
{
    public class StockDto
    {
        public string Name { get; set; }

        public string Category {  get; set; }

        public decimal CurrentPrice {  get; set; }

        public decimal ActualPrice { get; set; }
    }
}
