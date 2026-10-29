# Errores comunes

Estos son los errores que aparecen casi siempre al empezar con Identity.

| Error | Qué pasa | Cómo se hace bien |
|-------|----------|-------------------|
| Crear `Alumno` y `Profesor` como clases separadas | El login se duplica y los datos se desincronizan | Un `ApplicationUser` con un rol |
| Guardar el rol como texto en `ApplicationUser` | Identity y `[Authorize]` no lo entienden | `AddToRoleAsync` y `RoleNames` |
| Escribir `"Admin"` a mano | Un error de escritura deja al administrador fuera | `RoleNames.Admin` |
| Renombrar un rol que ya tiene datos | Aparecen dos roles y nadie es administrador | Elegir bien el nombre desde el principio |
| Tomar el `UserId` del formulario | Cualquiera puede actuar en nombre de otro | `User.GetRequiredUserId()` |
| Borrar usuarios con datos | Se pierden reseñas, pedidos o matrículas | Desactivar con `IsActive` |
| Proteger solo el botón con `IsInRole` | Cualquiera entra escribiendo la URL | `[Authorize]` en la acción |
| Invertir `UseAuthentication` y `UseAuthorization` | El usuario llega vacío y los roles no funcionan | Authentication primero |
| Cerrar sesión con un enlace `GET` | Una web externa puede cerrar la sesión | `[HttpPost]` en Logout |
| Ignorar el resultado de `CreateAsync` | La cuenta no se crea y nadie avisa | Comprobar `Succeeded` |
| Cambiar `ApplicationUser` sin migración | Error de columna al hacer login | `dotnet ef migrations add` |
| Asignar `OnValidatePrincipal` sin llamar al validador | Las sesiones antiguas no se invalidan | `SecurityStampValidator.ValidatePrincipalAsync` |

Antes de dar por bueno el trabajo con usuarios, conviene revisar que:

- Solo hay una clase de persona, `ApplicationUser`
- Ninguna entidad repite el email, el nombre de login o la contraseña de una persona
- Los nombres de los roles están en `RoleNames`
- `DbInitializer` crea los roles y las cuentas de demo, y `SeedData:Enabled` puede apagarlo
- Cada acción sensible tiene `[Authorize]` o `[Authorize(Roles = ...)]`
- Cada botón que se oculta tiene también su acción protegida
- El `UserId` se toma de `User.GetRequiredUserId()`, nunca del formulario
- `UseAuthentication` va antes que `UseAuthorization`
- Logout es `POST`, y el filtro de antifalsificación está activo
- Las cuentas se desactivan en vez de borrarse
- Cambiar la contraseña invalida las sesiones abiertas con la antigua
- Un usuario sin rol no ve ni entra en lo que no debe
