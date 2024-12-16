using System;
using System.Collections.Generic;

namespace BrewMaster.Models;

public partial class NotificationType
{
    public int NotTypeId { get; set; } 

    public string? NotType { get; set; } 

    public string? Description { get; set; }
}
