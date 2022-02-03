namespace OnlineRetailPlatformDiss.ViewModels
{
    public class BasketItemsMessageViewModel
    {
        public string? Message { get; set; } //Message to return to a swal
        public decimal? BasketTotal { get; set; } //Total Price of the Basket
        public int? BasketQuantity { get; set; } //Total amount of the Product in the Basket
        public int? BasketProductTotal { get; set; } //Overall amount of Products in the Basket
    }
}
