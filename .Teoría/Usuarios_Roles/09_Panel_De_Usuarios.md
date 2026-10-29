# Panel de administración de usuarios

> **Servicio**: Clase que contiene la lógica de negocio. El controller recibe la petición y el servicio decide qué hacer

El administrador necesita tres cosas: buscar cuentas, cambiar sus datos y su rol, y activarlas o desactivarlas. Todo el panel está en `UsersController` y en `UserService`.

**UsersController**

```csharp
[Authorize(Roles = RoleNames.Admin)]
public sealed class UsersController : Controller
{
    private readonly UserService userService;

    public UsersController(UserService userService)
    {
        this.userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> Index(string? search)
    {
        return View(new UserIndexViewModel
        {
            Search = search,
            Users = await userService.SearchAsync(search)
        });
    }
}
```

El atributo `[Authorize(Roles = RoleNames.Admin)]` está en la clase. Por eso cualquier acción nueva que añadamos al controller también queda protegida.

**Buscar usuarios**

`SearchAsync` busca por nombre de login, email o nombre visible. Después añade a cada fila su rol.

```csharp
var query = context.Users.AsNoTracking().AsQueryable();
if (!string.IsNullOrWhiteSpace(search))
{
    var value = search.Trim();
    query = query.Where(user =>
        (user.UserName != null && EF.Functions.Like(user.UserName, $"%{value}%")) ||
        (user.Email != null && EF.Functions.Like(user.Email, $"%{value}%")) ||
        (user.DisplayName != null && EF.Functions.Like(user.DisplayName, $"%{value}%")));
}
```

`AsNoTracking` indica que solo vamos a leer. Los datos no se van a guardar, así que EF Core no necesita seguirlos.

**Cambiar datos, contraseña y rol**

Antes de guardar, el servicio aplica las reglas del panel.

- El rol que se elige tiene que existir.
- Un administrador no puede quitarse su propio rol ni desactivarse.

```csharp
if (model.Id == currentAdminId && (model.Role != RoleNames.Admin || !model.IsActive))
{
    return Failure("No puedes quitarte el rol de administrador ni desactivar tu propia cuenta.");
}
```

El `currentAdminId` es el `Id` del administrador conectado, sacado de la cookie. No se lee del formulario.

Si el administrador escribe una contraseña nueva, no necesitamos la antigua. Identity genera un token de restablecimiento y lo usa para cambiarla.

```csharp
var resetToken = await userManager.GeneratePasswordResetTokenAsync(user);
var passwordResult = await userManager.ResetPasswordAsync(user, resetToken, model.NewPassword);
```

El cambio de rol quita todos los roles actuales y pone el nuevo. Como el panel trabaja con un solo rol por usuario, así siempre queda uno.

```csharp
var currentRoles = await userManager.GetRolesAsync(user);
var removeResult = await userManager.RemoveFromRolesAsync(user, currentRoles);
if (!removeResult.Succeeded)
{
    return removeResult;
}

return await userManager.AddToRoleAsync(user, model.Role);
```

Las reglas del panel, como la del último administrador, se usan en varias acciones. Si estuvieran en el controller, habría que copiarlas. En el servicio viven una sola vez, y el controller sigue siendo corto.
