# Restaurantes ASP.NET

Proyecto base para construir en clase, paso a paso, la versión ASP.NET del proyecto
[restaurantes-java](https://github.com/alansastre/restaurantes-java).

Esta carpeta se ha dejado intencionadamente sencilla: contiene únicamente una aplicación
ASP.NET Core MVC generada con .NET 10, Bootstrap local y una pantalla inicial. La aplicación
se irá completando durante las sesiones, igual que se hizo con el proyecto Java.

## Objetivo final

- Catálogo de restaurantes y platos.
- Reseñas y favoritos.
- Usuarios con roles `User` y `Admin`.
- Pedidos con líneas, cantidades, total y estados.
- Panel de administración.
- Vistas Razor renderizadas en servidor y Bootstrap.
- EF Core con SQLite para desarrollo local.

## Secuencia sugerida para las clases

1. Controlador MVC, rutas, vistas Razor, layout y Bootstrap.
2. Modelos `Restaurant`, `Dish`, `Review`, `Order` y `OrderLine`.
3. `ApplicationDbContext`, SQLite y migraciones EF Core.
4. Repositorios y consultas LINQ.
5. Servicios con la lógica de negocio.
6. CRUD de restaurantes y platos.
7. Identity, login, registro y roles.
8. Reseñas, favoritos y perfil.
9. Pedidos y panel de administración.
10. API REST opcional y mejoras de producción.

## Arrancar

Desde la raíz de este repositorio:

```bash
dotnet run
```

En esta fase no hay base de datos ni autenticación configuradas todavía.
