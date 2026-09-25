

using Microsoft.AspNetCore.Mvc;
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

}