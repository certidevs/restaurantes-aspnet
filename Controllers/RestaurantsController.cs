

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantesAspNet.Data;
using RestaurantesAspNet.Models;

namespace RestaurantesAspNet.Controllers;


// OBJETIVO: MOSTRAR RESTAURANTES EN EL NAVEGADOR EN HTML
public class RestaurantsController : Controller
{

    // context permite acceder a la tabla Restaurants
    private readonly ApplicationDbContext baseDeDatos; 

   // método constructor para importar la base de datos
    public RestaurantsController(ApplicationDbContext baseDeDatos)
    {
        this.baseDeDatos = baseDeDatos;
    }

   // métodos de comportamiento

   // listar restaurantes
   public IActionResult Index()
    {
        var restaurants = baseDeDatos.Restaurants.ToList();
        return View(restaurants);
    }

    // detalle de un restaurante
    public IActionResult Details(int id)
    {
        var restaurant = baseDeDatos.Restaurants
        .Include(r => r.Employees)
        .Include(r => r.Dishes)
        .FirstOrDefault(r => r.Id == id);

        // si no existe el restaurante que buscamos
        if (restaurant == null)
        {
            return NotFound();
        }

        return View(restaurant);
    }

}
