using System;
using System.Collections.Generic;

namespace BrewMaster.Models;

public partial class Employee
{
    public int UserId { get; set; }

    public string? UserType { get; set; }

    public string? Name { get; set; }

    public string? Password { get; set; }

    public virtual ICollection<Service> Services { get; set; } = new List<Service>();

    public virtual ICollection<Machine> Machines { get; set; } = new List<Machine>();
}
