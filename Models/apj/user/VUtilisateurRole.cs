using System;
using System.Collections.Generic;

namespace Template.Models.apj.user;

public partial class VUtilisateurRole
{
    public string Id { get; set; } = null!;

    public int? Refuser { get; set; }

    public string? Nomuser { get; set; }

    public int? Rang { get; set; }
}
