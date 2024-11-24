namespace Template.Models.invoice;

public partial class VDetailFactureComplet
{
    public string Id { get; set; } = null!;

    public string? Idfacture { get; set; }
    public DateOnly? DateF { get; set; }

    public string? Idproduit { get; set; }

    public decimal? PuProduit { get; set; }

    public int? Qte { get; set; }

    public decimal? TotalVente { get; set; }

    public string? Nom { get; set; }

    public string? UniteNom { get; set; }

    public string? Catnom { get; set; }
}
