Circuito completo para añadir funcionalidades en ASP.NET Core MVC

Introducción

Cuando añadimos una nueva funcionalidad a una aplicación ASP.NET Core MVC, normalmente necesitamos conectar varias partes del proyecto.

Por ejemplo, si queremos gestionar restaurantes, no basta con crear una clase Restaurant. También debemos indicar a Entity Framework Core que esa entidad existe, crear la tabla correspondiente en la base de datos, preparar un controlador que obtenga los datos y finalmente crear una vista que los muestre.

De forma simplificada, el proceso es:

flowchart LR
    A[Modelo] --> B[DbContext]
    B --> C[Migración]
    C --> D[Base de datos]
    D --> E[Controller]
    E --> F[View Razor]
    F --> G[Usuario]

Este circuito sigue principalmente la arquitectura MVC.

flowchart TD
    U[Usuario] --> C[Controller]
    C --> M[Model]
    M --> DB[(Base de datos)]
    DB --> M
    M --> C
    C --> V[View]
    V --> U

En MVC:

* Model representa los datos y las reglas de la aplicación.
* View se encarga de mostrar información al usuario.
* Controller actúa como intermediario entre el usuario, los modelos y las vistas.

⸻

Parte 1. Gestión de restaurantes

1. Models/ — Crear la entidad

El primer paso consiste en definir qué información representa un restaurante.

Creamos:

Models/Restaurant.cs

Un ejemplo podría ser:

public class Restaurant
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal AveragePrice { get; set; }
    public bool Active { get; set; }
    public int NumberEmployees { get; set; }
}

La clase Restaurant representa una entidad de nuestra aplicación.

Cada objeto creado a partir de esta clase representa un restaurante.

Por ejemplo:

Restaurant restaurante = new Restaurant
{
    Id = 1,
    Name = "Restaurante Central",
    AveragePrice = 25,
    Active = true,
    NumberEmployees = 10
};

Conceptualmente:

Restaurant
│
├── Id
├── Name
├── AveragePrice
├── Active
└── NumberEmployees

Cuando usamos Entity Framework Core, esta clase puede convertirse posteriormente en una tabla de la base de datos.

flowchart LR
    A["Clase C# Restaurant"] --> B["Entity Framework Core"]
    B --> C["Tabla Restaurants"]
    C --> D["Id"]
    C --> E["Name"]
    C --> F["AveragePrice"]
    C --> G["Active"]
    C --> H["NumberEmployees"]

⸻

2. Data/ — Registrar la entidad en DbContext

Después de crear el modelo, debemos indicarle a Entity Framework Core que queremos gestionar esta entidad.

Abrimos:

Data/ApplicationDbContext.cs

y añadimos:

public DbSet<Restaurant> Restaurants => Set<Restaurant>();

Un ejemplo simplificado sería:

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public DbSet<Restaurant> Restaurants => Set<Restaurant>();
}

¿Qué es DbContext?

DbContext es uno de los componentes principales de Entity Framework Core.

Actúa como intermediario entre nuestra aplicación C# y la base de datos.

flowchart LR
    A["Código C#"] --> B["ApplicationDbContext"]
    B --> C["Entity Framework Core"]
    C --> D[(Base de datos)]

Por ejemplo:

baseDeDatos.Restaurants

representa la colección de restaurantes almacenados en la base de datos.

Podemos imaginarlo así:

ApplicationDbContext
│
├── Restaurants
├── Platos
├── Usuarios
└── Otras entidades

⸻

3. Crear las migraciones

Una vez creado el modelo y registrado el DbSet, debemos actualizar la estructura de la base de datos.

Aquí aparecen las migraciones.

¿Qué es una migración?

Una migración es un conjunto de instrucciones generado por Entity Framework Core para modificar la estructura de la base de datos.

Por ejemplo:

Modelo C#
      ↓
Detectar cambios
      ↓
Crear migración
      ↓
Generar instrucciones SQL
      ↓
Modificar base de datos

El flujo completo es:

flowchart TD
    A["Modificamos Restaurant.cs"] --> B["EF Core detecta cambios"]
    B --> C["dotnet ef migrations add"]
    C --> D["Se crea una migración"]
    D --> E["dotnet ef database update"]
    E --> F[(Base de datos actualizada)]

Crear la migración

Ejecutamos:

dotnet ef migrations add CreateRestaurants

Este comando crea una nueva migración.

Normalmente aparecerá una carpeta similar a:

