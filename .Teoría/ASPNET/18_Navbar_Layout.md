# Navbar y Layout

> **Layout**: Plantilla común que rodea a todas las páginas, con la cabecera, el menú y el pie

La plantilla ya trae el archivo `Views/Shared/_Layout.cshtml`. Todas las vistas se dibujan dentro de él. La parte que cambia en cada página aparece en `@RenderBody()`.

Abrimos el archivo y buscamos el menú. La plantilla trae enlaces a `Home` y a `Privacy`. Cambiamos `Privacy` por `Restaurantes`.

```cshtml
<ul class="navbar-nav flex-grow-1">
    <li class="nav-item">
        <a class="nav-link text-dark" asp-controller="Home" asp-action="Index">Inicio</a>
    </li>
    <li class="nav-item">
        <a class="nav-link text-dark" asp-controller="Restaurants" asp-action="Index">Restaurantes</a>
    </li>
</ul>
```

`asp-controller` y `asp-action` crean la dirección del enlace. El texto del enlace es lo que ve el usuario.

Para añadir una página al menú, copiamos un `<li>` y cambiamos el controlador y la acción.

La etiqueta `<title>` usa `@ViewData["Title"]`. Cada vista define ese valor con `ViewData["Title"] = "...";`, así cada página tiene su propio título.

Como todas las páginas usan el mismo layout, un cambio en el menú aparece en todas a la vez.
