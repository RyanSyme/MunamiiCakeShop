using Munamii.Models;

namespace Munamii.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Cake> CakesOfTheWeek { get; }

        public HomeViewModel(IEnumerable<Cake> cakesOfTheWeek)
        {
            CakesOfTheWeek = cakesOfTheWeek;
        }
    }
}
