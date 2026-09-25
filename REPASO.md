


CIRCUITO COMPLETO PARA AÑADIR FUNCIONALIDADES:

1. Models/ (Clase / Modelo / Entidad)
    * Restaurant.cs: Id, Name, AveragePrice, Active, NumberEmployees

2. Data/
    * ApplicationDbContext.cs: `public DbSet<Restaurant> Restaurants => Set<Restaurant>();`

3. Terminal: (migraciones)
    * `dotnet ef migrations add CreateRestaurants`
    * `dotnet ef database update`

4. Insertar datos demo (opcional):
    * Data/DbInitializer.cs 
        * `context.Restaurants.AddRange(... new Restaurant ...)`
        * `context.saveChanges()`

5. Controllers/
    * RestaurantsController.cs
        * método Index
            * `baseDeDatos.Restaurants.ToList()`

6. Views/Restaurants/ (Plantillas Razor)
    * Index.cshtml
    * Shared/_Layout.cshtml (Opcional, añadir url `/Restaurantes` en la navbar)


----

LA SEMANA QUE VIENE:

1. Models/
    * Plato.cs: Id, Name, Description, Imagen, Price, Active, Restaurant

2. Data/
    * ApplicationDbContext.cs: `public DbSet<Plato> Platos => Set<Plato>();`

3. Terminal: (migraciones)
    * `dotnet ef migrations add CreatePlatos`
    * `dotnet ef database update`

4. Insertar datos demo (opcional):
    * Data/DbInitializer.cs 
        * `context.Platos.AddRange(... new Plato ...)`
        * `context.saveChanges()`

5. Controllers/
    * PlatosController.cs
        * método Index
            * `baseDeDatos.Platos.ToList()`


6. Views/Platos/ (Plantillas Razor)
    * Index.cshtml
    * Shared/_Layout.cshtml (Opcional, añadir url `/Platos` en la navbar)