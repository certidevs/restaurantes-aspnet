# Relaciones entre tablas

> **Relación**: Vínculo entre dos modelos, como un restaurante y sus platos

Un restaurante tiene muchos platos. Un plato pertenece a un solo restaurante. A esto lo llamamos una relación uno a muchos.

Para unir las dos tablas, el plato guarda el `Id` de su restaurante. Creamos `Models/Dish.cs`.

```csharp
namespace RestaurantesAspNet.Models;

public class Dish
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public double Price { get; set; }

    public int RestaurantId { get; set; }
    public Restaurant? Restaurant { get; set; }
}
```

Estas dos propiedades hacen la relación.

- `RestaurantId` es la clave foránea. Guarda el `Id` del restaurante.
- `Restaurant` es la propiedad de navegación. Nos permite llegar al restaurante desde el plato.

En el restaurante añadimos la lista de platos.

```csharp
public List<Dish> Dishes { get; set; } = new();
```

EF Core entiende la relación porque los nombres siguen una convención. La propiedad `Restaurant` y la propiedad `RestaurantId` bastan. No hace falta configurar nada más.

Lo mismo pasa con `Employee` y con `Review`.

Como `RestaurantId` no admite `null`, al borrar un restaurante EF Core también borra sus platos. Lo vemos en la lección de editar y borrar.
