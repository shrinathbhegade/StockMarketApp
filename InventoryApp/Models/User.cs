namespace InventoryApp.Models
{
    public class User
    {
        public int UserId { get; set; }

        public string UserName { get; set; }

        public string Email {  get; set; }  

        public string PasswordHash { get; set; }

        public decimal Balance {  get; set; }

        public string Role { get; set; } = "Admin";

        public ICollection<Transaction> Transactions { get; set; }
    }
}
