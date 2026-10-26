# Resumen del circuito de una petición

> **Circuito**: Camino que sigue una petición, desde que el navegador la envía hasta que recibe la página

Seguimos un ejemplo. El usuario escribe esta dirección en el navegador.

```text
http://localhost:5083/Restaurants/Details/2
```

Estos son los pasos que hace ASP.NET.

1. **Ruta**: `Program.cs` compara la dirección con las rutas. Así sabe que el controlador es `Restaurants`, la acción es `Details` y el `id` es `2`.
2. **Controlador**: `RestaurantsController` recibe la petición y ejecuta el método `Details`.
3. **Base de datos**: El controlador pide el restaurante con `id` 2 al `ApplicationDbContext`. EF Core busca la fila en la tabla `Restaurants` de SQLite y devuelve un objeto `Restaurant`.
4. **Vista**: El controlador llama a `View(restaurant)`. ASP.NET busca `Views/Restaurants/Details.cshtml`, que genera el HTML con los datos del restaurante.
5. **Respuesta**: El HTML llega al navegador y el usuario ve la página.

Visto en una línea, el camino queda así.

```text
Navegador -> Ruta -> Controlador -> EF Core -> SQLite -> Modelo -> Vista -> Navegador
```

Cada lección de esta carpeta explica una de estas piezas. Cuando una petición guarda datos, como el envío de un formulario, el circuito es parecido. La diferencia es que el controlador guarda los datos antes de responder.
