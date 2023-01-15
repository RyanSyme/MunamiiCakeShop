namespace Munamii.Models
{
    public class CategoryRepository: ICategoryRepository
    {
        private readonly MunamiiDbContext _munamiiDbContext;

        public CategoryRepository(MunamiiDbContext munamiiDbContext)
        {
            _munamiiDbContext = munamiiDbContext;
        }

        public IEnumerable<Category> AllCategories => 
            _munamiiDbContext.Categories.OrderBy(p => p.CategoryName);
    }
}
