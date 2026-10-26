# Controlador Index

> **Controlador**: Clase que recibe la petición, pide los datos y elige la vista

Creamos el archivo `Controllers/RestaurantsController.cs`. El nombre termina en `Controller` pero en la dirección solo aparece `Restaurants`.

```csharp
using Microsoft.AspNetCore.Mvc;
using RestaurantesAspNet.Data;

namespace RestaurantesAspNet.Controllers;

public class RestaurantsController : Controller
{
    private readonly ApplicationDbContext context;

    public RestaurantsController(ApplicationDbContext context)
    {
        this.context = context;
    }

    public IActionResult Index()
    {
        var restaurants = context.Restaurants
            .OrderBy(r => r.Name)
            .ToList();

        return View(restaurants);
    }
}
```

Cada método público del controlador es una acción. La acción `Index` es la que se usa por defecto, así que `/Restaurants` la ejecuta.

El constructor pide el `ApplicationDbContext`. ASP.NET lo crea y nos lo entrega, porque lo registramos en `Program.cs`.

`ToList()` ejecuta la consulta y guarda el resultado en una lista. `View(restaurants)` envía esa lista a la vista `Views/Restaurants/Index.cshtml`, que creamos más adelante.
