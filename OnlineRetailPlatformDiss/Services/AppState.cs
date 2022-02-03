namespace OnlineRetailPlatformDiss
{
    public class AppState
    {
        public int? basketQuantity { get; set; } //Basket Total

        public void setBasketQuantity(int quantity)
        {
            basketQuantity = quantity; 
            NotifyStateChanged(); 
        }

        public event Action? OnChange;

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
