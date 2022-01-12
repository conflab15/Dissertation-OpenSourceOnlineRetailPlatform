using System.ComponentModel.DataAnnotations;

namespace OnlineRetailPlatformDiss.Models
{
    public class Product
    {
        [Key]
        public Guid ProductID { get; set; }
        [Required, StringLength(20), Display(Name = "Name")]  
        public string ProductName { get; set; }
        [Required, StringLength(200), Display(Name = "Description")]
        public string ProductDescription { get; set; }  
        [Required, Display(Name = "Price")]
        public decimal ProductPrice { get; set; }   
        [Display(Name = "Promotional Price")]
        public decimal PromotionalPrice { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        [Required, Display(Name = "Available")]
        public int StockLevel { get; set; }


    }
}
