namespace Munamii.Models
{
    public class ShoppingCartItem
    {
        public int ShoppingCartItemId { get; set; }
        public Cake Cake { get; set; } = default;
        public int Quantity { get; set; }
        public string? ShoppingCartId { get; set; }
    }
}