Migrations/
├── 20260925_CreateRestaurants.cs
├── 20260925_CreateRestaurants.Designer.cs
└── ApplicationDbContextModelSnapshot.cs

No debemos modificar estos archivos manualmente salvo que sepamos exactamente qué estamos haciendo.

⸻

Aplicar la migración

Después ejecutamos:

dotnet ef database update

Este comando aplica las migraciones pendientes sobre la base de datos.

Antes:

Base de datos
Usuarios
Pedidos

Después:

Base de datos
Usuarios
Pedidos
Restaurants

⸻

4. Insertar datos de prueba

Este paso es opcional.

Podemos insertar datos iniciales para poder probar la aplicación sin tener que introducir restaurantes manualmente.

Podemos utilizar:

Data/DbInitializer.cs

Por ejemplo:

context.Restaurants.AddRange(
    new Restaurant
    {
        Name = "Restaurante Central",
        AveragePrice = 25,
        Active = true,
        NumberEmployees = 10
    },
    new Restaurant
    {
        Name = "La Cocina Verde",
        AveragePrice = 18,
        Active = true,
        NumberEmployees = 6
    }
);
context.SaveChanges();

¿Qué hace AddRange()?

Añade varios objetos al contexto de Entity Framework Core.

flowchart LR
    A["new Restaurant"] --> C["AddRange"]
    B["new Restaurant"] --> C
    C --> D["DbContext"]
    D --> E["SaveChanges()"]
    E --> F[(Base de datos)]

⸻

¿Qué hace SaveChanges()?

SaveChanges() envía a la base de datos los cambios realizados en el contexto.

Por ejemplo:

context.Restaurants.Add(restaurante);

todavía no garantiza que el restaurante haya sido guardado definitivamente.

Después debemos ejecutar:

context.SaveChanges();

El flujo sería:

Crear objeto
    ↓
Add()
    ↓
DbContext
    ↓
SaveChanges()
    ↓
INSERT SQL
    ↓
Base de datos

⸻

5. Controllers/ — Crear el controlador

Creamos:

Controllers/RestaurantsController.cs

El controlador recibe las peticiones HTTP relacionadas con restaurantes.

Ejemplo:

public class RestaurantsController : Controller
{
    private readonly ApplicationDbContext baseDeDatos;
    public RestaurantsController(ApplicationDbContext baseDeDatos)
    {
        this.baseDeDatos = baseDeDatos;
    }
    public IActionResult Index()
    {
        var restaurantes = baseDeDatos.Restaurants.ToList();
        return View(restaurantes);
    }
}

⸻

Explicación del controlador

Constructor

public RestaurantsController(ApplicationDbContext baseDeDatos)
{
    this.baseDeDatos = baseDeDatos;
}

ASP.NET Core proporciona automáticamente una instancia de ApplicationDbContext.

Esto se denomina inyección de dependencias.

flowchart LR
    A["ASP.NET Core"] --> B["ApplicationDbContext"]
    B --> C["RestaurantsController"]

No necesitamos crear manualmente:

new ApplicationDbContext();

El framework se encarga de gestionar esa dependencia.

⸻

Método Index()

public IActionResult Index()
{
    var restaurantes = baseDeDatos.Restaurants.ToList();
    return View(restaurantes);
}

Primero obtenemos los restaurantes:

baseDeDatos.Restaurants.ToList();

Entity Framework Core convierte esa instrucción en una consulta SQL equivalente aproximadamente a:

SELECT *
FROM Restaurants;

Después:

return View(restaurantes);

envía los datos a la vista.

El recorrido es:

sequenceDiagram
    participant U as Usuario
    participant C as RestaurantsController
    participant EF as Entity Framework
    participant DB as Base de datos
    participant V as Index.cshtml
    U->>C: GET /Restaurants
    C->>EF: Restaurants.ToList()
    EF->>DB: SELECT * FROM Restaurants
    DB-->>EF: Restaurantes
    EF-->>C: List<Restaurant>
    C->>V: View(restaurantes)
    V-->>U: HTML

⸻

6. Views/Restaurants/ — Crear la vista

Creamos:

Views/
└── Restaurants/
    └── Index.cshtml

La vista se encarga de mostrar los restaurantes al usuario.

Un ejemplo sencillo sería:

@model List<Restaurant>
<h1>Restaurantes</h1>
@foreach (var restaurant in Model)
{
    <div>
        <h2>@restaurant.Name</h2>
        <p>
            Precio medio:
            @restaurant.AveragePrice €
        </p>
        <p>
            Número de empleados:
            @restaurant.NumberEmployees
        </p>
    </div>
}

