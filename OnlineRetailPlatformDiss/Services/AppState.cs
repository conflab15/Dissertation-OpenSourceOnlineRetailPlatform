
namespace OnlineRetailPlatformDiss.Services
{
    public class AppState
    {
        public int? BasketCount { get; set; } //Total Items in Basket

        public void SetBasketCount(int count)
        {
            BasketCount = count; //Assigning the basket count
            NotifyStateChanged();
        }

        public event Action? OnChange; //When Changed...
        private void NotifyStateChanged() => OnChange?.Invoke(); //Notify that the change has been made...
    }
}
