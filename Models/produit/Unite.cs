using System;
using System.Collections.Generic;

namespace Template.Models.produit;

public partial class Unite
{
    public string Id { get; set; } = null!;

    public string? Nom { get; set; }

    public virtual ICollection<Produit> Produits { get; set; } = new List<Produit>();
}
