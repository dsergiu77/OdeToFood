using Microsoft.AspNetCore.Mvc;
using OdeToFood.Models;
using OdeToFood.Services;
using OdeToFood.ViewModels;

namespace OdeToFood.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRestaurantData restaurantData;
        private readonly IGreeter greeter;

        public HomeController(IRestaurantData restaurantData, IGreeter greeter)
        {
            this.restaurantData = restaurantData;
            this.greeter = greeter;
        }

        public IActionResult Index()
        {
            var model = new HomeIndexVM();
            model.Restaurants = this.restaurantData.GetAll();
            model.MessageOfTheDay = this.greeter.GetMessageOfTheDay();

            //this.HttpContext.Response.ContentType = "application/json";
            return View(model);
        }

        public IActionResult Details(int id)
        {
            var model = this.restaurantData.Get(id);
            if (model == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(model);

        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Restaurant input)
        {
            if (this.ModelState.IsValid)
            {
                var model = this.restaurantData.AddOrUpdate(input);

                return RedirectToAction(nameof(Details), new { id = model.Id });
            }
            else
            {
                // redisplay the form and show the validation errors from ModelState
                return View();
            }
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var model = this.restaurantData.Get(id);
            if (model == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult Update(Restaurant input)
        {
            if (this.ModelState.IsValid)
            {
                var model = this.restaurantData.AddOrUpdate(input);

                return RedirectToAction(nameof(Details), new { id = model.Id /*, foo = "bar"*/ }); // foo will be placed in the query string
            }
            else
            {
                // redisplay the form and show the validation errors from ModelState
                return View();
            }
        }
    }
}
