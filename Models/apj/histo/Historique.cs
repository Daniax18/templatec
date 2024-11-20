namespace Template.Models.apj.histo;

public partial class Historique
{
    public string Idhistorique { get; set; } = null!;

    public DateOnly Datehistorique { get; set; }

    public string Heure { get; set; } = null!;

    public string Objet { get; set; } = null!;

    public string Action { get; set; } = null!;

    public string Idutilisateur { get; set; } = null!;

    public string Refobjet { get; set; } = null!;

    public virtual ICollection<HistoriqueValeur> HistoriqueValeurs { get; set; } = new List<HistoriqueValeur>();
}
