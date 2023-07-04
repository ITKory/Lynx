using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class Tag
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public virtual ICollection<Role> Roles { get; set; } = new List<Role>();
}
