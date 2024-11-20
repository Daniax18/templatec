using iTextSharp.text;
using iTextSharp.text.pdf;
using OfficeOpenXml;
using System.Text;

namespace Template.Models.livraison;

public partial class VLivraisonComplet
{
    public string Id { get; set; } = null!;

    public DateOnly? Date { get; set; }

    public string? Idmagasin { get; set; }

    public string? Idboncommande { get; set; }

    public string? Idfournisseur { get; set; }

    public string? MNom { get; set; }

    public string? FNom { get; set; }

    //// PDF
    public static byte[] GeneratePdf(List<VLivraisonComplet> livraisons)
    {
        using (var stream = new MemoryStream())
        {
            var document = new Document(PageSize.A4, 36, 36, 50, 50); // Marges : gauche, droite, haut, bas
            PdfWriter.GetInstance(document, stream);
            document.Open();

            // Ajouter un logo
            var logo = Image.GetInstance("wwwroot/img/Logo.PNG"); // Chemin vers votre logo
            logo.ScaleAbsolute(100, 100); // Taille du logo
            logo.Alignment = Image.ALIGN_CENTER;
            document.Add(logo);

            // Ajouter un titre centré
            var title = new Paragraph("Liste des Livraisons", new Font(Font.FontFamily.HELVETICA, 18, Font.BOLD));
            title.Alignment = Element.ALIGN_CENTER;
            title.SpacingAfter = 20; // Espace après le titre
            document.Add(title);

            // Créer un tableau
            var table = new PdfPTable(4);
            table.WidthPercentage = 100; // Tableau en pleine largeur
            table.SetWidths(new float[] { 1, 2, 2, 2 }); // Proportions des colonnes

            // Ajouter les en-têtes
            var headerFont = new Font(Font.FontFamily.HELVETICA, 12, Font.BOLD);
            table.AddCell(new PdfPCell(new Phrase("Id", headerFont)) { HorizontalAlignment = Element.ALIGN_CENTER });
            table.AddCell(new PdfPCell(new Phrase("Date", headerFont)) { HorizontalAlignment = Element.ALIGN_CENTER });
            table.AddCell(new PdfPCell(new Phrase("Fournisseur", headerFont)) { HorizontalAlignment = Element.ALIGN_CENTER });
            table.AddCell(new PdfPCell(new Phrase("Magasin", headerFont)) { HorizontalAlignment = Element.ALIGN_CENTER });

            // Ajouter les données
            var dataFont = new Font(Font.FontFamily.HELVETICA, 10);
            foreach (var l in livraisons)
            {
                table.AddCell(new PdfPCell(new Phrase(l.Id.ToString(), dataFont)) { Padding = 5 });
                table.AddCell(new PdfPCell(new Phrase(l.Date.ToString(), dataFont)) { Padding = 5 });
                table.AddCell(new PdfPCell(new Phrase(l.FNom, dataFont)) { Padding = 5 });
                table.AddCell(new PdfPCell(new Phrase(l.MNom, dataFont)) { Padding = 5 });
            }

            document.Add(table);

            document.Close();

            return stream.ToArray();
        }
    }


    // EXCEL
    public static byte[] GenerateExcel(List<VLivraisonComplet> livraisons)
    {
        using (var package = new ExcelPackage())
        {
            var worksheet = package.Workbook.Worksheets.Add("Livraisons");
            worksheet.Cells[1, 1].Value = "Id";
            worksheet.Cells[1, 2].Value = "Date";
            worksheet.Cells[1, 3].Value = "Fournisseur";
            worksheet.Cells[1, 4].Value = "Magasin";

            for (int i = 0; i < livraisons.Count; i++)
            {
                worksheet.Cells[i + 2, 1].Value = livraisons[i].Id;
                worksheet.Cells[i + 2, 2].Value = livraisons[i].Date;
                worksheet.Cells[i + 2, 3].Value = livraisons[i].FNom;
                worksheet.Cells[i + 2, 4].Value = livraisons[i].MNom;
            }

            return package.GetAsByteArray();
        }
    }

    // CSV
    public static byte[] GenerateCsv(List<VLivraisonComplet> livraisons)
    {
        var csv = new StringBuilder();
        csv.AppendLine("Id,Date,Fournisseur,Magasin");

        foreach (var l in livraisons)
        {
            csv.AppendLine($"{l.Id},{l.Date},{l.FNom},{l.MNom}");
        }

        return Encoding.UTF8.GetBytes(csv.ToString());
    }
}
