using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class SeekerRegistration
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int SearchDepartureId { get; set; }

    public TimeOnly StartAt { get; set; }

    public TimeOnly EndAt { get; set; }

    public virtual SearchDeparture SearchDeparture { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
