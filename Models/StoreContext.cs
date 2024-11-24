using Microsoft.EntityFrameworkCore;
using Template.Models.analyse;
using Template.Models.apj.histo;
using Template.Models.apj.user;
using Template.Models.invoice;
using Template.Models.livraison;
using Template.Models.produit;

namespace Template.Models;

public partial class StoreContext : DbContext
{
    public StoreContext(DbContextOptions<StoreContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Magasin> Magasins { get; set; }

    public virtual DbSet<Utilisateur> Utilisateurs { get; set; }

    public virtual DbSet<VUtilisateurMagasinLib> VUtilisateurMagasinLibs { get; set; }

    public virtual DbSet<VUtilisateurRole> VUtilisateurRoles { get; set; }

    public virtual DbSet<Boncommande> Boncommandes { get; set; }

    public virtual DbSet<Categorie> Categories { get; set; }

    public virtual DbSet<Produit> Produits { get; set; }

    public virtual DbSet<Unite> Unites { get; set; }

    public virtual DbSet<Historique> Historiques { get; set; }

    public virtual DbSet<HistoriqueValeur> HistoriqueValeurs { get; set; }

    public virtual DbSet<Fournisseur> Fournisseurs { get; set; }

    public virtual DbSet<Livraison> Livraisons { get; set; }

    public virtual DbSet<VProduitComplet> VProduitComplets { get; set; }

    public virtual DbSet<Detaillivraison> Detaillivraisons { get; set; }

    public virtual DbSet<VDetailLivraisonComplet> VDetailLivraisonComplets { get; set; }

    public virtual DbSet<VLivraisonComplet> VLivraisonComplets { get; set; }
    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Detailfacture> Detailfactures { get; set; }

    public virtual DbSet<Facture> Factures { get; set; }

    public virtual DbSet<VDetailFactureComplet> VDetailFactureComplets { get; set; }

    public virtual DbSet<VFactureComplet> VFactureComplets { get; set; }

    public virtual DbSet<VAnalyseCaMagasin> VAnalyseCaMagasins { get; set; }

    public virtual DbSet<VAnalyseCategorie> VAnalyseCategories { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=NAUTILIUS\\SQLEXPRESS;Initial Catalog=store;Integrated Security=True;Trust Server Certificate=True")
         .LogTo(Console.WriteLine, LogLevel.Information)
            .EnableSensitiveDataLogging();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Magasin>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__magasin__3213E83FE905B164");

            entity.ToTable("magasin");

            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("id");
            entity.Property(e => e.Nom)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("nom");
        });

        modelBuilder.Entity<Utilisateur>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__utilisat__3213E83FCCCBA1C6");

