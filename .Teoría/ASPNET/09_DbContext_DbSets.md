# DbContext y DbSet

> **DbContext**: Clase que conecta nuestros modelos con la base de datos

El `DbContext` es el punto de entrada a la base de datos. Cada `DbSet` representa una tabla. Creamos la carpeta `Data` y dentro el archivo `ApplicationDbContext.cs`.

```csharp
using Microsoft.EntityFrameworkCore;
using RestaurantesAspNet.Models;

namespace RestaurantesAspNet.Data;

public sealed class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Restaurant> Restaurants => Set<Restaurant>();
    public DbSet<Dish> Dishes => Set<Dish>();
}
```

El constructor recibe las opciones que configuramos en `Program.cs`, como la conexión a SQLite.

El nombre del `DbSet` es el nombre de la tabla. Así `Restaurants` crea la tabla `Restaurants`, y cada propiedad de `Restaurant` pasa a ser una columna.

Las demás tablas las añadimos de la misma forma. En la lección de usuarios cambiamos la clase base por `IdentityDbContext`, para guardar también las cuentas.
