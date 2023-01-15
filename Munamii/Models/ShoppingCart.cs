using System.IO.Pipelines;
using Microsoft.EntityFrameworkCore;

namespace Munamii.Models
{
    public class ShoppingCart : IShoppingCart
    {
        private readonly MunamiiDbContext _munamiiDbContext;

        public string? ShoppingCartId { get; set; }

        public List<ShoppingCartItem> ShoppingCartItems { get; set; } = default!;

        private ShoppingCart(MunamiiDbContext munamiiDbContext)
        {
            _munamiiDbContext = munamiiDbContext;
        }

        public static ShoppingCart GetCart(IServiceProvider services)
        {
            ISession? session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext?.Session;

            MunamiiDbContext context = services.GetService<MunamiiDbContext>() ?? throw new Exception("Error initializing");

            string cartId = session?.GetString("CartId") ?? Guid.NewGuid().ToString();

            session?.SetString("CartId", cartId);

            return new ShoppingCart(context) { ShoppingCartId = cartId };
        }

        public void AddToCart(Cake cake)
        {
            var shoppingCartItem =
                    _munamiiDbContext.ShoppingCartItems.SingleOrDefault(
                        s => s.Cake.CakeId == cake.CakeId && s.ShoppingCartId == ShoppingCartId);

            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem
                {
                    ShoppingCartId = ShoppingCartId,
                    Cake = cake,
                    Quantity = 1
                };

                _munamiiDbContext.ShoppingCartItems.Add(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.Quantity++;
            }
            _munamiiDbContext.SaveChanges();
        }

        public int RemoveFromCart(Cake cake)
        {
            var shoppingCartItem =
                    _munamiiDbContext.ShoppingCartItems.SingleOrDefault(
                        s => s.Cake.CakeId == cake.CakeId && s.ShoppingCartId == ShoppingCartId);

            var localAmount = 0;

            if (shoppingCartItem != null)
            {
                if (shoppingCartItem.Quantity > 1)
                {
                    shoppingCartItem.Quantity--;
                    localAmount = shoppingCartItem.Quantity;
                }
                else
                {
                    _munamiiDbContext.ShoppingCartItems.Remove(shoppingCartItem);
                }
            }

            _munamiiDbContext.SaveChanges();

            return localAmount;
        }

        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            return ShoppingCartItems ??=
                       _munamiiDbContext.ShoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId)
                           .Include(s => s.Cake).ToList();
        }

        public void ClearCart()
        {
            var cartItems = _munamiiDbContext
                .ShoppingCartItems
                .Where(cart => cart.ShoppingCartId == ShoppingCartId);

            _munamiiDbContext.ShoppingCartItems.RemoveRange(cartItems);

            _munamiiDbContext.SaveChanges();
        }

        public decimal GetShoppingCartTotal()
        {
            var total = _munamiiDbContext.ShoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId)
                .Select(c => c.Cake.Price * c.Quantity).Sum();
            return total;
        }
    }
}
