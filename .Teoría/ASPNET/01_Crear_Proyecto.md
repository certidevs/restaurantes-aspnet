# Crear un proyecto de ASP.NET desde cero

> **ASP.NET Core MVC**: Framework que sirve para crear páginas web con C#

Para crear la aplicación de restaurantes desde cero hace falta el SDK de .NET 10. Se comprueba con este comando.

```powershell
dotnet --version
```

Debe mostrar una versión que empiece por `10.0`. Si no es así, se instala el SDK de .NET 10 desde la web oficial de Microsoft.

Antes de crear el proyecto, conviene entrar en la carpeta donde se va a guardar. En este ejemplo es `C:\cdev`.

```powershell
cd C:\cdev
```

El proyecto se crea con la plantilla de MVC. La opción `-n` le pone el nombre `RestaurantesAspNet` y crea una carpeta con ese mismo nombre.

```powershell
dotnet new mvc -n RestaurantesAspNet
```

Después se entra en la carpeta nueva. Dentro está el archivo `RestaurantesAspNet.csproj`, que describe el proyecto.

```powershell
cd RestaurantesAspNet
```

Si se usa VS Code, la carpeta se abre con este comando.

```powershell
code .
```

La aplicación se ejecuta con el perfil `http`, que está en `Properties/launchSettings.json`.

```powershell
dotnet run --launch-profile http
```

El comando responde algo así.

```text
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: http://localhost:5083
info: Microsoft.Hosting.Lifetime[0]
      Application started. Press Ctrl+C to shut down.
```

El puerto puede ser otro en cada ordenador. El perfil `http` abre el navegador solo. Si no se abre, basta con copiar la dirección y pegarla en el navegador.

Para parar la aplicación se pulsa `Ctrl + C` en la terminal.

La plantilla trae una página de inicio, una página de privacidad y un menú. Los usuarios y la base de datos se añaden en las siguientes lecciones.
