# La clase ApplicationUser

> **ApplicationUser**: Clase que representa a cada usuario. Hereda de `IdentityUser` y le añade los datos propios de la aplicación

No escribimos la clase de usuario desde cero. Identity ya trae una clase, `IdentityUser`, con todo lo necesario para una cuenta. Nosotros creamos la nuestra heredando de ella.

```csharp
using Microsoft.AspNetCore.Identity;

namespace RestaurantesAspNet.Models;

public sealed class ApplicationUser : IdentityUser
{
    public string? DisplayName { get; set; }
    public string? AvatarFileName { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public bool IsActive { get; set; } = true;
}
```

Estas son las propiedades que ya trae `IdentityUser`, sin que las escribamos.

- `Id`: la clave de la cuenta. Es un `string`, no un número
- `UserName`: el nombre para entrar, por ejemplo `ana`
- `Email` y `EmailConfirmed`: el correo y si está confirmado
- `PasswordHash`: la contraseña cifrada. Nunca se guarda el texto
- `SecurityStamp`: un valor que cambia al cambiar la contraseña. Identity lo usa para cerrar las sesiones que tenían la contraseña antigua
- `LockoutEnd` y `AccessFailedCount`: los datos del bloqueo tras varios fallos

Estas son las propiedades que añadimos nosotros.

- `DisplayName`: el nombre que se enseña en pantalla. Puede ser distinto al nombre de login.
- `AvatarFileName`: el nombre del archivo de la foto de perfil.
- `CreatedAt`: la fecha en la que se creó la cuenta.
- `IsActive`: si la cuenta puede entrar. Si está en `false`, el login la rechaza aunque la contraseña sea correcta.

**Qué no va en la clase**

- Un campo `Role` como texto. Los roles tienen su propia tabla, y un texto suelto no lo entiende la autorización.
- Una contraseña en otra propiedad. Identity ya la guarda cifrada.
- Datos de un tipo de persona, como `IsTeacher`. Eso es un rol.

**La palabra `sealed`**

La palabra `sealed` impide que alguien herede de la clase. Identity espera siempre la misma clase, así que una subclase haría más difícil saber qué tipo de usuario estamos manejando.

**Conectar la clase con la base de datos**

El contexto cambia su clase base a `IdentityDbContext<ApplicationUser>`. Así EF Core crea las tablas de Identity usando nuestra clase.

```csharp
public sealed class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    // ...
}
```

En `Program.cs` registramos Identity con la misma clase.

```csharp
builder.Services
    .AddIdentity<ApplicationUser, IdentityRole>(options =>
    {
        options.User.RequireUniqueEmail = true;
    })
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();
```

Después de cambiar la clase, creamos la migración y aplicamos los cambios.

```powershell
dotnet ef migrations add CrearUsuarios --output-dir Data/Migrations
dotnet ef database update
```

La clase de usuario es la única que usa Identity. Por eso cualquier campo nuevo que añadamos aquí hay que tratarlo como un cambio de tabla. Si los usuarios que ya existen no tienen valor para ese campo, debe ser opcional (`?`) o tener un valor por defecto, como `IsActive = true`.
