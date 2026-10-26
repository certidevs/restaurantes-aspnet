# Enumeraciones

> **Enumeración**: Tipo con una lista fija de valores, como las opciones de un desplegable

Un restaurante tiene un tipo de cocina. Puede ser española, japonesa o italiana pero nunca cualquier texto. Para limitar las opciones usamos un `enum`.

Creamos el archivo `Models/FoodType.cs`.

```csharp
using System.ComponentModel.DataAnnotations;

namespace RestaurantesAspNet.Models;

public enum FoodType
{
    [Display(Name = "Española")]
    Spanish,

    [Display(Name = "Japonesa")]
    Japanese,

    [Display(Name = "Italiana")]
    Italian
}
```

El atributo `Display` cambia el texto que ve el usuario. En el código usamos `Spanish` y en la pantalla aparece `Española`.

Añadimos la propiedad al modelo `Restaurant`.

```csharp
public FoodType? FoodType { get; set; }
```

El `?` indica que el restaurante puede no tener tipo de cocina todavía. EF Core guarda el número de cada valor en la base de datos, no el texto.

En el curso usamos otros dos `enum`.

- `DishType` clasifica los platos en entrantes, principales y postres
- `OrderStatus` indica si un pedido está pendiente, en curso o completado
