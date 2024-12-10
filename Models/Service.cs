using System;
using System.Collections.Generic;

namespace BrewMaster.Models;

public partial class Service
{
    public int ServiceId { get; set; }

    public DateTime? Date { get; set; }

    public string? ServiceType { get; set; }

    public int UserId { get; set; }

    public virtual Employee User { get; set; } = null!;
}
