using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class SearchRequest
{
    public int Id { get; set; }

    public int LostId { get; set; }

    public int MissingInformerId { get; set; }

    public int RequestAdministratorId { get; set; }

    public string Face { get; set; } = null!;

    public string? Comment { get; set; }

    public DateOnly DateOfLosee { get; set; }

    public DateOnly? DateOfFound { get; set; }

    public bool IsActive { get; set; }

    public bool IsFound { get; set; }

    public virtual ICollection<FoundStat> FoundStats { get; set; } = new List<FoundStat>();

    public virtual Profile Lost { get; set; } = null!;

    public virtual Profile MissingInformer { get; set; } = null!;

    public virtual User RequestAdministrator { get; set; } = null!;

    public virtual ICollection<SearchDeparture> SearchDepartures { get; set; } = new List<SearchDeparture>();
}