La variable:

Model

contiene los datos enviados desde el controlador.

Es decir:

Controller
return View(restaurantes);
        ↓
View
@model List<Restaurant>
        ↓
Model contiene restaurantes

⸻

Flujo completo de una petición

Cuando el usuario accede a:

/Restaurants

ASP.NET Core realiza aproximadamente este recorrido:

flowchart TD
    A["Navegador"] --> B["GET /Restaurants"]
    B --> C["RestaurantsController"]
    C --> D["Index()"]
    D --> E["ApplicationDbContext"]
    E --> F["Restaurants.ToList()"]
    F --> G[(Base de datos)]
    G --> H["List<Restaurant>"]
    H --> I["return View(restaurantes)"]
    I --> J["Index.cshtml"]
    J --> K["HTML"]
    K --> A

⸻

7. Añadir enlace al menú de navegación

Opcionalmente podemos modificar:

Views/Shared/_Layout.cshtml

para añadir un enlace a la sección de restaurantes.

Por ejemplo:

<a asp-controller="Restaurants"
   asp-action="Index">
    Restaurantes
</a>

Es preferible usar:

asp-controller

y:

asp-action

en lugar de escribir directamente:

<a href="/Restaurants">

porque ASP.NET Core puede generar la URL automáticamente.

⸻

Estructura final de restaurantes

Nuestro proyecto podría quedar así:

Proyecto/
│
├── Controllers/
│   └── RestaurantsController.cs
│
├── Data/
│   ├── ApplicationDbContext.cs
│   └── DbInitializer.cs
│
├── Models/
│   └── Restaurant.cs
│
├── Views/
│   ├── Restaurants/
│   │   └── Index.cshtml
│   │
│   └── Shared/
│       └── _Layout.cshtml
│
└── Migrations/

Esquemáticamente:

flowchart TD
    A["Restaurant.cs"] --> B["ApplicationDbContext"]
    B --> C["Migración"]
    C --> D[(Restaurants)]
    D --> E["RestaurantsController"]
    E --> F["Index.cshtml"]
    F --> G["Usuario"]

⸻

Parte 2. La semana que viene: Platos

El procedimiento para crear los platos será prácticamente el mismo.

Esta repetición es importante porque representa uno de los patrones habituales de desarrollo en aplicaciones CRUD.

⸻

1. Models/ — Crear Plato

Creamos:

Models/Plato.cs

Por ejemplo:

public class Plato
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Imagen { get; set; }
    public decimal Price { get; set; }
    public bool Active { get; set; }
    public Restaurant Restaurant { get; set; }
}

El modelo podría representarse así:

classDiagram
class Restaurant {
    int Id
    string Name
    decimal AveragePrice
    bool Active
    int NumberEmployees
}
class Plato {
    int Id
    string Name
    string Description
    string Imagen
    decimal Price
    bool Active
}
Restaurant "1" --> "*" Plato

Este esquema representa una posible relación:

1 restaurante
      │
      └──── puede tener ────> muchos platos

⸻

2. Registrar Plato en ApplicationDbContext

En:

Data/ApplicationDbContext.cs

añadimos:

public DbSet<Plato> Platos => Set<Plato>();

Ahora nuestro contexto podría tener:

public DbSet<Restaurant> Restaurants => Set<Restaurant>();
public DbSet<Plato> Platos => Set<Plato>();

Conceptualmente:

flowchart TD
    A["ApplicationDbContext"]
    A --> B["Restaurants"]
    A --> C["Platos"]
    B --> D[(Tabla Restaurants)]
    C --> E[(Tabla Platos)]

⸻

3. Crear migración para Platos

Ejecutamos:

dotnet ef migrations add CreatePlatos

Después:

dotnet ef database update

El resultado esperado será:

Base de datos
│
├── Restaurants
│
└── Platos

⸻

4. Insertar platos de prueba

Podemos utilizar:

Data/DbInitializer.cs

Por ejemplo:

context.Platos.AddRange(
    new Plato
    {
        Name = "Pizza Margarita",
        Description = "Tomate, mozzarella y albahaca",
        Price = 9.50m,
        Active = true
    },
    new Plato
    {
        Name = "Hamburguesa",
        Description = "Carne, queso y verduras",
        Price = 12.00m,
        Active = true
    }
);
context.SaveChanges();

⸻

5. Crear PlatosController

