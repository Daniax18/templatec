namespace Template.Models.livraison;

public partial class Livraison
{
    public string Id { get; set; } = null!;

    public DateOnly? Date { get; set; }

    public string? Idmagasin { get; set; }

    public string? Idboncommande { get; set; }

    public string? Idfournisseur { get; set; }
}
