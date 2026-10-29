# Crear roles y usuarios de demo

> **Seed**: Datos iniciales que la aplicación crea al arrancar, para poder probarla sin tocar la base de datos a mano

Para probar la aplicación necesitamos un administrador y un usuario normal. Creamos los dos al arrancar, en lugar de hacerlo a mano en la base de datos. Todo ocurre en `Data/DbInitializer.cs`.

El proceso sigue siempre el mismo orden.

1. Aplicar las migraciones, para que existan las tablas
2. Crear los roles si no existen
3. Crear los usuarios si no existen
4. Asignar a cada usuario su rol si todavía no lo tiene

```csharp
public static class DbInitializer
{
    public static async Task InitializeAsync(IServiceProvider services)
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        context.Database.Migrate();

        var configuration = services.GetRequiredService<IConfiguration>();
        if (!configuration.GetValue("SeedData:Enabled", true))
        {
            return;
        }

        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        await EnsureRoleAsync(roleManager, RoleNames.User);
        await EnsureRoleAsync(roleManager, RoleNames.Admin);

        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
        await EnsureUserAsync(userManager, "admin", "admin@restaurantes.local", "Administrador", "Admin123!", RoleNames.Admin);
        await EnsureUserAsync(userManager, "user", "user@restaurantes.local", "Usuario de demo", "User123!", RoleNames.User);

        // ... el resto de datos de demo
    }
}
```

`Migrate()` va primero. Sin tablas no hay dónde guardar ni roles ni usuarios.

El arranque se ejecuta cada vez que iniciamos la aplicación. Si no comprobáramos antes, la segunda vez fallaría al crear `admin`, porque el nombre de usuario es único. Por eso cada paso mira primero si ya existe.

```csharp
private static async Task EnsureRoleAsync(RoleManager<IdentityRole> roleManager, string roleName)
{
    if (!await roleManager.RoleExistsAsync(roleName))
    {
        var result = await roleManager.CreateAsync(new IdentityRole(roleName));
        if (!result.Succeeded)
        {
            throw new InvalidOperationException($"No se pudo crear el rol {roleName}.");
        }
    }
}
```

`CreateAsync` no lanza una excepción si la contraseña no cumple las reglas. Devuelve un resultado con los errores. Si lo ignoramos, la cuenta no se crea y nadie avisa. Por eso el código revisa `Succeeded` y lanza una excepción con un mensaje claro.

Identity cifra la contraseña al crear el usuario. Si escribiéramos el `INSERT` a mano, tendríamos que guardar el cifrado nosotros, y es fácil equivocarse. Con `UserManager.CreateAsync` el cifrado lo hace Identity.

**Desactivar las cuentas de demo**

Las cuentas de demo tienen contraseñas conocidas. En un servidor real serían una puerta abierta. Por eso el flag `SeedData:Enabled` las apaga sin tocar el código.

```json
{
  "SeedData": { "Enabled": false }
}
```

Con el flag en `false`, el arranque termina antes de crear roles y usuarios.
