namespace OnlineRetailPlatformDiss.Models
{
    public class ShoppingBasketVM
    {
        //ViewModel to pass data to the view, to highlight items and the total value of the basket...
        public List<ProductModel>? BasketItems { get; set; }
        public decimal? BasketTotal { get; set; }
        //public decimal BasketTotal => BasketItems.Sum(c => c.Count * c.ProductPrice);
    }
}
