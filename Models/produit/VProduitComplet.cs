namespace Template.Models.produit;

public partial class VProduitComplet
{
    public string Id { get; set; } = null!;

    public string? Nom { get; set; }

    public string? Idunite { get; set; }

    public string? Idcategorie { get; set; }

    public decimal? Pu { get; set; }

    public decimal? Pv { get; set; }

    public string? UniteNom { get; set; }
}
