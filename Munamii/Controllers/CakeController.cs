using Munamii.Models;
using Munamii.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Munamii.Controllers
{
    public class CakeController : Controller
    {
        //Uses an interface to make a copy of the repo
        private readonly ICakeRepository _cakeRepository;
        private readonly ICategoryRepository _categoryRepository;

        public CakeController(ICakeRepository cakeRepository, ICategoryRepository categoryRepository)
        {
            _cakeRepository = cakeRepository;
            _categoryRepository = categoryRepository;
        }

        //passes a list of either the categoried list or full list to the view
        public ViewResult List(string category)
        {
            IEnumerable<Cake> cakes;
            string? currentCategory;

            if (string.IsNullOrEmpty(category))
            {
                cakes = _cakeRepository.AllCakes.OrderBy(c => c.CakeId);
                currentCategory = "All Cakes";
            }
            else
            {
                cakes = _cakeRepository.AllCakes.Where(c => c.Category.CategoryName == category)
                    .OrderBy(c => c.CakeId);
                currentCategory = _categoryRepository.AllCategories.FirstOrDefault(c => c.CategoryName == category)?.CategoryName;
            }

            return View(new CakeListViewModel(cakes, currentCategory));
        }

        // passes the details of the individual product to the view
        public IActionResult Details(int id) 
        {
            var cake = _cakeRepository.GetCakeById(id);
            if(cake == null)
            {

                return NotFound();
            }
            return View(cake);
        }
    }
}
