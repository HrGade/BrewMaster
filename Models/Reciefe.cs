using System;
using System.Collections.Generic;

namespace BrewMaster.Models;

public partial class Reciefe
{
    public DateTime Date { get; set; }

    public int MachineId { get; set; }

    public virtual Machine Machine { get; set; } = null!;
}
