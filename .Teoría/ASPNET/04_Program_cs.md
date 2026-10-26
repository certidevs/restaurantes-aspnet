# Program.cs paso a paso

> **Program.cs**: Archivo donde preparamos la aplicación antes de arrancarla

Este archivo se ejecuta al empezar la aplicación. Así queda en esta etapa.

```csharp
using Microsoft.EntityFrameworkCore;
using RestaurantesAspNet.Data;

var builder = WebApplication.CreateBuilder(args);

// Los servicios son las piezas que la aplicación necesita
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));
builder.Services.AddControllersWithViews();

var app = builder.Build();

// El orden de estas líneas es el orden en que una petición las atraviesa
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
```

Cada parte tiene una función.

- `CreateBuilder` lee la configuración, como `appsettings.json`.
- `builder.Services` registra las piezas que usamos. `AddDbContext` conecta la base de datos y `AddControllersWithViews` activa los controladores y las vistas.
- `builder.Build()` crea la aplicación con todo lo que hemos registrado.
- Las líneas `app.Use...` definen el camino que sigue cada petición.
- `MapControllerRoute` define las rutas. La ruta por defecto es `controlador/acción/id`.
- `app.Run()` arranca la aplicación.

El orden importa. `UseRouting` va antes que `UseAuthorization`, porque la autorización necesita saber a qué acción llega la petición.

La cadena de conexión está en `appsettings.json`.

```json
"ConnectionStrings": {
  "DefaultConnection": "Data Source=App_Data/restaurantes.db"
}
```

`Data Source` indica el archivo de SQLite. La carpeta `App_Data` tiene que existir antes de arrancar. Si no existe, la creamos a mano.

Este código todavía no compila. Hace falta la clase `ApplicationDbContext`, que creamos en la lección de DbContext.
