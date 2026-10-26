# Filtros de búsqueda

> **Filtro**: Condición que reduce la lista de resultados, como buscar por nombre

Queremos buscar restaurantes por nombre y filtrar por precio. Los valores llegan desde la dirección, por ejemplo `/Restaurants?search=sushi&maxPrice=30`. Cada parámetro de la acción recibe su valor.

```csharp
public IActionResult Index(string? search, double? maxPrice)
{
    var query = context.Restaurants.AsQueryable();

    if (!string.IsNullOrWhiteSpace(search))
    {
        query = query.Where(r => r.Name.ToLower().Contains(search.ToLower()));
    }

    if (maxPrice != null)
    {
        query = query.Where(r => r.AveragePrice <= maxPrice);
    }

    ViewBag.Search = search;
    ViewBag.MaxPrice = maxPrice;

    var restaurants = query.OrderBy(r => r.Name).ToList();
    return View(restaurants);
}
```

Estas son las ideas clave.

- `AsQueryable()` nos deja añadir condiciones antes de ejecutar la consulta.
- Cada `if` añade un filtro, solo si el usuario lo ha rellenado.
- `ToLower()` pasa los dos textos a minúsculas. Así la búsqueda no distingue entre mayúsculas y minúsculas.
- `ViewBag` devuelve los valores a la vista, para que el formulario recuerde lo que escribió.

El formulario de la vista envía estos datos con el método `GET`. Por eso la dirección cambia cada vez que buscamos.
