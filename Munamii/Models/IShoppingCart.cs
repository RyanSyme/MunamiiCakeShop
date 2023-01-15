namespace Munamii.Models
{
    public interface IShoppingCart
    {
        void AddToCart(Cake cake);
        int RemoveFromCart(Cake cake);
        List<ShoppingCartItem> GetShoppingCartItems();
        void ClearCart();
        decimal GetShoppingCartTotal();
        List<ShoppingCartItem> ShoppingCartItems { get; set; }
    }
}
