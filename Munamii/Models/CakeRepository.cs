using Microsoft.EntityFrameworkCore;

namespace Munamii.Models
{
    public class CakeRepository : ICakeRepository
    {
        private readonly MunamiiDbContext _munamiiDbContext;

        public CakeRepository(MunamiiDbContext munamiiDbContext)
        {
            _munamiiDbContext = munamiiDbContext;
        }

        public IEnumerable<Cake> AllCakes
        {
            get
            {
                return _munamiiDbContext.Cakes.Include(c => c.Category);
            }
        }

        public IEnumerable<Cake> CakesOfTheWeek
        {
            get
            {
                return _munamiiDbContext.Cakes.Include(c => c.Category).Where(p => p.IsCakeOfTheWeek);
            }
        }

        public Cake? GetCakeById(int cakeId)
        {
            return _munamiiDbContext.Cakes.FirstOrDefault(p => p.CakeId == cakeId);
        }

        public IEnumerable<Cake> SearchCake(string searchQuery)
        {
            throw new NotImplementedException();
        }
    }
}
