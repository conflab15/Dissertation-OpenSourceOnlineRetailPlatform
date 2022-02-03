namespace OnlineRetailPlatformDiss.Models
{
    public class BasketItemModel
    {
        //Basket Item Model (A single ProductModel with how many of the Product the User wants to order)
        public ProductModel? Product { get; set; } //The Product...
        public int ProductQuantity { get; set; } //How many of the Product to add to the Basket...
    }
}
