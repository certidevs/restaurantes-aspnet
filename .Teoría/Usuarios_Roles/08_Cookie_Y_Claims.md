# La cookie y los claims

> **Claim**: Dato sobre el usuario que viaja dentro de la cookie, como su `Id`, su nombre o sus roles

Cuando el usuario inicia sesión, Identity crea una cookie. El navegador la guarda y la envía en cada petición. Dentro de la cookie van los **claims**, pares de datos que describen al usuario.

```
NameIdentifier  = "8f3b..."        (el Id de la cuenta)
Name            = "ana"            (el nombre de login)
Role            = "Admin"          (un claim por cada rol)
SecurityStamp   = "..."            (para invalidar sesiones)
```

En cada petición, ASP.NET lee la cookie y crea con esos claims el objeto `User`. Por eso podemos escribir `User.IsInRole(...)` sin consultar la base de datos.

La cookie va cifrada. El usuario no puede leer ni cambiar los claims sin romperla. Tampoco podemos cambiar un rol en la cookie desde el navegador.

**Caducidad y renovación**

En el proyecto, la cookie se llama `RestaurantesAspNet.Auth` y se renueva con cada petición. Un usuario activo no es expulsado. Uno que lleva tiempo sin entrar sí.

```csharp
builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.Name = "RestaurantesAspNet.Auth";
    options.SlidingExpiration = true;
});
```

**Comprobar la cuenta en cada petición**

La cookie no sabe si la cuenta se ha desactivado después de entrar. Para cortar la sesión al instante, el proyecto comprueba la cuenta en cada petición con `OnValidatePrincipal`.

```csharp
options.Events.OnValidatePrincipal = async context =>
{
    if (context.Principal is null)
    {
        context.RejectPrincipal();
        return;
    }

    // Asignar este evento sustituye el validador de SecurityStamp que Identity deja puesto por defecto.
    // Hay que llamarlo a mano para que las sesiones antiguas se invaliden al cambiar la contraseña.
    await SecurityStampValidator.ValidatePrincipalAsync(context);
    if (context.Principal is null)
    {
        return;
    }

    var userManager = context.HttpContext.RequestServices
        .GetRequiredService<UserManager<ApplicationUser>>();
    var user = await userManager.GetUserAsync(context.Principal);
    if (user is null || !user.IsActive)
    {
        context.RejectPrincipal();
    }
};
```

Hay tres cosas que comprueba este código.

- Si la cuenta ya no existe o está desactivada, la petición se trata como anónima
- Si cambiamos la contraseña, el `SecurityStamp` cambia. Las cookies antiguas dejan de valer
- `RejectPrincipal` solo quita al usuario en esa petición. No borra la cookie del navegador

**Cambio de rol**

Los roles están en la cookie, así que un cambio de rol no se nota hasta que el usuario vuelve a iniciar sesión. Es un compromiso: consultar los roles en cada petición cuesta una consulta extra en cada clic.
