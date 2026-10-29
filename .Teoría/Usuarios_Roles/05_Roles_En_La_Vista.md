# Mostrar opciones según el rol

> **Vista**: Página HTML que genera Razor a partir de un modelo. Aquí decidimos qué se ve y qué no

En la vista tenemos disponible `User`, que es la persona que hace la petición. Con él decidimos qué botones y menús aparecen.

- `User.Identity?.IsAuthenticated`: ¿ha iniciado sesión?
- `User.IsInRole(RoleNames.Admin)`: ¿tiene el rol `Admin`?
- `User.Identity?.Name`: el nombre con el que entró

```cshtml
@if (User.IsInRole(RoleNames.Admin))
{
    <a class="btn btn-success" asp-controller="Restaurants" asp-action="Create">Nuevo restaurante</a>
}
```

Para usar `RoleNames` en una vista, `Views/_ViewImports.cshtml` debe tener los `using` correspondientes.

```cshtml
@using RestaurantesAspNet.Models
@using Microsoft.AspNetCore.Authorization
```

**Ocultar no es proteger**

Un botón oculto no protege nada. Cualquiera puede escribir la dirección de la acción en el navegador. Por eso cada acción que ocultamos debe tener su `[Authorize]`.

| Capa | Pregunta | Herramienta | Efecto |
|------|----------|-------------|--------|
| Vista | ¿Qué le enseño? | `User.IsInRole` | Oculta botones y menús |
| Controller | ¿Puede ejecutar esto? | `[Authorize]` | Bloquea la petición |

Una vista bien hecha tiene las dos capas. El botón solo aparece a quien puede usarlo, y la acción está protegida aunque alguien escriba la URL.

**Cambiar el rol de alguien**

Los roles viajan en la cookie, así que `User.IsInRole` mira lo que había en la cookie cuando el usuario inició sesión. Si cambiamos el rol de alguien, el cambio se nota al volver a iniciar sesión, no en el siguiente clic.

La vista y el controller hacen la misma comprobación de rol. Por eso siempre dicen lo mismo.
