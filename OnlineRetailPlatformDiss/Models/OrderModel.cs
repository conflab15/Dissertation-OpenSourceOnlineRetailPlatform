using System.ComponentModel.DataAnnotations;

namespace OnlineRetailPlatformDiss.Models
{
    public class OrderModel
    {
        [Key]
        public Guid OrderId { get; set; }
        [Required]
        public DateTime OrderDate { get; set; } = DateTime.Now; //Automatically set here.
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? Forename { get; set; }
        [Required]
        public string? Surname { get; set; }
        [Required]
        public string? AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; } //Not Required
        [Required]
        public string? Town { get; set; }
        [Required]
        public string? County { get; set; }
        [Required]
        public string? PostCode { get; set; }
        [Required]
        public string? Telephone { get; set; }
        public decimal OrderTotal { get; set; }

        public string? OrderStatus { get; set; }

        //List of Order Lines
        public List<OrderLineModel>? OrderLines { get; set; }
    }
}
