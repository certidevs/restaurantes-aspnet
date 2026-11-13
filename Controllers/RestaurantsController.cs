using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantesAspNet.Data;
using RestaurantesAspNet.Models;

namespace RestaurantesAspNet.Controllers;


// OBJETIVO: MOSTRAR RESTAURANTES EN EL NAVEGADOR EN HTML
public class RestaurantsController : Controller
{

    // context permite acceder a la tabla Restaurants
    private readonly ApplicationDbContext context;

    // método constructor para importar la base de datos
    public RestaurantsController(ApplicationDbContext context)
    {
        this.context = context;
    }

    // métodos de comportamiento

    // listar restaurantes con filtros opcionales
    public IActionResult Index(string? search, FoodType? foodType, double? maxPrice)
    {
        var query = context.Restaurants.AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(r => r.Name.ToLower().Contains(search.ToLower()));
        }

        if (foodType != null)
        {
            query = query.Where(r => r.FoodType == foodType);
        }

        if (maxPrice != null)
        {
            query = query.Where(r => r.AveragePrice <= maxPrice);
        }

        ViewBag.Search = search;
        ViewBag.FoodType = foodType;
        ViewBag.maxPrice = maxPrice;

        var restaurants = query.OrderBy(r => r.Name).ToList();
        return View(restaurants);
    }

    // detalle de un restaurante
    public IActionResult Details(int id)
    {
        var restaurant = context.Restaurants
        .Include(r => r.Employees)
        .Include(r => r.Dishes)
        .Include(r => r.Reviews)
        .FirstOrDefault(r => r.Id == id);

        // si no existe el restaurante que buscamos
        if (restaurant == null)
        {
            return NotFound();
        }

        return View(restaurant);
    }

    [HttpGet]
    public IActionResult Create()
    {
        var restaurant = new Restaurant { Active = true };
        return View("Form", restaurant);
    }

    [HttpPost]
    public IActionResult Save(Restaurant restaurant)
    {
        if (!ModelState.IsValid)
        {
            return View("Form", restaurant);
        }

        context.Restaurants.Add(restaurant);
        context.SaveChanges();

        TempData["Message"] = "Restaurante guardado correctamente.";
        return RedirectToAction("Details", new { id = restaurant.Id });
    }


}
