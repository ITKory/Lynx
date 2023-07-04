using System;
using System.Collections.Generic;

namespace  Domain.Models;

public partial class City
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public virtual ICollection<Profile> Profiles { get; set; } = new List<Profile>();
}
