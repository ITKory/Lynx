using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class CrewMate
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int CrewId { get; set; }

    public virtual Crew Crew { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
