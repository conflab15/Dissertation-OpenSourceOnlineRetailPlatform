using System.ComponentModel.DataAnnotations;

namespace OnlineRetailPlatformDiss.Models
{
    public class ProductModel
    {
        [Key]
        public Guid ProductID { get; set; }
        [Required, StringLength(50), Display(Name = "Name")]  
        public string? ProductName { get; set; }
        [Required, StringLength(200), Display(Name = "Description")]
        public string? ProductDescription { get; set; }  
        [Required, Display(Name = "Price")]
        public decimal ProductPrice { get; set; }   
        [Required]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        [Required, Display(Name = "Business Name")]
        public string? BusinessName { get; set; }

        [Required, Display(Name = "Special Options")]
        public bool HasOptions { get; set; } //If the product allows options, a text box shall be displayed within the basket to accept them

        [Display(Name = "Colours or Styles")]
        public List<ProductOptions>? Colours { get; set; } //List of colours for the user to add/amend

        [Required, Display(Name = "Available")]
        public int StockLevel { get; set; }

        //Storing the Image URL as a string to be retrieved from an images folder. 
        [Display(Name = "Product Image")]
        public string? ImageUrl { get; set; }


    }
}
