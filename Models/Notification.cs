using System;
using System.Collections.Generic;

namespace BrewMaster.Models;

public partial class Notification
{
    public int NotificationId { get; set; }

    public int? NotTypeId { get; set; }

    public int? UserId { get; set; }

    public int? MachineId { get; set; }

    public DateTime? Date { get; set; }
}
