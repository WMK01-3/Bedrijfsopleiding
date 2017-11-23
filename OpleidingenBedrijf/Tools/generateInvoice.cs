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
        public static void Init(Invoice invoice)
        {
            PdfDocument document = new PdfDocument();
            document.Info.Title = "Created with PDFsharp";
            //string payed = (enrollment.Payed) ? "Ja" : "Nee";

            PdfPage page = document.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);

            XFont titleFont = new XFont("Century Gothic", 14, XFontStyle.Bold);
            XFont normalFont = new XFont("Verdana", 8);
            XFont normalDikFont = new XFont("Verdana", 8, XFontStyle.Bold);

            // Bedrijf info
            gfx.DrawString("DOPE B.V.", titleFont, XBrushes.Black, 460, 41);
            gfx.DrawString("Vanderlaenstraat 420", normalFont, XBrushes.Black, 460, 58);
            gfx.DrawString("1337 EZ, Zwollywood", normalFont, XBrushes.Black, 460, 72);
            gfx.DrawString("KVK: 1238923434", normalFont, XBrushes.Black, 460, 86);
            gfx.DrawString("Btw: NL 1238923434", normalFont, XBrushes.Black, 460, 100);


            // Logo
            string imageLoc = @"..\..\images\Logo.png";
            DrawImage(gfx, imageLoc, 15, 20, 200, 113);


            // Factuur info
            gfx.DrawString("FACTUUR", titleFont, XBrushes.Black, 25, 214);
            gfx.DrawString("Factuurdatum:", normalFont, XBrushes.Black, 25, 232);
            gfx.DrawString("Factuur nummer:", normalFont, XBrushes.Black, 25, 246);
            gfx.DrawString("Betaald:", normalFont, XBrushes.Black, 25, 260);

            gfx.DrawString(invoice.Date.ToString("d-MM-2017"), normalFont, XBrushes.Black, 115, 232);
            gfx.DrawString("#1106", normalDikFont, XBrushes.Black,115, 246);
            gfx.DrawString("Denket niet eh", normalDikFont, XBrushes.Black, 115, 260);


            // Table first row
            XPen tableLine = new XPen(XColors.DarkSlateGray, 0.5);
            gfx.DrawString("Lessen", normalDikFont, XBrushes.Black, 25, 295);
            gfx.DrawString("Cursus", normalDikFont, XBrushes.Black, 120, 295);
            gfx.DrawString("Bedrag", normalDikFont, XBrushes.Black, 280, 295);
            gfx.DrawString("Totaal", normalDikFont, XBrushes.Black, 420, 295);
            gfx.DrawString("Btw", normalDikFont, XBrushes.Black, 520, 295);
            gfx.DrawLine(tableLine, 25, 300, 575, 300);
            //gfx.DrawLine(tableLine, 25, 316, 575, 320);


            // generate enrollment data
            int i = 1;
            decimal[] total = new decimal[3] { 0,0,0 };  // 0 = subtotal, 1 = btw total, 2 = full total

            foreach (Enrollment enrollment in invoice.Enrollments)
            {
                int heightBorder = 300 + (i * 16);
                int heightText = 295 + (i * 16);


                string classes = $"{enrollment.Course.Dates.Count()} x {enrollment.Course.Duration} min";
                decimal priceClass = Math.Round( (enrollment.Course.Price / enrollment.Course.Dates.Count()) / 100 * 79, 2);
                decimal totalPrice = Math.Round( enrollment.Course.Price / 100 * 79, 2);
                decimal btwPrice = Math.Round( enrollment.Course.Price / 100 * 21,2);

                // draw course row
                gfx.DrawString(classes, normalFont, XBrushes.Black, 25, heightText);
                gfx.DrawString(enrollment.Course.Title, normalFont, XBrushes.Black, 120, heightText);
                gfx.DrawString($"€ {priceClass}", normalFont, XBrushes.Black, 280, heightText);
                gfx.DrawString($"€ {totalPrice}", normalFont, XBrushes.Black, 420, heightText);
                gfx.DrawString($"€ {btwPrice}", normalFont, XBrushes.Black, 520, heightText);
                gfx.DrawLine(tableLine, 25, heightBorder, 575, heightBorder);

                // saving prices
                total[0] += totalPrice;     total[1] += btwPrice;       total[2] += enrollment.Course.Price;
                i++;
            }

            //int hb = 300 + ((i+1) * 16);
            //int ht = 295 + ((i+1) * 16);

            gfx.DrawLine(tableLine, 375, 300 + ((i + 1) * 16), 575, 300 + ((i + 1) * 16));









            const string filename = "Factuur.pdf";
            document.Save(filename);
            Process.Start(filename);
        }

        public static void DrawImage(XGraphics gfx, string jpegSamplePath, int x, int y, int width, int height)
        {
            XImage image = XImage.FromFile(jpegSamplePath);
            gfx.DrawImage(image, x, y, width, height);
        }


    }


}
