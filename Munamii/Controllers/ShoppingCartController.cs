using Microsoft.AspNetCore.Mvc;
using Munamii.Models;
using Munamii.ViewModels;

namespace Munamii.Controllers
{
    public class ShoppingCartController : Controller
    {
        // creates a copy of the cart items
        private readonly ICakeRepository _cakeRepository;
        private readonly IShoppingCart _shoppingCart;
        public ShoppingCartController(ICakeRepository cakeRepository, IShoppingCart shoppingCart)
        {
            _cakeRepository = cakeRepository;
            _shoppingCart = shoppingCart;
        }
        public ViewResult Index()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;

            var shoppingCartViewModel = new ShoppingCartViewModel(
                _shoppingCart, _shoppingCart.GetShoppingCartTotal());

            return View(shoppingCartViewModel);
        }

        // adds item to the cart
        public RedirectToActionResult AddToShoppingCart(int cakeId)
        {
            var selectedCake = _cakeRepository.AllCakes.FirstOrDefault(c => c.CakeId == cakeId);
            
            if(selectedCake != null)
            {
                _shoppingCart.AddToCart(selectedCake);
            }
            return RedirectToAction("Index");
        }

        // removes item from the cart
        public RedirectToActionResult RemoveFromShoppingCart(int cakeId)
        {
            var selectedCake = _cakeRepository.AllCakes.FirstOrDefault(c => c.CakeId == cakeId);

            if (selectedCake != null)
            {
                _shoppingCart.RemoveFromCart(selectedCake);
            }
            return RedirectToAction("Index");
        }
    }

}
