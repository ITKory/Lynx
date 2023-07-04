using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class Location
{
    public int Id { get; set; }

    public string Latitude { get; set; } = null!;

    public string Longitude { get; set; } = null!;

    public virtual ICollection<Profile> Profiles { get; set; } = new List<Profile>();

    public virtual ICollection<SearchDeparture> SearchDepartures { get; set; } = new List<SearchDeparture>();
}
