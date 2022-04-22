namespace OnlineRetailPlatformDiss.Models
{
    public class ShoppingBasketRemoveVM
    {
        public string? Message { get; set; }
        public decimal BasketTotal { get; set; }
        public int BasketCount { get; set; }
        public int ProductCount { get; set; }
        public Guid DeleteId { get; set; }
    }
}
