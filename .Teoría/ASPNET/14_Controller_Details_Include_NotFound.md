# Acción Details con Include y NotFound

> **Include**: Orden que carga también las tablas relacionadas, como los platos de un restaurante

La página de detalle muestra un restaurante con su carta. El `id` llega en la dirección, por ejemplo `/Restaurants/Details/2`.

`Include` es un método de Entity Framework Core, así que añadimos `using Microsoft.EntityFrameworkCore;` al principio del controlador.

```csharp
public IActionResult Details(int id)
{
    var restaurant = context.Restaurants
        .Include(r => r.Dishes)
        .FirstOrDefault(r => r.Id == id);

    if (restaurant == null)
    {
        return NotFound();
    }

    return View(restaurant);
}
```

`Include(r => r.Dishes)` carga también los platos. Sin él, la lista llega vacía, porque EF Core no carga las relaciones por su cuenta.

`FirstOrDefault` devuelve `null` si no encuentra ningún restaurante con ese `id`. Con `First` la aplicación lanzaría una excepción, así que preferimos `FirstOrDefault`.

Comprobamos el `null` antes de usar el restaurante. Si no existe, `NotFound()` responde con un error 404, de página no encontrada.

Para probarlo, abrimos `/Restaurants/Details/999`. La respuesta debe ser un 404.
