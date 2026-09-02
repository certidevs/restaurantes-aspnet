# Restaurantes ASP.NET

Plantilla de clase para construir paso a paso la versión ASP.NET Core MVC de
[restaurantes-java](https://github.com/alansastre/restaurantes-java). Este repositorio
es independiente de `biblioteca-aspnet`: contiene solo la base transversal que todos
los proyectos de grupo necesitarán.

## Ya incluido

- .NET 10 LTS, C# 14 y ASP.NET Core MVC con Razor.
- Entity Framework Core 10 + SQLite en `App_Data/restaurantes.db`.
- ASP.NET Core Identity: registro, login, logout, cookies, bloqueo y roles.
- `ApplicationUser`, roles `Admin` y `User`, perfil, cambio de contraseña y avatar.
- Panel CRUD de administración de usuarios con las reglas de no borrar el último admin.
- Bootstrap 5.3 local y cambio claro/oscuro; el CSS propio se limita al avatar.
- Datos demo: `admin` / `Admin123!` y `user` / `User123!`.
- Migración inicial, Dockerfile y GitHub Actions para restaurar, compilar y probar.

No hay todavía `Restaurant`, `Dish`, `Review`, `Order`, controladores ni vistas del
dominio. Es intencional: esas piezas se crearán en clase, igual que en el proyecto
Java, para entender cada asociación y cada slice vertical.

## Ejecutar

Desde esta carpeta:

```bash
dotnet run --project RestaurantesAspNet.csproj
```

Abrir la URL que muestra la consola (normalmente `http://localhost:5251`). La primera
ejecución crea SQLite, aplica la migración de Identity y genera los usuarios demo.

## Arquitectura de la plantilla

```text
Navegador → Controller → servicio concreto solo si aporta una regla → DbContext → SQLite
                   ↓
              ViewModel → Razor + Bootstrap
```

`ApplicationDbContext` es el repositorio y unidad de trabajo que aporta EF Core. Por
eso no hay `Repositories/` ni interfaces `I...Service`: para este curso solo añadirían
boilerplate. `UserService` se conserva porque agrupa la lógica reutilizable de
Identity, perfiles, roles y avatares. Las APIs de Identity usan `async` porque así las
ofrece el framework; el resto se añadirá con el código más directo posible.

## Cómo continuará en clase

1. Leer `ApplicationUser`, `ApplicationDbContext`, `Program.cs` y el flujo de login.
2. Crear la primera entidad de restaurante y su `DbSet` en el contexto.
3. Añadir la migración con `dotnet ef migrations add AddRestaurants`.
4. Desarrollar el slice vertical: ViewModel, servicio si aporta una regla, controlador,
   vistas Razor y datos demo.
5. Repetir con platos y luego añadir las asociaciones que correspondan.
6. Conectar cada entidad con `ApplicationUser` cuando tenga propietario, reserva,
   favorito o autor.

## CI y Docker

El workflow [build-and-test.yml](.github/workflows/build-and-test.yml) ejecuta en cada
`push` a `main` y *pull request*:

```bash
dotnet restore RestaurantesAspNet.slnx
dotnet build RestaurantesAspNet.slnx --configuration Release --no-restore
dotnet test RestaurantesAspNet.slnx --configuration Release --no-build
```

El test xUnit actual es deliberadamente mínimo y cubre una regla de la subida de
avatares. Sirve para que el alumnado compruebe la configuración antes de introducir
tests de base de datos o controladores.

Para empaquetar la plantilla:

```bash
docker build -t restaurantes-aspnet:local .
docker run --rm -p 10000:10000 restaurantes-aspnet:local
```
