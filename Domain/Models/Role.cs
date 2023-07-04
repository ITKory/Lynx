using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class Role
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int TagId { get; set; }

    public virtual Tag Tag { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
