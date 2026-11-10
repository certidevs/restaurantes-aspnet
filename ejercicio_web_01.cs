/* 
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RestaurantesAspNet.Models;

public enum ReservationStatus
{
    [Display(Name = "Pendiente")]
    Pending = 0,
    [Display(Name = "Confirmada")]
    Confirmed,
    [Display(Name = "Cancelada")]
    Cancelled

}

public class Restaurant
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public List<Reservation> Reservations { get; set; } = new();
}

public class Reservation
{
    public int Id { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public DateOnly Date { get; set; }
    public int GuestCount { get; set; }
    public ReservationStatus Status { get; set; } = ReservationStatus.Pending;

    public int RestaurantId { get; set; }
    public Restaurant? Restaurant { get; set; }
}
 */