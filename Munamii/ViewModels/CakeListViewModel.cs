using Munamii.Models;

namespace Munamii.ViewModels
{
    public class CakeListViewModel
    {
        public IEnumerable<Cake> Cakes { get; }
        public string? CurrentCategory { get; }

        public CakeListViewModel(IEnumerable<Cake> cakes, string? currentCategory)
        {
            Cakes = cakes;
            CurrentCategory = currentCategory;
        }
    }
}
