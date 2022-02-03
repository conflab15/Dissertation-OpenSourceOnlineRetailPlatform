using System.ComponentModel.DataAnnotations;

namespace OnlineRetailPlatformDiss.Models
{
    public class BasketsModel
    {
        //Basket (A basket has an ID, and the Basket SERVICE ID, followed by a product, and the total of the product...
        [Key]
        public Guid BasketID { get; set; } //Unique ID of the Basket for the User

        public Guid BasketServiceID { get; set; } //ID of the Basket Service...
        
        public ProductModel? BasketProduct { get; set; } //Product in the Users Basket

        public int BasketQuantity { get; set; } //Total Number of Items within the Basket

        public decimal BasketPrice { get; set; } //The Total Price of the Basket Items

    }
}
