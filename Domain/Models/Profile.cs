using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class Profile
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string? RelativesPhone { get; set; }

    public DateOnly Birthday { get; set; }

    public string Call { get; set; } = null!;

    public int CityId { get; set; }

    public int? LocationId { get; set; }

    public virtual City City { get; set; } = null!;

    public virtual Location Location { get; set; } = null!;

    public virtual ICollection<SearchRequest> SearchRequestLosts { get; set; } = new List<SearchRequest>();

    public virtual ICollection<SearchRequest> SearchRequestMissingInformers { get; set; } = new List<SearchRequest>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
