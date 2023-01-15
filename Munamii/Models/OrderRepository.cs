namespace Munamii.Models
{
    public class OrderRepository : IOrderRepository
    {
        private readonly MunamiiDbContext _munamiiDbContext;
        private readonly IShoppingCart _shoppingCart;

        public OrderRepository(MunamiiDbContext munamiiDbContext, IShoppingCart shoppingCart)
        {
            _munamiiDbContext = munamiiDbContext;
            _shoppingCart = shoppingCart;
        }

        public void CreateOrder(Order order)
        {
            order.OrderPlaced = DateTime.Now;

            List<ShoppingCartItem>? shoppingCartItems = _shoppingCart.ShoppingCartItems;
            order.OrderTotal = _shoppingCart.GetShoppingCartTotal();

            order.OrderDetails = new List<OrderDetail>();

            foreach(ShoppingCartItem? shoppingCartItem in shoppingCartItems)
            {
                var orderDetail = new OrderDetail
                {
                    Quantity = shoppingCartItem.Quantity,
                    CakeId = shoppingCartItem.Cake.CakeId,
                    Price = shoppingCartItem.Cake.Price
                };

                order.OrderDetails.Add(orderDetail);
            }

            _munamiiDbContext.Orders.Add(order);

            _munamiiDbContext.SaveChanges();
        }
    }
}
