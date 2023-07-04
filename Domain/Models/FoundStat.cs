using System;
using System.Collections.Generic;

namespace Domain.Models ;        

public partial class FoundStat
{
    public int Id { get; set; }

    public int SearchRequestId { get; set; }

    public int UserId { get; set; }

    public virtual SearchRequest SearchRequest { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
