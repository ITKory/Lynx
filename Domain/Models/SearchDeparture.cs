using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class SearchDeparture
{
    public int Id { get; set; }

    public int CartographerId { get; set; }

    public int SearchAdministratorId { get; set; }

    public int SearchRequestId { get; set; }

    public DateOnly Date { get; set; }

    public int LocationId { get; set; }

    public virtual User Cartographer { get; set; } = null!;

    public virtual ICollection<Crew> Crews { get; set; } = new List<Crew>();

    public virtual Location Location { get; set; } = null!;

    public virtual User SearchAdministrator { get; set; } = null!;

    public virtual SearchRequest SearchRequest { get; set; } = null!;

    public virtual ICollection<SeekerRegistration> SeekerRegistrations { get; set; } = new List<SeekerRegistration>();
}
