# Vista Details

> **Vista de detalle**: Página que muestra los datos de un solo elemento

La vista de detalle recibe un solo restaurante. Su archivo es `Views/Restaurants/Details.cshtml`.

```cshtml
@model Restaurant
@{
    ViewData["Title"] = Model.Name;
}

<h1>@Model.Name</h1>

<dl class="row">
    <dt class="col-sm-3">Precio medio</dt>
    <dd class="col-sm-9">@Model.AveragePrice €</dd>

    <dt class="col-sm-3">Activo</dt>
    <dd class="col-sm-9">@(Model.Active ? "Sí" : "No")</dd>
</dl>

<h2 class="h4">Carta</h2>

@if (Model.Dishes.Count == 0)
{
    <p class="text-secondary">Este restaurante todavía no tiene platos.</p>
}
else
{
    <ul class="list-group">
        @foreach (var dish in Model.Dishes)
        {
            <li class="list-group-item d-flex justify-content-between">
                <span>@dish.Name</span>
                <span>@dish.Price €</span>
            </li>
        }
    </ul>
}
```

Usamos `@model Restaurant`, porque el controlador le pasa un restaurante completo.

`@if` y `else` deciden qué mostrar. Si el restaurante no tiene platos, escribimos un aviso en lugar de una lista vacía.

`Model.Dishes` llega cargada gracias al `Include` del controlador. Si olvidamos el `Include`, la carta aparece siempre vacía.

`ViewData["Title"]` pone el título en la pestaña del navegador. Lo usamos también en el layout.
