using System;
using System.Collections.Generic;

namespace BrewMaster.Models;

public partial class Machine
{
    public int MachineId { get; set; }

    public int? AmountOfUses { get; set; }

    public string? Location { get; set; }

    public DateOnly? LatestCleaning { get; set; }

    public DateOnly? LatestService { get; set; }

    public DateOnly? LatestFillUp { get; set; }

    public virtual ICollection<Reciefe> Recieves { get; set; } = new List<Reciefe>();

    public virtual ICollection<Employee> Users { get; set; } = new List<Employee>();
}
