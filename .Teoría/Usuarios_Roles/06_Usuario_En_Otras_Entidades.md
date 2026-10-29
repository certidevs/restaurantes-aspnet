# Guardar quién hace cada cosa

> **Clave foránea a usuario**: Columna que guarda el `Id` de la cuenta que hizo algo, como una reseña o un pedido

Muchas veces lo que guardamos no es información de la persona, sino algo que la persona **hace**. Una reseña, un pedido o una reserva. Cada una es una entidad con una clave que apunta a la cuenta, igual que `Dish` apunta a `Restaurant`.

```csharp
public class Review
{
    public int Id { get; set; }
    public string Comment { get; set; } = string.Empty;
    public int Rating { get; set; }
    public DateTime Date { get; set; }

    public int RestaurantId { get; set; }
    public Restaurant? Restaurant { get; set; }

    public string? UserId { get; set; }
    public ApplicationUser? User { get; set; }
}
```

Estas son las reglas.

- `UserId` es `string`, porque el `Id` de `ApplicationUser` es un texto. Nunca es `int`
- La propiedad `User` es la navegación. Nos permite llegar a la cuenta desde la reseña
- `string?` quiere decir que puede estar vacía. En restaurantes una reseña puede no tener autor, por ejemplo si viene de datos antiguos

**Quién rellena `UserId`**

El `UserId` nunca viene del formulario. Si viniera del HTML, cualquiera podría enviar una reseña a nombre de otra persona. Lo tomamos siempre del servidor, de la cookie de la sesión.

```csharp
[Authorize]
[HttpPost]
public IActionResult Save(Review review)
{
    review.Date = DateTime.Now;
    review.UserId = User.GetRequiredUserId();
    context.Reviews.Add(review);
    context.SaveChanges();

    return RedirectToAction("Details", "Restaurants", new { id = review.RestaurantId });
}
```

`GetRequiredUserId` es un método de extensión que escribimos en `Utilities/ClaimsPrincipalExtensions.cs`. Lee el `Id` que Identity guardó en la cookie.

```csharp
public static string GetRequiredUserId(this ClaimsPrincipal user) =>
    user.FindFirstValue(ClaimTypes.NameIdentifier)
    ?? throw new InvalidOperationException("La sesión autenticada no contiene el identificador del usuario.");
```

**Mostrar el nombre del autor**

Sin `Include`, EF Core no carga la navegación `User`. Hay que pedirla al consultar.

```csharp
var restaurant = await context.Restaurants
    .Include(r => r.Reviews)
        .ThenInclude(review => review.User)
    .FirstOrDefaultAsync(r => r.Id == id);
```

Después, en la vista, mostramos `review.User?.DisplayName`. El `?` evita un error si la reseña no tiene autor.

**No copiar el nombre**

La reseña no guarda el nombre del autor. Si lo copiáramos, el nombre antiguo se quedaría en las reseñas cuando la persona lo cambie. Con la clave, hay una sola fuente de verdad: la cuenta.

Qué pasa con estas claves cuando se elimina una cuenta depende de si son opcionales. En restaurantes son opcionales, así que una reseña se queda sin autor y no se pierde.
