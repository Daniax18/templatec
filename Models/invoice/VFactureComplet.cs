using iTextSharp.text;
using iTextSharp.text.pdf;
using OfficeOpenXml;
using System.Text;

namespace Template.Models.invoice;

public partial class VFactureComplet
{
    public string Id { get; set; } = null!;

    public DateOnly? DateF { get; set; }

    public string? Idclient { get; set; }

    public string? Idmagasin { get; set; }

    public string? Remarque { get; set; }

    public decimal? TotalFacture { get; set; }

    public string? MNom { get; set; }

    public string? CNom { get; set; }

    //// PDF
    public static byte[] GeneratePdf(List<VFactureComplet> factures)
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
            var title = new Paragraph("Liste des Factures", new Font(Font.FontFamily.HELVETICA, 18, Font.BOLD));
            title.Alignment = Element.ALIGN_CENTER;
            title.SpacingAfter = 20; // Espace après le titre
            document.Add(title);

            // Créer un tableau
            var table = new PdfPTable(5);
            table.WidthPercentage = 100; // Tableau en pleine largeur
            table.SetWidths(new float[] { 1, 2, 2, 2 }); // Proportions des colonnes

            // Ajouter les en-têtes
            var headerFont = new Font(Font.FontFamily.HELVETICA, 12, Font.BOLD);
            table.AddCell(new PdfPCell(new Phrase("Id", headerFont)) { HorizontalAlignment = Element.ALIGN_CENTER });
            table.AddCell(new PdfPCell(new Phrase("Date", headerFont)) { HorizontalAlignment = Element.ALIGN_CENTER });
            table.AddCell(new PdfPCell(new Phrase("Client", headerFont)) { HorizontalAlignment = Element.ALIGN_CENTER });
            table.AddCell(new PdfPCell(new Phrase("Magasin", headerFont)) { HorizontalAlignment = Element.ALIGN_CENTER });
            table.AddCell(new PdfPCell(new Phrase("TotalFacture", headerFont)) { HorizontalAlignment = Element.ALIGN_CENTER });

            // Ajouter les données
            var dataFont = new Font(Font.FontFamily.HELVETICA, 10);
            foreach (var l in factures)
            {
                table.AddCell(new PdfPCell(new Phrase(l.Id.ToString(), dataFont)) { Padding = 5 });
                table.AddCell(new PdfPCell(new Phrase(l.DateF.ToString(), dataFont)) { Padding = 5 });
                table.AddCell(new PdfPCell(new Phrase(l.CNom, dataFont)) { Padding = 5 });
                table.AddCell(new PdfPCell(new Phrase(l.MNom, dataFont)) { Padding = 5 });
                table.AddCell(new PdfPCell(new Phrase($"Ar {l.TotalFacture}", dataFont)) { Padding = 5 });
            }

            document.Add(table);

            document.Close();

            return stream.ToArray();
        }
    }


    // EXCEL
    public static byte[] GenerateExcel(List<VFactureComplet> factures)
    {
        using (var package = new ExcelPackage())
        {
            var worksheet = package.Workbook.Worksheets.Add("Factures");
            worksheet.Cells[1, 1].Value = "Id";
            worksheet.Cells[1, 2].Value = "Date";
            worksheet.Cells[1, 3].Value = "Client";
            worksheet.Cells[1, 4].Value = "Magasin";
            worksheet.Cells[1, 5].Value = "TotalFacture";

            for (int i = 0; i < factures.Count; i++)
            {
                worksheet.Cells[i + 2, 1].Value = factures[i].Id;
                worksheet.Cells[i + 2, 2].Value = factures[i].DateF;
                worksheet.Cells[i + 2, 3].Value = factures[i].CNom;
                worksheet.Cells[i + 2, 4].Value = factures[i].MNom;
                worksheet.Cells[i + 2, 5].Value = factures[i].TotalFacture;
            }

            return package.GetAsByteArray();
        }
    }

    // CSV
    public static byte[] GenerateCsv(List<VFactureComplet> factures)
    {
        var csv = new StringBuilder();
        csv.AppendLine("Id,Date,Client,Magasin,TotalFacture");

        foreach (var l in factures)
        {
            csv.AppendLine($"{l.Id},{l.DateF},{l.CNom},{l.MNom},{l.TotalFacture}");
        }

        return Encoding.UTF8.GetBytes(csv.ToString());
    }
}
