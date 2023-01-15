using System.IO.Pipelines;

namespace Munamii.Models
{
    // adds cakes to the database if empty
    public static class DbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            MunamiiDbContext context = 
                applicationBuilder.ApplicationServices.CreateScope()
                .ServiceProvider.GetRequiredService<MunamiiDbContext>();

            if (!context.Categories.Any())
            {
                context.Categories.AddRange(Categories.Select(c => c.Value));
            }

            if (!context.Cakes.Any())
            {
                context.AddRange
                (
                    new Cake { Name = "Red Velvet", Price = 9.95M, Description = "It's very very red", Category = Categories["Cupcakes"], ImageUrl = "https://images.unsplash.com/photo-1614707267537-b85aaf00c4b7?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=387&q=80", IsCakeOfTheWeek = true },
                    new Cake { Name = "Lemon Cupcake", Price = 5.95M, Description = "Delicious lemony cupcake", Category = Categories["Cupcakes"], ImageUrl = "https://images.unsplash.com/photo-1576618148400-f54bed99fcfd?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=580&q=80", IsCakeOfTheWeek = false },
                    new Cake { Name = "Fairy Clown", Price = 4.95M, Description = "Traditional cupcake with a cherry on top", Category = Categories["Cupcakes"], ImageUrl = "https://images.unsplash.com/photo-1599785209796-786432b228bc?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=386&q=80", IsCakeOfTheWeek = false },
                    new Cake { Name = "Hazelnut Delight", Price = 8.95M, Description = "Chocolate and hazelnut goodness", Category = Categories["Cupcakes"], ImageUrl = "https://images.unsplash.com/photo-1603532648955-039310d9ed75?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=387&q=80", IsCakeOfTheWeek = false},
                    new Cake { Name = "Vanilla Waffer", Price = 7.95M, Description = "Buiscut and vanilla cupcake with a waffer topping", Category = Categories["Cupcakes"], ImageUrl = "https://images.unsplash.com/photo-1623246123320-0d6636755796?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=388&q=80", IsCakeOfTheWeek = false },
                    new Cake { Name = "Double Chocolate", Price = 6.95M, Description = "Delicious chocolate cupcake with a creamy chocolate cream topping", Category = Categories["Cupcakes"], ImageUrl = "https://images.unsplash.com/photo-1634586130269-a2defb4da4de?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=387&q=80", IsCakeOfTheWeek = false },
                    new Cake { Name = "Rainbow Cupcake", Price = 8.99M, Description = "Everyone loves a rainbow!", Category = Categories["Cupcakes"], ImageUrl = "https://images.unsplash.com/photo-1596151226641-3bf476a05146?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=387&q=80", IsCakeOfTheWeek = false },
                    new Cake { Name = "Brazilian Caramel", Price = 6.99M, Description = "Caramel, Caramel, Caramel, Caramel!", Category = Categories["Cupcakes"], ImageUrl = "https://images.unsplash.com/photo-1623293605896-6dc523bb02e5?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=435&q=80", IsCakeOfTheWeek = true },
                    new Cake { Name = "Pink Summer", Price = 299.95M, Description = "Traditional wedding cake drapped in a beautiful bouquet of flowers", Category = Categories["Wedding Cakes"], ImageUrl = "https://images.unsplash.com/photo-1535254973040-607b474cb50d?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=387&q=80", IsCakeOfTheWeek = false },
                    new Cake { Name = "Red Rose", Price = 259.95M, Description = "Round white floral four tiered fondant cake", Category = Categories["Wedding Cakes"], ImageUrl = "https://images.unsplash.com/photo-1525257831700-183b9b8bf5c4?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=387&q=80", IsCakeOfTheWeek = false },
                    new Cake { Name = "Natural delight", Price = 199.95M, Description = "White and brown cake on brown woven tray", Category = Categories["Wedding Cakes"], ImageUrl = "https://images.unsplash.com/photo-1627077457288-18c9ad3c68e5?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=382&q=80", IsCakeOfTheWeek = false },
                    new Cake { Name = "Floral Mousse", Price = 219.95M, Description = "Multi-tier floral mousse cake", Category = Categories["Wedding Cakes"], ImageUrl = "https://images.unsplash.com/photo-1522057384400-681b421cfebc?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=387&q=80", IsCakeOfTheWeek = false },
                    new Cake { Name = "Kingdom", Price = 249.95M, Description = "White fondant four tier wedding cake", Category = Categories["Wedding Cakes"], ImageUrl = "https://images.unsplash.com/photo-1574538860416-baadc5d4ec57?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=386&q=80", IsCakeOfTheWeek = false },
                    new Cake { Name = "Three Tier", Price = 199.95M, Description = "Three tier white fondant wedding cake", Category = Categories["Wedding Cakes"], ImageUrl = "https://images.unsplash.com/photo-1561702856-b4ec96854ed8?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=987&q=80", IsCakeOfTheWeek = false },
                    new Cake { Name = "Fig & Champagne", Price = 199.95M, Description = "Fig and champagne wedding cake", Category = Categories["Wedding Cakes"], ImageUrl = "https://images.unsplash.com/photo-1565661834013-d196ca46e14e?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=388&q=80", IsCakeOfTheWeek = false },
                    new Cake { Name = "Stawberry Bloom", Price = 21.95M, Description = "Wedding cake with strawberries and flowers", Category = Categories["Wedding Cakes"], ImageUrl = "https://images.unsplash.com/photo-1623428454614-abaf00244e52?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=387&q=80", IsCakeOfTheWeek = true }
                );
            }

            context.SaveChanges();
        }

        private static Dictionary<string, Category>? categories;

        public static Dictionary<string, Category> Categories
        {
            get
            {
                if (categories == null)
                {
                    var genresList = new Category[]
                    {
                        new Category { CategoryName = "Cupcakes" },
                        new Category { CategoryName = "Wedding Cakes" }
                    };

                    categories = new Dictionary<string, Category>();

                    foreach (Category genre in genresList)
                    {
                        categories.Add(genre.CategoryName, genre);
                    }
                }

                return categories;
            }
        }
    }
}
