namespace OnlineRetailPlatformDiss.Models
{
    public class Baskets
    {
        public int Id { get; set; }
        public string? BasketId { get; set; }
        public Guid ProductId { get; set; }
        public int Count { get; set; }
        public DateTime DateCreated { get; set; }

        public ProductModel? Product { get; set; }
    }
}
