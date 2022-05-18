using System.ComponentModel.DataAnnotations;

namespace OnlineRetailPlatformDiss.Models
{
    public class OrderLineModel
    {
        [Key]
        public Guid OrderLineId { get; set; }
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public int ProductQuantity { get; set; }
        public decimal ProductPrice { get; set; }
        public string? ProductColour { get; set; } //Product Colour Options
        public string? ProductOptions { get; set; } //Product Customisation Options
        public ProductModel? Product { get; set; } //Product Model
        public string? Status { get; set; } //Status of the Order Item, because orders can have items from different businesses.
    }
}
