# Estructura del proyecto

Al crear el proyecto aparecen muchas carpetas y archivos. De momento solo necesitamos conocer los que usamos en el curso.

Estas son las carpetas principales:

- `Controllers` guarda las clases que reciben las peticiones del navegador
- `Models` guarda las clases que describen los datos, como `Restaurant`
- `Views` guarda las páginas `.cshtml` que generan el HTML
- `wwwroot` guarda los archivos que el navegador descarga tal cual, como CSS, JavaScript e imágenes

Y estos son los archivos principales de la raíz del proyecto:

- `Program.cs` configura la aplicación y define las rutas
- `appsettings.json` guarda la configuración, como la cadena de conexión a la base de datos
- `RestaurantesAspNet.csproj` lista los paquetes que usa el proyecto

## Cómo busca ASP.NET las vistas

ASP.NET busca cada vista a partir del controlador y de la acción. Si la petición llega a `HomeController` con la acción `Index`, usa el archivo `Views/Home/Index.cshtml`.

Por eso la carpeta de las vistas tiene el nombre del controlador sin la palabra `Controller`. Cuando creemos los restaurantes, tendremos `Controllers/RestaurantsController.cs`, `Models/Restaurant.cs` y `Views/Restaurants/Index.cshtml`.

La carpeta `Views/Shared` guarda las partes comunes a todas las páginas, como `_Layout.cshtml`. Lo vemos en la lección de Navbar y Layout.
