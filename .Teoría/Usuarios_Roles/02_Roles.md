# Roles

> **Rol**: Etiqueta que agrupa lo que puede hacer un tipo de usuario, como `Admin` o `User`

Un rol no es una tabla de personas. Es un nombre que dice qué permisos tiene quien lo lleva. Identity guarda los roles en su propia tabla, `AspNetRoles`, y los asigna a cada usuario en otra tabla, `AspNetUserRoles`.

Identity usa tres tablas:

- `AspNetUsers`: una fila por cuenta, con su `Id`, su nombre de login y su email
- `AspNetRoles`: una fila por rol, con su nombre, por ejemplo `Admin`
- `AspNetUserRoles`: una fila por cada asignación, con el `Id` de la cuenta y el `Id` del rol

Si `ana` tiene el rol `Admin` y `pepe` tiene el rol `User`, la tabla `AspNetUserRoles` tiene dos filas. Un usuario puede tener uno, varios o ningún rol. Esa relación es de muchos a muchos, y la gestiona Identity. No hace falta crear ninguna tabla nuestra para los roles.

**Constantes para los nombres**

Escribir `"Admin"` en todas partes es peligroso. Si escribimos `"admim"` por error, el código compila y el administrador no entra. Por eso guardamos los nombres en una clase.

```csharp
namespace RestaurantesAspNet.Models;

public static class RoleNames
{
    public const string Admin = "Admin";
    public const string User = "User";
}
```

Ahora escribimos `RoleNames.Admin` en lugar de `"Admin"`. El compilador nos avisa si nos equivocamos de nombre.

**Constantes en lugar de `enum`**

- Identity guarda el rol como texto, y `[Authorize(Roles = ...)]` espera un texto
- Un `enum` guarda un número por dentro. Para convertirlo a texto hace falta un paso extra
- Un rol creado en la base de datos y que no esté en el `enum` no existe para el código. Con constantes, el código y la base de datos usan el mismo texto

**Renombrar un rol**

Si cambiamos `RoleNames.Admin` de `"Admin"` a `"Administrador"`, el código compila. Pero la base de datos sigue con `"Admin"`, y el seed creará un segundo rol vacío. Nadie sería administrador de verdad.

Por eso, cuando ya hay datos, no se renombra un rol. Si hace falta, se hace con una migración de datos que cambie `AspNetRoles.Name`.

**Cuántos roles tiene un usuario**

Identity permite varios roles por usuario. En el proyecto de restaurantes cada usuario tiene uno, porque la pantalla de administración muestra un solo desplegable. Si quisiéramos varios, la pantalla tendría que mostrar casillas.

```csharp
await userManager.AddToRoleAsync(user, RoleNames.Admin);
await userManager.RemoveFromRoleAsync(user, RoleNames.User);
```

Crear un rol nuevo no necesita migración. Solo se añade una fila en `AspNetRoles`, y el código de arranque puede hacerlo si no existe todavía.
