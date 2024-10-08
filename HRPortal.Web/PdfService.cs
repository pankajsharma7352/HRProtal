﻿using HRPortal.Models.Models;
using iText.Kernel.Colors;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;
using System.Collections.Generic;
using System.IO;


namespace HRPortal.Web
{
    public class PdfService
    {
        public byte[] GeneratePdf(List<ShowEmployeeDetails> employeeDetails)
        {
            using (var memoryStream = new MemoryStream())
            {
                var writerProperties = new WriterProperties();

                    var writer = new PdfWriter(memoryStream);
                    var pdf = new PdfDocument(writer);
                    var document = new Document(pdf);
               
                document.Add(new Paragraph("Employee Details").SetBold().SetFontSize(15));

                var table = new Table(UnitValue.CreatePercentArray(new float[] { 15, 15, 20, 15, 10, 25, 15, 15, 10 }))
    .SetWidth(UnitValue.CreatePercentValue(100));
                    table.AddHeaderCell(new Cell()
                        .Add(new Paragraph("First Name")).SetFontSize(10)
                        .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
                        .SetBold()
                        .SetTextAlignment(TextAlignment.CENTER)
                        .SetVerticalAlignment(VerticalAlignment.MIDDLE)
                        .SetBorder(Border.NO_BORDER));

                    table.AddHeaderCell(new Cell()
                        .Add(new Paragraph("Last Name")).SetFontSize(10)
                        .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
                        .SetBold()
                        .SetTextAlignment(TextAlignment.CENTER)
                        .SetVerticalAlignment(VerticalAlignment.MIDDLE)
                        .SetBorder(Border.NO_BORDER));
                    table.AddHeaderCell(new Cell()
                        .Add(new Paragraph("Email")).SetFontSize(10)
                        .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
                        .SetBold()
                        .SetTextAlignment(TextAlignment.CENTER)
                        .SetVerticalAlignment(VerticalAlignment.MIDDLE)
                        .SetBorder(Border.NO_BORDER));

                    table.AddHeaderCell(new Cell()
                        .Add(new Paragraph("Phone")).SetFontSize(10)
                        .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
                        .SetBold()
                        .SetTextAlignment(TextAlignment.CENTER)
                        .SetVerticalAlignment(VerticalAlignment.MIDDLE)
                        .SetBorder(Border.NO_BORDER));

                    table.AddHeaderCell(new Cell()
                        .Add(new Paragraph("Gender")).SetFontSize(10)
                        .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
                        .SetBold()
                        .SetTextAlignment(TextAlignment.CENTER)
                        .SetVerticalAlignment(VerticalAlignment.MIDDLE)
                        .SetBorder(Border.NO_BORDER));

                    table.AddHeaderCell(new Cell()
                        .Add(new Paragraph("Address")).SetFontSize(10)
                        .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
                        .SetBold()
                        .SetTextAlignment(TextAlignment.CENTER)
                        .SetVerticalAlignment(VerticalAlignment.MIDDLE)
                        .SetBorder(Border.NO_BORDER));

                    table.AddHeaderCell(new Cell()
                        .Add(new Paragraph("City")).SetFontSize(10)
                        .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
                        .SetBold()
                        .SetTextAlignment(TextAlignment.CENTER)
                        .SetVerticalAlignment(VerticalAlignment.MIDDLE)
                        .SetBorder(Border.NO_BORDER));

                    table.AddHeaderCell(new Cell()
                        .Add(new Paragraph("State")).SetFontSize(10)
                        .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
                        .SetBold()
                        .SetTextAlignment(TextAlignment.CENTER)
                        .SetVerticalAlignment(VerticalAlignment.MIDDLE)
                        .SetBorder(Border.NO_BORDER));

                    table.AddHeaderCell(new Cell()
                        .Add(new Paragraph("PinCode")).SetFontSize(10)
                        .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
                        .SetBold()
                        .SetTextAlignment(TextAlignment.CENTER)
                        .SetVerticalAlignment(VerticalAlignment.MIDDLE)
                        .SetBorder(Border.NO_BORDER));


                foreach (var employee in employeeDetails)
                {
                        table.AddCell(new Cell()
                            .Add(new Paragraph(employee.FirstName)).SetFontSize(10)
                            .SetTextAlignment(TextAlignment.LEFT)
                            .SetVerticalAlignment(VerticalAlignment.MIDDLE)
                            .SetBorder(new SolidBorder(ColorConstants.BLACK, 1)));

                        table.AddCell(new Cell()
                            .Add(new Paragraph(employee.LastName)).SetFontSize(10)
                            .SetTextAlignment(TextAlignment.LEFT)
                            .SetVerticalAlignment(VerticalAlignment.MIDDLE)
                            .SetBorder(new SolidBorder(ColorConstants.BLACK, 1)));

                        table.AddCell(new Cell()
                            .Add(new Paragraph(employee.Email)).SetFontSize(10)
                            .SetTextAlignment(TextAlignment.LEFT)
                            .SetVerticalAlignment(VerticalAlignment.MIDDLE)
                            .SetBorder(new SolidBorder(ColorConstants.BLACK, 1)));

                        table.AddCell(new Cell()
                            .Add(new Paragraph(employee.Phone)).SetFontSize(10)
                            .SetTextAlignment(TextAlignment.LEFT)
                            .SetVerticalAlignment(VerticalAlignment.MIDDLE)
                            .SetBorder(new SolidBorder(ColorConstants.BLACK, 1)));

                        table.AddCell(new Cell()
                            .Add(new Paragraph(employee.Gender)).SetFontSize(10)
                            .SetTextAlignment(TextAlignment.LEFT)
                            .SetVerticalAlignment(VerticalAlignment.MIDDLE)
                            .SetBorder(new SolidBorder(ColorConstants.BLACK, 1)));

                        table.AddCell(new Cell()
                            .Add(new Paragraph(employee.AddressLine1)).SetFontSize(10)
                            .SetTextAlignment(TextAlignment.LEFT)
                            .SetVerticalAlignment(VerticalAlignment.MIDDLE)
                            .SetBorder(new SolidBorder(ColorConstants.BLACK, 1)));

                        table.AddCell(new Cell()
                            .Add(new Paragraph(employee.City)).SetFontSize(10)
                            .SetTextAlignment(TextAlignment.LEFT)
                            .SetVerticalAlignment(VerticalAlignment.MIDDLE)
                            .SetBorder(new SolidBorder(ColorConstants.BLACK, 1)));

                        table.AddCell(new Cell()
                            .Add(new Paragraph(employee.State)).SetFontSize(10)
                            .SetTextAlignment(TextAlignment.LEFT)
                            .SetVerticalAlignment(VerticalAlignment.MIDDLE)
                            .SetBorder(new SolidBorder(ColorConstants.BLACK, 1)));

                        table.AddCell(new Cell()
                            .Add(new Paragraph(employee.PinCode)).SetFontSize(10)
                            .SetTextAlignment(TextAlignment.LEFT)
                            .SetVerticalAlignment(VerticalAlignment.MIDDLE)
                            .SetBorder(new SolidBorder(ColorConstants.BLACK, 1)));
         
                }

                document.Add(table);
                document.Close();
               
                return memoryStream.ToArray();
            }
        }
    }
}
