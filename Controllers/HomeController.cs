using RestaurantesAspNet.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace RestaurantesAspNet.Controllers;

/// <summary>Página de inicio de la plantilla común, sin entidades de restaurante todavía.</summary>
public sealed class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error() => View(new ErrorViewModel
    {
        RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
    });
}
