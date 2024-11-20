namespace Template.Models.livraison;

public partial class VDetailLivraisonComplet
{
    public string Id { get; set; } = null!;

    public string? Idlivraison { get; set; }

    public string? Idproduit { get; set; }

    public decimal? PuProduit { get; set; }

    public int? Qte { get; set; }

    public decimal? TotalAchat { get; set; }

    public string? Nom { get; set; }

    public string? UniteNom { get; set; }
}
