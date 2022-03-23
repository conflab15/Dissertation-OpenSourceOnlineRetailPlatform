using System.ComponentModel.DataAnnotations;

namespace OnlineRetailPlatformDiss.Models
{
    public class ShoppingBasketProductModel
    {
        [Key]
        public int? ShoppingCartItemId { get; set; }
        public ProductModel? Product { get; set; }
        public int Quantity { get; set; }
        public string? ShoppingBasketId { get; set; }
    }
}
