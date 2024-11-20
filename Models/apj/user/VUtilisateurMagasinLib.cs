using System;
using System.Collections.Generic;

namespace Template.Models.apj.user;

public partial class VUtilisateurMagasinLib
{
    public string Id { get; set; } = null!;

    public int? Refuser { get; set; }

    public string? Nomuser { get; set; }

    public string? Pwduser { get; set; }

    public string MagasinId { get; set; } = null!;
}
