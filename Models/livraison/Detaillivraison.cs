using Template.Models;

namespace Template;

public partial class Detaillivraison
{
    public string Id { get; set; } = null!;

    public string? Idlivraison { get; set; }

    public string? Idproduit { get; set; }

    public decimal? PuProduit { get; set; }

    public int? Qte { get; set; }

    public decimal? TotalAchat { get; set; }

    public Detaillivraison() { }

    public Detaillivraison(string idmere, string key, string idproduit, decimal qty, StoreContext _context)
    {
        try
        {
            var produit = _context.Produits
                .FirstOrDefault(p => p.Id == idproduit);

            if (produit == null) throw new Exception("No produict selected");
            PuProduit = produit.Pu;
            TotalAchat = PuProduit * qty;
            Id = key;
            Qte = (int)qty;
            Idlivraison = idmere;
            Idproduit = idproduit;
        }
        catch (Exception ex)
        {
            throw new Exception($"Error on new DetailLivraison constructor: {ex.Message}");
        }

    }
}
