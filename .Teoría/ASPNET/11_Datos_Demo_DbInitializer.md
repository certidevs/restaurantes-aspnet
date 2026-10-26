# Datos de demo con DbInitializer

> **Datos de demo**: Registros de prueba que la aplicación crea al arrancar

Una tabla vacía no sirve para probar la aplicación. Para no escribir los datos a mano, creamos la clase `DbInitializer` en el archivo `Data/DbInitializer.cs`.

```csharp
using Microsoft.EntityFrameworkCore;
using RestaurantesAspNet.Models;

namespace RestaurantesAspNet.Data;

public static class DbInitializer
{
    public static void Initialize(ApplicationDbContext context)
    {
        context.Database.Migrate();

        if (!context.Restaurants.Any())
        {
            context.Restaurants.AddRange(
                new Restaurant { Name = "Casa Pepe", AveragePrice = 22.5, Active = true },
                new Restaurant { Name = "Sushi Zen", AveragePrice = 35, Active = true },
                new Restaurant { Name = "La Trattoria", AveragePrice = 18, Active = false });
            context.SaveChanges();
        }
    }
}
```

`Migrate()` aplica las migraciones que faltan, así que la base de datos siempre tiene las tablas. `Any()` comprueba si la tabla ya tiene filas. Si tiene datos, no añadimos nada otra vez.

`AddRange` añade varios restaurantes a la vez y `SaveChanges` los guarda en la base de datos.

Llamamos a `Initialize` desde `Program.cs`, antes de `app.Run()`.

```csharp
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    DbInitializer.Initialize(context);
}
```

`CreateScope` nos da un ámbito para pedir el `DbContext`, igual que lo hace una petición del navegador.
