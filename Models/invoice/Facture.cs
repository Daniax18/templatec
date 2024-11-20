using System;
using System.Collections.Generic;

namespace Template.Models.invoice;

public partial class Facture
{
    public string Id { get; set; } = null!;

    public DateOnly? DateF { get; set; }

    public string? Idclient { get; set; }

    public string? Idmagasin { get; set; }

    public string? Remarque { get; set; }

    public decimal? TotalFacture { get; set; }

    public virtual ICollection<Detailfacture> Detailfactures { get; set; } = new List<Detailfacture>();

    public virtual Client? IdclientNavigation { get; set; }
}
