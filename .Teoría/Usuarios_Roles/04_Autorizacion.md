# Proteger acciones con Authorize

> **Autorización**: Comprobación de que el usuario que hace una petición puede ejecutar esa acción

Hay dos preguntas distintas. La **autenticación** responde a "¿quién eres?", y es el login. La **autorización** responde a "¿puedes hacer esto?", y es lo que hacemos con `[Authorize]`.

Estos son los atributos que usamos.

| Atributo | Quién puede entrar |
|----------|--------------------|
| Sin atributo | Cualquiera, aunque no haya login |
| `[Authorize]` | Cualquier usuario que haya iniciado sesión |
| `[Authorize(Roles = RoleNames.Admin)]` | Solo usuarios con el rol `Admin` |
| `[AllowAnonymous]` | Permite entrar sin login, aunque el controller lo pida |

El atributo se puede poner en el controller, y entonces afecta a todas sus acciones.

```csharp
[Authorize(Roles = RoleNames.Admin)]
public sealed class UsersController : Controller
{
    // Todas las acciones de este controller son solo para administradores
}
```

**Si el usuario no cumple la condición**

- Si el usuario no ha iniciado sesión, ASP.NET lo manda a `/account/login`
- Si ha iniciado sesión pero no tiene el rol, lo manda a `/account/accessdenied`

Esas dos rutas se configuran en `Program.cs`.

```csharp
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/account/login";
    options.AccessDeniedPath = "/account/accessdenied";
});
```

**El orden en el pipeline**

`UseAuthentication` lee la cookie y construye al usuario. `UseAuthorization` comprueba los atributos. Tienen que estar en este orden.

```csharp
app.UseAuthentication();
app.UseAuthorization();
```

Si los invertimos, el usuario llega vacío a la autorización, y ningún rol funciona.

**Los controllers de login**

`AccountController` no lleva `[Authorize]` en la clase. Si lo llevara, nadie podría iniciar sesión. Por eso Login, Register y AccessDenied llevan `[AllowAnonymous]`, y Logout lleva `[Authorize]`.

```csharp
[Authorize]
[HttpPost]
public async Task<IActionResult> Logout()
{
    await signInManager.SignOutAsync();
    return RedirectToAction(nameof(Login), new { loggedOut = true });
}
```

**Logout con `POST`**

Si cerrar sesión fuera un enlace `GET`, cualquier web externa podría cerrar la sesión del usuario con una imagen. Con `POST` no es posible hacerlo desde un enlace.

Además, `Program.cs` registra un filtro global que exige el token antifalsificación en todos los `POST`.

```csharp
builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
});
```

Así un formulario de otra web no puede enviar un `POST` a nuestra aplicación en nombre del usuario.

Aunque el usuario escriba `/Users` a mano, el atributo lo para. Por eso la protección de verdad siempre va en el controller.
