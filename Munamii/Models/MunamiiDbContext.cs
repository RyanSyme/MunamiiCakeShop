using Microsoft.EntityFrameworkCore;

namespace Munamii.Models
{
    public class MunamiiDbContext: DbContext
    {
        public MunamiiDbContext(DbContextOptions<MunamiiDbContext>
            options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Cake> Cakes { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set;}
        public DbSet<Order> Orders { get; set;}
        public DbSet<OrderDetail> OrderDetails { get; set; }
    }
}
