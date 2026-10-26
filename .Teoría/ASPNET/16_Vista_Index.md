# Vista Index

> **Vista**: Archivo `.cshtml` que mezcla HTML con código C# para crear una página

La vista de la lista está en `Views/Restaurants/Index.cshtml`. El nombre de la carpeta es el del controlador, y el nombre del archivo es el de la acción.

```cshtml
@model List<RestaurantListItemViewModel>
@{
    ViewData["Title"] = "Restaurantes";
}

<h1>Restaurantes</h1>

<table class="table">
    <thead>
        <tr>
            <th>Nombre</th>
            <th>Platos</th>
            <th>Activo</th>
        </tr>
    </thead>
    <tbody>
        @foreach (var restaurant in Model)
        {
            <tr>
                <td><a asp-action="Details" asp-route-id="@restaurant.Id">@restaurant.Name</a></td>
                <td>@restaurant.DishCount</td>
                <td>@(restaurant.Active ? "Sí" : "No")</td>
            </tr>
        }
    </tbody>
</table>
```

Esta mezcla de HTML y C# se escribe con la sintaxis Razor. Estas son las piezas nuevas.

- `@model` indica el tipo de datos que recibe la vista.
- `@foreach` recorre la lista de restaurantes.
- `@` delante de código C# hace que se escriba su valor. Sin `@`, lo que escribimos es HTML.
- `asp-action` y `asp-route-id` crean el enlace `/Restaurants/Details/2`.

La plantilla ya trae `@addTagHelper` en `Views/_ViewImports.cshtml`, por eso funcionan los `asp-`. Estos atributos se llaman Tag Helpers. Para que la vista reconozca el ViewModel, añadimos esta línea en el mismo archivo.

```cshtml
@using RestaurantesAspNet.ViewModels.Restaurants
```
