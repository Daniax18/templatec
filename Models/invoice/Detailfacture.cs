namespace Template.Models.invoice;

public partial class Detailfacture
{

    public string Id { get; set; } = null!;

    public string? Idfacture { get; set; }

    public string? Idproduit { get; set; }

    public decimal? PuProduit { get; set; }

    public int? Qte { get; set; }

    public decimal? TotalVente { get; set; }

    public virtual Facture? IdfactureNavigation { get; set; }

    public Detailfacture() { }

    public Detailfacture(string? idfacture, string? idproduit, int? qte, StoreContext _context)
    {
        Random random = new Random();
        int randomNumber = random.Next(1, 1000);
        Id = $"DF_{randomNumber}";
        Idfacture = idfacture;
        Idproduit = idproduit;
        Qte = qte;

        var produit = _context.Produits
            .FirstOrDefault(p => p.Id == idproduit);

        if (produit == null) throw new Exception("No produict selected");
        PuProduit = produit.Pu;
        TotalVente = PuProduit * qte;
    }
}
