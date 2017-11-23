using BedrijfsOpleiding.Models;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Media;

namespace BedrijfsOpleiding.Tools
{
    static class generateInvoice
    {
        public static void NewPdf(Invoice invoice)
        {
            PdfDocument document = new PdfDocument();
            document.Info.Title = "Created with PDFsharp";

            PdfPage page = document.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);

            XFont titleFont = new XFont("Century Gothic", 14, XFontStyle.Bold);
            XFont normalFont = new XFont("Verdana", 8);
            XFont normalBoldFont = new XFont("Verdana", 8, XFontStyle.Bold);
            XFont footerFont = new XFont("Verdana", 5, XFontStyle.Italic);

            XPen tableLine = new XPen(XColors.DarkSlateGray, 0.5);
            XPen footerLine = new XPen(XColors.Gray, 0.5);


            // Bedrijf info
            gfx.DrawString("DOPE B.V.", titleFont, XBrushes.Black, 460, 41);
            gfx.DrawString("Vanderlaenstraat 420", normalFont, XBrushes.Black, 460, 58);
            gfx.DrawString("1337 EZ, Zwollywood", normalFont, XBrushes.Black, 460, 72);


            gfx.DrawString("Tel: (055) 420 69 69", normalFont, XBrushes.Black, 460, 97);
            gfx.DrawString("sales@dopecourses.co.uk", normalFont, XBrushes.Black, 460, 111);

            gfx.DrawString("KVK: 1238923434", normalFont, XBrushes.Black, 460, 136);
            gfx.DrawString("Btw: NL 1238923434", normalFont, XBrushes.Black, 460, 150);
            gfx.DrawString("NL03 INGB 0003 25 65", normalFont, XBrushes.Black, 460, 164);

            // Logo
            string imageLoc = @"..\..\images\Logo.png";
            DrawImage(gfx, imageLoc, 15, 20, 200, 113);


            // Factuur info
            gfx.DrawString("FACTUUR", titleFont, XBrushes.Black, 25, 214);
            gfx.DrawString("Factuurdatum:", normalFont, XBrushes.Black, 25, 232);
            gfx.DrawString("Factuur nummer:", normalFont, XBrushes.Black, 25, 246);
            gfx.DrawString("Betaald:", normalFont, XBrushes.Black, 25, 260);

            gfx.DrawString(invoice.Date.ToString("d-MM-2017"), normalFont, XBrushes.Black, 115, 232);
            gfx.DrawString("#1106", normalBoldFont, XBrushes.Black,115, 246);
            gfx.DrawString("Denket niet eh", normalBoldFont, XBrushes.Black, 115, 260);


            // Table first row
            gfx.DrawString("Lessen", normalBoldFont, XBrushes.Black, 25, 345);
            gfx.DrawString("Cursus", normalBoldFont, XBrushes.Black, 120, 345);
            gfx.DrawString("Prijs per les", normalBoldFont, XBrushes.Black, 280, 345);
            gfx.DrawString("Totaal", normalBoldFont, XBrushes.Black, 420, 345);
            gfx.DrawString("Btw", normalBoldFont, XBrushes.Black, 520, 345);
            gfx.DrawLine(tableLine, 25, 350, 575, 350);
            //gfx.DrawLine(tableLine, 25, 316, 575, 320);


            // generate enrollment data
            int i = 1;
            decimal[] total = new decimal[3] { 0,0,0 };  // 0 = subtotal, 1 = btw total, 2 = full total

            foreach (Enrollment enrollment in invoice.Enrollments)
            {
                int heightBorder = 350 + (i * 16);
                int heightText = 345 + (i * 16);


                string classes = $"{enrollment.Course.Dates.Count()} x {enrollment.Course.Duration} min";
                decimal priceClass = Math.Round( (enrollment.Course.Price / enrollment.Course.Dates.Count()) / 100 * 79, 2, MidpointRounding.AwayFromZero);
                decimal totalPrice = Math.Round( enrollment.Course.Price / 100 * 79, 2, MidpointRounding.AwayFromZero);
                decimal btwPrice = Math.Round( enrollment.Course.Price / 100 * 21, 2, MidpointRounding.AwayFromZero);

                // draw course row
                gfx.DrawString(classes, normalFont, XBrushes.Black, 25, heightText);
                gfx.DrawString(enrollment.Course.Title, normalFont, XBrushes.Black, 120, heightText);
                gfx.DrawString($"€ {String.Format("{0:0.00}", priceClass)}", normalFont, XBrushes.Black, 280, heightText);
                gfx.DrawString($"€ {String.Format("{0:0.00}",totalPrice)}", normalFont, XBrushes.Black, 420, heightText);
                gfx.DrawString($"€ {String.Format("{0:0.00}", btwPrice)}", normalFont, XBrushes.Black, 520, heightText);
                gfx.DrawLine(tableLine, 25, heightBorder, 575, heightBorder);

                // saving prices
                total[0] += totalPrice;     total[1] += btwPrice;       total[2] += enrollment.Course.Price;
                i++;
            }

            // Drawing subtotal && total
            gfx.DrawLine(tableLine, 280, 350 + ((i + 1) * 16), 520, 350 + ((i + 1) * 16));

            gfx.DrawString($"Subtotaal", normalBoldFont, XBrushes.Black, 280, 345 + (i * 16));
            gfx.DrawString($"€ {String.Format("{0:0.00}", total[0])}", normalBoldFont, XBrushes.Black, 420, 345 + (i * 16));

            gfx.DrawString($"BTW", normalBoldFont, XBrushes.Black, 280, 345 + ((i + 1) * 16));
            gfx.DrawString($"€ {String.Format("{0:0.00}", total[1])}", normalFont, XBrushes.Black, 420, 345 + ((i + 1) * 16));

            gfx.DrawString($"Totaal", normalBoldFont, XBrushes.Black, 280, 345 + ((i + 2) * 16));
            gfx.DrawString($"€ {String.Format("{0:0.00}", total[2])}", normalBoldFont, XBrushes.Black, 420, 345 + ((i + 2) * 16));


            // Drawing footer
            gfx.DrawLine(footerLine, 25, 810, 575, 810);
            string footer1 = $"We verzoeken u vriendelijk het bovenstaande bedrag van € {String.Format("{0:0.00}", total[2])} te voldoen op onze bankrekening";
            string footer2 = $"onder vermelding van het factuur nummer. Voor vragen kunt u contact opnemen per email of telefoon";

            gfx.DrawString(footer1, footerFont, XBrushes.Gray, new XRect(0, 816, page.Width, 10), XStringFormats.Center);
            gfx.DrawString(footer2, footerFont, XBrushes.Gray, new XRect(0, 823, page.Width, 10), XStringFormats.Center);

            Invoice stuff = invoice;

            string filename = $"{DateTime.Now.ToString("yyyyMMdd")}_{invoice.Customer.LastName},{invoice.Customer.FirstName}_Factuur.pdf";
            string filepath = @"..\..\Invoices\";
            document.Save(filepath + filename);
            //Process.Start(filepath + filename);
        }

        public static void DrawImage(XGraphics gfx, string jpegSamplePath, int x, int y, int width, int height)
        {
            XImage image = XImage.FromFile(jpegSamplePath);
            gfx.DrawImage(image, x, y, width, height);
        }

    }


}