            entity.ToTable("utilisateur");

            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("id");
            entity.Property(e => e.Adruser)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("adruser");
            entity.Property(e => e.Idrole)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("idrole");
            entity.Property(e => e.Loginuser)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("loginuser");
            entity.Property(e => e.Nomuser)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("nomuser");
            entity.Property(e => e.Pwduser)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("pwduser");
            entity.Property(e => e.Refuser).HasColumnName("refuser");
            entity.Property(e => e.Teluser)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("teluser");
        });

        modelBuilder.Entity<VUtilisateurMagasinLib>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("v_utilisateur_magasin_lib");

            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("id");
            entity.Property(e => e.MagasinId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("magasin_id");
            entity.Property(e => e.Nomuser)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("nomuser");
            entity.Property(e => e.Pwduser)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("pwduser");
            entity.Property(e => e.Refuser).HasColumnName("refuser");
        });

        modelBuilder.Entity<VUtilisateurRole>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("v_utilisateur_role");

            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("id");
            entity.Property(e => e.Nomuser)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("nomuser");
            entity.Property(e => e.Rang).HasColumnName("rang");
            entity.Property(e => e.Refuser).HasColumnName("refuser");
        });

        modelBuilder.Entity<Boncommande>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__boncomma__3213E83FD01F0E92");

            entity.ToTable("boncommande");

            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("id");
            entity.Property(e => e.Ref)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("ref");
        });

        modelBuilder.Entity<Categorie>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__categori__3213E83F404E1619");

            entity.ToTable("categorie");

            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("id");
            entity.Property(e => e.Nom)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("nom");
        });

        modelBuilder.Entity<Produit>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__produit__3213E83F1D785769");

            entity.ToTable("produit");

            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("id");
            entity.Property(e => e.Idcategorie)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("idcategorie");
            entity.Property(e => e.Idunite)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("idunite");
            entity.Property(e => e.Nom)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("nom");
            entity.Property(e => e.Pu)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("pu");
            entity.Property(e => e.Pv)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("pv");

            entity.HasOne(d => d.IdcategorieNavigation).WithMany(p => p.Produits)
                .HasForeignKey(d => d.Idcategorie)
                .HasConstraintName("FK__produit__idcateg__5BE2A6F2");

            entity.HasOne(d => d.IduniteNavigation).WithMany(p => p.Produits)
                .HasForeignKey(d => d.Idunite)
                .HasConstraintName("FK__produit__idunite__5AEE82B9");
        });

        modelBuilder.Entity<Unite>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__unite__3213E83FFA60CA94");

            entity.ToTable("unite");

            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("id");
            entity.Property(e => e.Nom)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("nom");
        });

        modelBuilder.Entity<Historique>(entity =>
        {
            entity.HasKey(e => e.Idhistorique).HasName("PK__historiq__FAF8C0725417573B");

            entity.ToTable("historique");

            entity.Property(e => e.Idhistorique)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("idhistorique");
            entity.Property(e => e.Action)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("action");
            entity.Property(e => e.Datehistorique).HasColumnName("datehistorique");
            entity.Property(e => e.Heure)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("heure");
            entity.Property(e => e.Idutilisateur)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("idutilisateur");
            entity.Property(e => e.Objet)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("objet");
            entity.Property(e => e.Refobjet)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("refobjet");
        });

        modelBuilder.Entity<HistoriqueValeur>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__historiq__3213E83F1B6DC371");

            entity.ToTable("historique_valeur");

            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("id");
            entity.Property(e => e.Idhisto)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("idhisto");
            entity.Property(e => e.NomClasse)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("nom_classe");
            entity.Property(e => e.NomTable)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("nom_table");
            entity.Property(e => e.Refhisto)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("refhisto");
            entity.Property(e => e.Val1)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("val1");
            entity.Property(e => e.Val10)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("val10");
            entity.Property(e => e.Val2)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("val2");
            entity.Property(e => e.Val3)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("val3");
            entity.Property(e => e.Val4)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("val4");
            entity.Property(e => e.Val5)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("val5");
            entity.Property(e => e.Val6)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("val6");
            entity.Property(e => e.Val7)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("val7");
            entity.Property(e => e.Val8)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("val8");
            entity.Property(e => e.Val9)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("val9");

            entity.HasOne(d => d.IdhistoNavigation).WithMany(p => p.HistoriqueValeurs)
                .HasForeignKey(d => d.Idhisto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__historiqu__idhis__04E4BC85");
        });


        modelBuilder.Entity<Fournisseur>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__fourniss__3213E83FB6306B0C");

            entity.ToTable("fournisseur");

            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("id");
            entity.Property(e => e.Nom)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("nom");
        });

        modelBuilder.Entity<Livraison>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__livraiso__3213E83FB8A1A08D");

            entity.ToTable("livraison");

            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("id");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.Idboncommande)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("idboncommande");
            entity.Property(e => e.Idfournisseur)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("idfournisseur");
            entity.Property(e => e.Idmagasin)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("idmagasin");
        });

        modelBuilder.Entity<VProduitComplet>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("v_produit_complet");

            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("id");
            entity.Property(e => e.Idcategorie)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("idcategorie");
            entity.Property(e => e.Idunite)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("idunite");
            entity.Property(e => e.Nom)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("nom");
            entity.Property(e => e.Pu)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("pu");
            entity.Property(e => e.Pv)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("pv");
            entity.Property(e => e.UniteNom)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("unite_nom");
        });

        modelBuilder.Entity<Detaillivraison>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__detailli__3213E83F7F9D27D7");

            entity.ToTable("detaillivraison");

            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("id");
            entity.Property(e => e.Idlivraison)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("idlivraison");
            entity.Property(e => e.Idproduit)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("idproduit");
            entity.Property(e => e.PuProduit)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("pu_produit");
            entity.Property(e => e.Qte).HasColumnName("qte");
            entity.Property(e => e.TotalAchat)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("total_achat");
        });

        modelBuilder.Entity<VDetailLivraisonComplet>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("v_detail_livraison_complet");

            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("id");
            entity.Property(e => e.Idlivraison)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("idlivraison");
            entity.Property(e => e.Idproduit)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("idproduit");
            entity.Property(e => e.Nom)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("nom");
            entity.Property(e => e.PuProduit)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("pu_produit");
            entity.Property(e => e.Qte).HasColumnName("qte");
            entity.Property(e => e.TotalAchat)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("total_achat");
            entity.Property(e => e.UniteNom)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("unite_nom");
        });

        modelBuilder.Entity<VLivraisonComplet>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("v_livraison_complet");

            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.FNom)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("f_nom");
            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("id");
            entity.Property(e => e.Idboncommande)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("idboncommande");
            entity.Property(e => e.Idfournisseur)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("idfournisseur");
            entity.Property(e => e.Idmagasin)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("idmagasin");
            entity.Property(e => e.MNom)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("m_nom");
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__client__3213E83F48A04F21");

            entity.ToTable("client");

            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("id");
            entity.Property(e => e.Contact)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("contact");
            entity.Property(e => e.Email)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Nom)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("nom");
        });

        modelBuilder.Entity<Detailfacture>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__detailfa__3213E83F6F488AB3");

            entity.ToTable("detailfacture");

            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("id");
            entity.Property(e => e.Idfacture)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("idfacture");
            entity.Property(e => e.Idproduit)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("idproduit");
            entity.Property(e => e.PuProduit)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("pu_produit");
            entity.Property(e => e.Qte).HasColumnName("qte");
            entity.Property(e => e.TotalVente)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("total_vente");

            entity.HasOne(d => d.IdfactureNavigation).WithMany(p => p.Detailfactures)
                .HasForeignKey(d => d.Idfacture)
                .HasConstraintName("FK__detailfac__idfac__55009F39");
        });

        modelBuilder.Entity<Facture>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__facture__3213E83F72EF4BF5");

            entity.ToTable("facture");

            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("id");
            entity.Property(e => e.DateF).HasColumnName("date_f");
            entity.Property(e => e.Idclient)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("idclient");
            entity.Property(e => e.Idmagasin)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("idmagasin");
            entity.Property(e => e.Remarque)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("remarque");
            entity.Property(e => e.TotalFacture)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("total_facture");

            entity.HasOne(d => d.IdclientNavigation).WithMany(p => p.Factures)
                .HasForeignKey(d => d.Idclient)
                .HasConstraintName("FK__facture__idclien__51300E55");
        });

        modelBuilder.Entity<VDetailFactureComplet>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("v_detail_facture_complet");

            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("id");
            entity.Property(e => e.Idfacture)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("idfacture");
            entity.Property(e => e.DateF).HasColumnName("date_f");
            entity.Property(e => e.Idproduit)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("idproduit");
            entity.Property(e => e.Nom)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("nom");
            entity.Property(e => e.PuProduit)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("pu_produit");
            entity.Property(e => e.Qte).HasColumnName("qte");
            entity.Property(e => e.TotalVente)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("total_vente");
            entity.Property(e => e.UniteNom)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("unite_nom");
            entity.Property(e => e.Catnom)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("catnom");
        });

        modelBuilder.Entity<VFactureComplet>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("v_facture_complet");

            entity.Property(e => e.CNom)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("c_nom");
            entity.Property(e => e.DateF).HasColumnName("date_f");
            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("id");
            entity.Property(e => e.Idclient)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("idclient");
            entity.Property(e => e.Idmagasin)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("idmagasin");
            entity.Property(e => e.MNom)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("m_nom");
            entity.Property(e => e.Remarque)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("remarque");
            entity.Property(e => e.TotalFacture)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("total_facture");
        });

        modelBuilder.Entity<VAnalyseCaMagasin>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("v_analyse_ca_magasin");

            entity.Property(e => e.Nom)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("nom");
            entity.Property(e => e.Total)
                .HasColumnType("decimal(38, 2)")
                .HasColumnName("total");
        });

        modelBuilder.Entity<VAnalyseCategorie>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("v_analyse_categorie");

            entity.Property(e => e.Nom)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("nom");
            entity.Property(e => e.Total).HasColumnName("total");
        });


        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
