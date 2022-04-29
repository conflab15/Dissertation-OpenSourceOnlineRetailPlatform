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
        public ProductModel? Product { get; set; } //Product Model
    }
}
