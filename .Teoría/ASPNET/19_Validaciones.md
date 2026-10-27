# Validaciones

> **Validación**: Regla que comprueba que los datos son correctos antes de guardarlos

Un restaurante no puede guardarse sin nombre. Un precio no puede ser negativo. Escribimos estas reglas como atributos encima de cada propiedad del modelo.

```csharp
using System.ComponentModel.DataAnnotations;

namespace RestaurantesAspNet.Models;

public class Restaurant
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio.")]
    [StringLength(100, ErrorMessage = "El nombre no puede superar los 100 caracteres.")]
    public string Name { get; set; } = string.Empty;

    [Range(0, 1000, ErrorMessage = "El precio medio debe estar entre 0 y 1000.")]
    public double? AveragePrice { get; set; }

    public bool Active { get; set; }
}
```

Estos son los atributos que usamos.

- `[Required]` no permite dejar el campo vacío
- `[StringLength]` limita el número de caracteres
- `[Range]` fija el valor mínimo y el máximo

Estas reglas viven en el modelo. Así se aplican a todos los formularios que usen `Restaurant`.

Las comprobamos en la acción que guarda el formulario, con `ModelState.IsValid`. Lo vemos en la siguiente lección.
