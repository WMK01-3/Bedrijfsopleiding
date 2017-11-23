using BedrijfsOpleiding.Models;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System;
using System.Diagnostics;
using System.Net.Mail;

namespace BedrijfsOpleiding.Tools
{
    public static class GenerateInvoice
    {
        public static string NewPdf(Invoice invoice)
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
            const string imageLoc = @"..\..\images\Logo.png";
            DrawImage(gfx, imageLoc, 15, 20, 200, 113);


            // Factuur info
            gfx.DrawString("FACTUUR", titleFont, XBrushes.Black, 25, 214);
            gfx.DrawString("Factuurdatum:", normalFont, XBrushes.Black, 25, 232);
            gfx.DrawString("Factuur nummer:", normalFont, XBrushes.Black, 25, 246);
            gfx.DrawString("Betaald:", normalFont, XBrushes.Black, 25, 260);

            gfx.DrawString(invoice.Date.ToString("d-MM-2017"), normalFont, XBrushes.Black, 115, 232);
            gfx.DrawString("#1106", normalBoldFont, XBrushes.Black, 115, 246);
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
            var i = 1;
            var total = new decimal[] { 0, 0, 0 };  // 0 = subtotal, 1 = btw total, 2 = full total

            foreach (Enrollment enrollment in invoice.Enrollments)
            {
                int heightBorder = 350 + i * 16;
                int heightText = 345 + i * 16;


                string classes = $"{enrollment.Course.Dates.Count} x {enrollment.Course.Duration} min";
                decimal priceClass = Math.Round(enrollment.Course.Price / enrollment.Course.Dates.Count / 100 * 79, 2, MidpointRounding.AwayFromZero);
                decimal totalPrice = Math.Round(enrollment.Course.Price / 100 * 79, 2, MidpointRounding.AwayFromZero);
                decimal btwPrice = Math.Round(enrollment.Course.Price / 100 * 21, 2, MidpointRounding.AwayFromZero);

                // draw course row
                gfx.DrawString(classes, normalFont, XBrushes.Black, 25, heightText);
                gfx.DrawString(enrollment.Course.Title, normalFont, XBrushes.Black, 120, heightText);
                gfx.DrawString(s: $"€ {priceClass:0.00}", font: normalFont, brush: XBrushes.Black, x: 280, y: heightText);
                gfx.DrawString(s: $"€ {totalPrice:0.00}", font: normalFont, brush: XBrushes.Black, x: 420, y: heightText);
                gfx.DrawString(s: $"€ {btwPrice:0.00}", font: normalFont, brush: XBrushes.Black, x: 520, y: heightText);
                gfx.DrawLine(tableLine, 25, heightBorder, 575, heightBorder);

                // saving prices
                total[0] += totalPrice; total[1] += btwPrice; total[2] += enrollment.Course.Price;
                i++;
            }

            // Drawing subtotal && total
            gfx.DrawLine(tableLine, 280, 350 + (i + 1) * 16, 520, 350 + (i + 1) * 16);

            gfx.DrawString("Subtotaal", normalBoldFont, XBrushes.Black, 280, 345 + i * 16);
            gfx.DrawString($"€ {total[0]:0.00}", normalBoldFont, XBrushes.Black, 420, 345 + i * 16);

            gfx.DrawString("BTW", normalBoldFont, XBrushes.Black, 280, 345 + (i + 1) * 16);
            gfx.DrawString($"€ {total[1]:0.00}", normalFont, XBrushes.Black, 420, 345 + (i + 1) * 16);

            gfx.DrawString("Totaal", normalBoldFont, XBrushes.Black, 280, 345 + (i + 2) * 16);
            gfx.DrawString($"€ {total[2]:0.00}", normalBoldFont, XBrushes.Black, 420, 345 + (i + 2) * 16);


            // Drawing footer
            gfx.DrawLine(footerLine, 25, 810, 575, 810);
            string footer1 = $"We verzoeken u vriendelijk het bovenstaande bedrag van € {total[2]:0.00} te voldoen op onze bankrekening";
            const string footer2 = "onder vermelding van het factuur nummer. Voor vragen kunt u contact opnemen per email of telefoon";

            gfx.DrawString(footer1, footerFont, XBrushes.Gray, new XRect(0, 816, page.Width, 10), XStringFormats.Center);
            gfx.DrawString(footer2, footerFont, XBrushes.Gray, new XRect(0, 823, page.Width, 10), XStringFormats.Center);

            string filename = $"{DateTime.Now:yyyyMMdd}_{invoice.Customer.LastName},{invoice.Customer.FirstName}_Factuur.pdf";
            const string filepath = @"..\..\Invoices\";
            document.Save(filepath + filename);
            return filename;
        }

        private static void DrawImage(XGraphics gfx, string jpegSamplePath, int x, int y, int width, int height)
        {
            XImage image = XImage.FromFile(jpegSamplePath);
            gfx.DrawImage(image, x, y, width, height);
        }

        public static void mailInvoice(string pdf, Invoice invoice)
        {
            try
            {
                MailMessage m = new MailMessage();
                SmtpClient sc = new SmtpClient("smtp.gmail.com");

                m.From = new MailAddress("sales@dopecourses.com");
                m.To.Add("");  // "to" email

                m.Subject = "Confimation on your order #null";
                m.Attachments.Add(new Attachment(@"..\..\Invoices\" + pdf));


                // what I must do for sending a pdf with this email 

                m.Body = $"Heya, {invoice.Customer.FirstName}";
                m.Body += "\n\nWe've succesfully received your order on the DOPE Courses desktop application.";
                m.Body += "\n\nWe've added the invoice for you to check, if something is wrong or if you have any questions, please contact us by mail or phone";
                m.Body += "\n\nYours sincerly";
                m.Body += "\n\nThe Dope Sales team";
                m.Body += "\nTel: +31 6 34 12 32 12";
                m.Body += "\nEmail: sales@dopecourses.co.uk";

                sc.Port = 587;
                sc.Host = "smtp.gmail.com";
                sc.EnableSsl = true;
                sc.Timeout = 10000;
                sc.DeliveryMethod = SmtpDeliveryMethod.Network;
                sc.UseDefaultCredentials = false;
                sc.Credentials = new System.Net.NetworkCredential("dopecourses@gmail.com", "lwcZTyvUJw");

                sc.Send(m);
            }
            catch (Exception ex)
            {
                string bad = "Error: " + ex;
                Debug.WriteLine("shit failed, " + bad);
            }
        }
    }
}
