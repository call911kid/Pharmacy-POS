using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using BLL.DTOs.Invoice;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using ZXing;
using ZXing.Common;
using ZXing.Rendering;
using DrawingImageFormat = System.Drawing.Imaging.ImageFormat;

namespace UI.Printing
{
    public sealed class InvoicePdfGenerator : IInvoicePdfGenerator
    {
        public InvoicePdfGenerator()
        {
            QuestPDF.Settings.License = LicenseType.Community;
        }

        public async Task<string> GenerateAsync(InvoiceDto invoice, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(invoice);

            var outputDirectory = Path.Combine(Path.GetTempPath(), "PharmacyPOS", "Invoices");
            Directory.CreateDirectory(outputDirectory);

            var outputPath = Path.Combine(
                outputDirectory,
                $"invoice-{invoice.Id}-{DateTime.Now:yyyyMMddHHmmss}.pdf");

            var barcodeValue = string.IsNullOrWhiteSpace(invoice.Barcode)
                ? $"INV-{invoice.Id:000000}"
                : invoice.Barcode.Trim();
            var barcodeBytes = CreateBarcodeImage(barcodeValue);

            await Task.Run(() =>
            {
                Document.Create(container =>
                {
                    container.Page(page =>
                    {
                        page.Size(PageSizes.A4);
                        page.Margin(28);
                        page.DefaultTextStyle(x => x.FontSize(10));

                        page.Header().Column(column =>
                        {
                            column.Item().Text("Pharmacy POS").Bold().FontSize(18);
                            column.Item().PaddingTop(4).Row(row =>
                            {
                                row.RelativeItem().Column(info =>
                                {
                                    info.Item().Text($"Invoice #{invoice.Id}");
                                    info.Item().Text($"Date: {invoice.InvoiceDate:dd-MMM-yyyy hh:mm tt}");

                                    if (!string.IsNullOrWhiteSpace(invoice.CustomerName))
                                    {
                                        info.Item().Text($"Customer: {invoice.CustomerName}");
                                    }

                                    if (!string.IsNullOrWhiteSpace(invoice.CustomerPhone))
                                    {
                                        info.Item().Text($"Phone: {invoice.CustomerPhone}");
                                    }
                                });

                                row.ConstantItem(220).Column(barcode =>
                                {
                                    barcode.Item().AlignRight().Image(barcodeBytes);
                                    barcode.Item().PaddingTop(4).AlignCenter().Text(barcodeValue)
                                        .FontSize(9)
                                        .SemiBold();
                                });
                            });
                        });

                        page.Content().PaddingVertical(18).Column(column =>
                        {
                            column.Item().Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn(4);
                                    columns.ConstantColumn(60);
                                    columns.ConstantColumn(90);
                                    columns.ConstantColumn(90);
                                });

                                static IContainer HeaderCell(IContainer container) =>
                                    container
                                        .BorderBottom(1)
                                        .BorderColor(Colors.Grey.Lighten2)
                                        .PaddingVertical(6)
                                        .PaddingHorizontal(4);

                                static IContainer BodyCell(IContainer container) =>
                                    container
                                        .BorderBottom(1)
                                        .BorderColor(Colors.Grey.Lighten3)
                                        .PaddingVertical(6)
                                        .PaddingHorizontal(4);

                                table.Header(header =>
                                {
                                    header.Cell().Element(HeaderCell).Text("Product").SemiBold();
                                    header.Cell().Element(HeaderCell).Text("Qty").SemiBold();
                                    header.Cell().Element(HeaderCell).AlignRight().Text("Unit Price").SemiBold();
                                    header.Cell().Element(HeaderCell).AlignRight().Text("Total").SemiBold();
                                });

                                foreach (var item in invoice.InvoiceItems)
                                {
                                    table.Cell().Element(BodyCell).Text(
                                        string.IsNullOrWhiteSpace(item.ProductName)
                                            ? $"Product #{item.ProductId}"
                                            : item.ProductName);
                                    table.Cell().Element(BodyCell).Text(item.Quantity.ToString());
                                    table.Cell().Element(BodyCell).AlignRight().Text($"{item.UnitPrice:0.00} EGP");
                                    table.Cell().Element(BodyCell).AlignRight().Text($"{item.TotalPrice:0.00} EGP");
                                }
                            });

                            column.Item().PaddingTop(18).AlignRight().Width(220).Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                });

                                table.Cell().Padding(4).Text("Status").SemiBold();
                                table.Cell().Padding(4).AlignRight().Text(invoice.Status);
                                table.Cell().Padding(4).Text("Total").SemiBold();
                                table.Cell().Padding(4).AlignRight().Text($"{invoice.TotalAmount:0.00} EGP").Bold();
                            });
                        });

                        page.Footer().AlignCenter().Text("Generated by Pharmacy POS");
                    });
                }).GeneratePdf(outputPath);
            }, cancellationToken);

            return outputPath;
        }

        private static byte[] CreateBarcodeImage(string barcodeValue)
        {
            var writer = new BarcodeWriterPixelData
            {
                Format = BarcodeFormat.CODE_128,
                Options = new EncodingOptions
                {
                    Width = 360,
                    Height = 90,
                    Margin = 4,
                    PureBarcode = false
                }
            };

            var pixelData = writer.Write(barcodeValue);

            using var bitmap = new Bitmap(pixelData.Width, pixelData.Height, PixelFormat.Format32bppRgb);
            var bitmapData = bitmap.LockBits(
                new Rectangle(0, 0, pixelData.Width, pixelData.Height),
                ImageLockMode.WriteOnly,
                PixelFormat.Format32bppRgb);

            try
            {
                Marshal.Copy(pixelData.Pixels, 0, bitmapData.Scan0, pixelData.Pixels.Length);
            }
            finally
            {
                bitmap.UnlockBits(bitmapData);
            }

            using var stream = new MemoryStream();
            bitmap.Save(stream, DrawingImageFormat.Png);
            return stream.ToArray();
        }
    }
}
