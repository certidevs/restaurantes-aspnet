# Borrar y desactivar usuarios

> **Desactivar**: Marcar una cuenta como inactiva para que no pueda entrar, sin borrar nada de lo que tiene

Hay dos formas de quitar el acceso a alguien. Podemos **borrar** la cuenta, o **desactivarla**. En el proyecto de restaurantes usamos la segunda como norma. El campo `IsActive` de `ApplicationUser` es el que lo indica.

**Desactivar en lugar de borrar**

Un cliente que deja de venir sigue teniendo reseñas y pedidos. Si borramos su cuenta, hay que decidir qué pasa con esos datos. Desactivar evita esa pregunta: la cuenta deja de servir para entrar, y todo lo demás sigue en su sitio.

**Al borrar una cuenta**

Cuando borramos una cuenta, EF Core mira cada clave que apunta a ella.

- Si la clave es opcional (`string?`), la deja en `NULL`. La reseña o el pedido siguen existiendo sin autor
- Si la clave es obligatoria (`string`), por defecto EF Core borra también esas filas. Es el borrado en cascada

En restaurantes, `Review.UserId` y `Order.UserId` son opcionales. Por eso, al borrar un usuario, sus reseñas y pedidos se quedan sin autor, y no se pierde nada.

Las tablas de Identity, como los roles de la cuenta, sí se borran en cascada. Así Identity limpia lo que la cuenta tenía.

**Comprobaciones antes de borrar**

Un administrador no puede borrarse a sí mismo, ni dejar la aplicación sin administradores. El servicio lo comprueba antes de borrar.

```csharp
public async Task<IdentityResult> DeleteAsync(string id, string currentAdminId)
{
    if (id == currentAdminId)
    {
        return Failure("No puedes eliminar tu propia cuenta desde la administración.");
    }

    var user = await userManager.FindByIdAsync(id);
    if (user is null)
    {
        return Failure("El usuario no existe.");
    }

    if (await userManager.IsInRoleAsync(user, RoleNames.Admin))
    {
        var administrators = await userManager.GetUsersInRoleAsync(RoleNames.Admin);
        if (administrators.Count <= 1)
        {
            return Failure("No se puede eliminar al último administrador.");
        }
    }

    return await userManager.DeleteAsync(user);
}
```

El `currentAdminId` viene de la cookie con `User.GetRequiredUserId()`, no del formulario.

**Desactivar en el login**

Una cuenta desactivada no debe poder entrar, y tampoco debe seguir dentro si ya tenía una sesión abierta. El login la rechaza antes de comprobar la contraseña.

```csharp
var user = await userManager.FindByNameAsync(identifier)
    ?? await userManager.FindByEmailAsync(identifier);
if (user is null || !user.IsActive)
{
    ModelState.AddModelError(string.Empty, "El usuario o la contraseña no son válidos.");
    return View(model);
}
```

El mensaje es el mismo que para un usuario que no existe. Así nadie puede saber qué cuentas están desactivadas.

Una sesión ya abierta también se corta. En cada petición se vuelve a comprobar que la cuenta sigue activa.
