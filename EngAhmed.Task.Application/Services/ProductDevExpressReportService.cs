using DevExpress.XtraReports.UI;
using EngAhmed.TaskP.Application.Contracts.IAppService;
using EngAhmed.TaskP.Application.Dto.DProduct;
using System.Drawing;

namespace EngAhmed.TaskP.Application.Services
{
    public class ProductDevExpressReportService : IProductDevExpressReportService
    {
        public byte[] GenerateDevExpressProductReport(List<ProductDto> products, string type)
        {
            XtraReport _report = new XtraReport();

            var _headerBand = new ReportHeaderBand();
            _headerBand.Controls.Add(new XRLabel()
            {
                Text = "Product DevExpress Report",
                BoundsF = new RectangleF(0, 0, 650, 40),
                Font = new Font("Arial", 16, FontStyle.Bold),
                TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 15)
            });

            var _logo = new XRPictureBox()
            {
                ImageUrl = "C:\\Users\\dell\\OneDrive\\Desktop\\portfile\\whats app\\whats app\\download (1).jpeg",
                BoundsF = new RectangleF(0, 25, 50, 50),
                Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage,
                Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0)
            };
            _headerBand.Controls.Add(_logo);

            _headerBand.Controls.Add(new XRLabel()
            {
                Text = $"Report Date: {DateTime.Now:dd/MM/yyyy}",
                BoundsF = new RectangleF(500, 50, 150, 20),
                Font = new Font("Arial", 10),
                TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight,
                Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 5, 5)
            });

            _report.Bands.Add(_headerBand);

            DetailBand _detailBand = new DetailBand();
            _detailBand.HeightF = 20;
            _report.Bands.Add(_detailBand);

            XRTable _table = new XRTable();
            _table.WidthF = 650;
            XRTableRow _headerRow = new XRTableRow();
            _table.Rows.Add(_headerRow);

            _headerRow.Cells.Add(new XRTableCell()
            {
                Text = "ID",
                WidthF = 100,
                Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 5, 5)
            });
            _headerRow.Cells.Add(new XRTableCell()
            {
                Text = "Name",
                WidthF = 300,
                Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 5, 5)
            });
            _headerRow.Cells.Add(new XRTableCell()
            {
                Text = "Price",
                WidthF = 250,
                Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 5, 5)
            });

            foreach (var _product in products)
            {
                XRTableRow _row = new XRTableRow();
                _row.Cells.Add(new XRTableCell()
                {
                    Text = _product.Id.ToString(),
                    Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 5, 5),

                });
                _row.Cells.Add(new XRTableCell()
                {
                    Text = _product.Name,
                    Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 5, 5)

                });
                _row.Cells.Add(new XRTableCell()
                {
                    Text = _product.Price.ToString("C"),
                    TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft,
                    Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 5, 5)

                });
                _table.Rows.Add(_row);
            }
            _detailBand.Controls.Add(_table);

            var _footerBand = new ReportFooterBand();
            _footerBand.Controls.Add(new XRLabel()
            {
                Text = $"Total Products: {products.Count}",
                BoundsF = new RectangleF(0, 0, 650, 40),
                Font = new Font("Arial", 12, FontStyle.Bold),
                TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                Padding = new DevExpress.XtraPrinting.PaddingInfo(10, 10, 50, 10)
            });

            _report.Bands.Add(_footerBand);
            if (type == "pdf") { 
                using (var _stream = new MemoryStream())
                {
                    _report.ExportToPdf(_stream);
                    return _stream.ToArray();
                }
                }else if(type == "Excel")
            {
                using (var _stream = new MemoryStream())
                {
                    _report.ExportToXlsx(_stream);
                    return _stream.ToArray();
                }

            }
            using (var _stream = new MemoryStream())
            {
                _report.ExportToDocx(_stream);
                return _stream.ToArray();
            }
        }
      
    }
}