Creamos:

Controllers/PlatosController.cs

Por ejemplo:

public class PlatosController : Controller
{
    private readonly ApplicationDbContext baseDeDatos;
    public PlatosController(ApplicationDbContext baseDeDatos)
    {
        this.baseDeDatos = baseDeDatos;
    }
    public IActionResult Index()
    {
        var platos = baseDeDatos.Platos.ToList();
        return View(platos);
    }
}

Su comportamiento es prácticamente idéntico al controlador de restaurantes.

flowchart LR
    A["GET /Platos"] --> B["PlatosController"]
    B --> C["Platos.ToList()"]
    C --> D[(Base de datos)]
    D --> E["List<Plato>"]
    E --> F["Index.cshtml"]

⸻

6. Crear Views/Platos/

Creamos:

Views/
└── Platos/
    └── Index.cshtml

Ejemplo:

@model List<Plato>
<h1>Platos</h1>
@foreach (var plato in Model)
{
    <div>
        <h2>@plato.Name</h2>
        <p>
            @plato.Description
        </p>
        <p>
            Precio:
            @plato.Price €
        </p>
    </div>
}

⸻

7. Añadir Platos al menú

En:

Views/Shared/_Layout.cshtml

podemos añadir:

<a asp-controller="Platos"
   asp-action="Index">
    Platos
</a>

⸻

Circuito general para crear una nueva entidad

Este procedimiento puede reutilizarse cuando queramos añadir:

* Restaurantes
* Platos
* Usuarios
* Categorías
* Pedidos
* Reservas
* Empleados

El patrón general será:

flowchart TD
    A["1. Crear Model"]
    B["2. Añadir DbSet"]
    C["3. Crear migración"]
    D["4. Actualizar base de datos"]
    E["5. Insertar datos opcionales"]
    F["6. Crear Controller"]
    G["7. Crear Views"]
    H["8. Añadir navegación"]
    A --> B
    B --> C
    C --> D
    D --> E
    E --> F
    F --> G
    G --> H

⸻

Resumen académico

En una aplicación ASP.NET Core MVC con Entity Framework Core podemos distinguir tres grandes bloques.

Persistencia

Gestiona los datos:

Models
   ↓
DbContext
   ↓
Entity Framework Core
   ↓
Base de datos

Lógica de petición

Gestionada principalmente por los controladores:

HTTP Request
     ↓
Controller
     ↓
Consultar datos
     ↓
Preparar respuesta

Presentación

Gestionada por Razor:

Controller
    ↓
View
    ↓
Razor
    ↓
HTML
    ↓
Navegador

Todo unido queda así:

flowchart LR
    USER["Usuario"]
    CONTROLLER["Controller"]
    MODEL["Model"]
    EF["Entity Framework Core"]
    DB[(Base de datos)]
    VIEW["Razor View"]
    HTML["HTML"]
    USER -->|Petición HTTP| CONTROLLER
    CONTROLLER --> MODEL
    MODEL --> EF
    EF --> DB
    DB --> EF
    EF --> MODEL
    MODEL --> CONTROLLER
    CONTROLLER --> VIEW
    VIEW --> HTML
    HTML --> USER

⸻

Chuleta rápida

Cada vez que quieras crear una nueva funcionalidad basada en una entidad puedes seguir esta secuencia:

1. Crear modelo
        ↓
2. Añadir DbSet
        ↓
3. Crear migración
        ↓
4. Actualizar BD
        ↓
5. Añadir datos de prueba
        ↓
6. Crear controlador
        ↓
7. Crear vista
        ↓
8. Añadir enlace al menú

Comandos principales:

dotnet ef migrations add NombreMigracion
dotnet ef database update

Consulta habitual desde un controlador:

baseDeDatos.Restaurants.ToList();

Enviar los datos a una vista:

return View(restaurantes);

Recibirlos desde Razor:

@model List<Restaurant>

⸻

Idea fundamental

No debemos pensar que Model, Controller, View y base de datos son elementos aislados.

Todos forman parte de un mismo circuito:

flowchart LR
    A["Usuario"]
    --> B["Controller"]
    --> C["Entity Framework Core"]
    --> D[(Base de datos)]
    --> C
    --> B
    --> E["View"]
    --> A

Por tanto, cuando añadimos una nueva funcionalidad a una aplicación ASP.NET Core MVC, normalmente estamos construyendo una cadena que conecta:

clase C# → Entity Framework Core → base de datos → controlador → Razor → navegador.