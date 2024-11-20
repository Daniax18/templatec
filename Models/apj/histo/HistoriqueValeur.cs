namespace Template.Models.apj.histo;

public partial class HistoriqueValeur
{
    public string Id { get; set; } = null!;

    public string Idhisto { get; set; } = null!;

    public string Refhisto { get; set; } = null!;

    public string NomTable { get; set; } = null!;

    public string NomClasse { get; set; } = null!;

    public string? Val1 { get; set; }

    public string? Val2 { get; set; }

    public string? Val3 { get; set; }

    public string? Val4 { get; set; }

    public string? Val5 { get; set; }

    public string? Val6 { get; set; }

    public string? Val7 { get; set; }

    public string? Val8 { get; set; }

    public string? Val9 { get; set; }

    public string? Val10 { get; set; }

    public virtual Historique IdhistoNavigation { get; set; } = null!;
}
