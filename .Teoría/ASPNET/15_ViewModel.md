# ViewModel

> **ViewModel**: Clase creada solo para los datos de una pantalla

En la lista de restaurantes queremos mostrar también el número de platos. Ese dato no está guardado en la tabla `Restaurants`, así que el modelo `Restaurant` no lo tiene. Por eso creamos un ViewModel.

Creamos el archivo `ViewModels/Restaurants/RestaurantListItemViewModel.cs`.

```csharp
namespace RestaurantesAspNet.ViewModels.Restaurants;

public class RestaurantListItemViewModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool Active { get; set; }
    public int DishCount { get; set; }
}
```

Un ViewModel no es una tabla. EF Core no lo guarda.

Cambiamos el método `Index` del controlador para devolver estos ViewModels. Añadimos también `using RestaurantesAspNet.ViewModels.Restaurants;` al principio del archivo.

```csharp
public IActionResult Index()
{
    var restaurants = context.Restaurants
        .OrderBy(r => r.Name)
        .Select(r => new RestaurantListItemViewModel
        {
            Id = r.Id,
            Name = r.Name,
            Active = r.Active,
            DishCount = r.Dishes.Count
        })
        .ToList();

    return View(restaurants);
}
```

`Select` crea un ViewModel por cada restaurante. `r.Dishes.Count` cuenta los platos directamente en la base de datos.

Así la vista recibe solo los datos que necesita, y no toda la clase `Restaurant`. Si ya tenemos filtros, el `Select` va al final de la consulta, justo antes de `ToList()`.
