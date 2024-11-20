namespace Template.Models.invoice;

public partial class Client
{
    public string Id { get; set; } = null!;

    public string? Nom { get; set; }

    public string? Email { get; set; }

    public string? Contact { get; set; }

    public virtual ICollection<Facture> Factures { get; set; } = new List<Facture>();
}
