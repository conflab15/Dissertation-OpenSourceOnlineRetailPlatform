namespace OnlineRetailPlatformDiss.Models
{
    public class ShoppingBasketVM
    {
        //ViewModel to pass data to the view, to highlight items and the total value of the basket...
        public List<Baskets>? BasketItems { get; set; }
        public decimal BasketTotal => BasketItems.Sum(c => c.Count * c.Product.ProductPrice);
    }
}
