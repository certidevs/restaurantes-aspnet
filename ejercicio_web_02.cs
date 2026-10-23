/* 
using System;
using System.Collections.Generic;
using System.Linq;

public class Restaurant
{
    public int Id { get; set; }
    public List<Employee> Employees { get; set; } = new();

    public List<WorkShift> GetShifts()
    {
        return Employees
        .SelectMany(e => e.Shifts)
        .OrderBy(s => s.Date)
        .ThenBy(s => s.StartsAt)
        .ToList();
    }
}

public class Employee
{
    public int Id { get; set; }
    public int RestaurantId { get; set; }
    public Restaurant? Restaurant { get; set; }

    public List<WorkShift> Shifts { get; set; } = new();

}

public class WorkShift
{
    public int Id { get; set; }
    public DateOnly Date { get; set; }
    public TimeOnly StartsAt { get; set; }
    public TimeOnly EndsAt { get; set; }

    public int EmployeeId { get; set; }
    public Employee? Employee { get; set; }

}
 */