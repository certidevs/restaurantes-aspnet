# Modelos

> **Modelo**: Clase de C# que describe los datos de la aplicación

Un modelo es una clase normal de C#. Cada propiedad pública se convertirá en una columna de la tabla. Creamos la carpeta `Models` y dentro el archivo `Restaurant.cs`.

```csharp
namespace RestaurantesAspNet.Models;

public class Restaurant
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public double? AveragePrice { get; set; }
    public bool Active { get; set; }
}
```

Estas son las propiedades.

- `Id` es la clave principal. EF Core lo rellena solo al guardar el restaurante.
- `Name` es el nombre del restaurante. `string` es el tipo para el texto.
- `AveragePrice` es el precio medio. El `?` indica que puede estar vacío.
- `Active` indica si el restaurante está en servicio. Solo puede ser `true` o `false`.

`= string.Empty` deja el nombre vacío en lugar de `null` cuando creamos un restaurante nuevo.

Los modelos no tienen botones ni HTML. Solo describen datos. Las pantallas los usan más adelante.
