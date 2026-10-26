# Instalar los paquetes de la base de datos

Una aplicación de restaurantes necesita guardar datos. Usamos Entity Framework Core, que traduce nuestras clases a tablas. También usamos SQLite, que guarda todo en un único archivo. Así no hace falta instalar ningún servidor de base de datos.

Desde la carpeta del proyecto instalamos los dos paquetes. Usamos la misma versión en los dos.

```powershell
dotnet add package Microsoft.EntityFrameworkCore.Sqlite --version 10.0.11
dotnet add package Microsoft.EntityFrameworkCore.Design --version 10.0.11
```

Los paquetes quedan anotados en el archivo `RestaurantesAspNet.csproj`.

```xml
<PackageReference Include="Microsoft.EntityFrameworkCore.Sqlite" Version="10.0.11" />
```

`Sqlite` conecta EF Core con SQLite. `Design` lo usan las herramientas de migraciones que lanzamos desde la terminal.

Por último instalamos `dotnet-ef`. Es la herramienta de línea de comandos para crear y aplicar migraciones. Antes creamos un manifiesto de herramientas, que guarda la versión dentro del proyecto.

```powershell
dotnet new tool-manifest
dotnet tool install dotnet-ef --version 10.0.11
```

Comprobamos que funciona.

```powershell
dotnet ef --version
```

El comando responde algo así.

```text
Entity Framework Core .NET Command-line Tools
10.0.11
```

Usamos la misma versión en todos los paquetes. Así evitamos errores raros al crear migraciones.
