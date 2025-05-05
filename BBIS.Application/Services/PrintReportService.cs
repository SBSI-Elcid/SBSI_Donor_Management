using BBIS.Application.Contracts;
using BBIS.Application.DTOs.Reports;
using BBIS.Common.Extensions;
using ClosedXML.Excel;
using NetCoreHTMLToPDF;
using System.Reflection;

namespace BBIS.Application.Services
{
    public class PrintReportService : IPrintReportService
    {
        public byte[] GenerateExcelReport<T>(ExportReportDto<T> reportData, string sheetName)
        {
            try
            {
                var workBook = new XLWorkbook();
                var worksheet = workBook.Worksheets.Add(sheetName);

                var row1 = worksheet.Row(1);
                row1.Style.Fill.SetBackgroundColor(XLColor.FromHtml("#333F4F"));
                row1.Style.Font.Bold = true;
                row1.Style.Font.SetFontColor(XLColor.White);

                // Setup the column headers
                for (var col = 1; col <= reportData.Columns.Count; col++)
                {
                    worksheet.Cell(1, col).Value = reportData.Columns[col - 1].Label;
                }

                int xrow = 2;

                for (int row = 0; row < reportData.Rows.Count(); row++)
                {
                    int col = 0;

                    foreach (var colField in reportData.Columns)
                    {
                        Type type = reportData.Rows[row].GetType();
                        PropertyInfo prop = type.GetProperty(colField.Field);

                        var value = prop.GetValue(reportData.Rows[row], null);
                        if (value.GetType() == typeof(DateTime))
                        {
                            var date = (DateTime)value;

                            if (date.Kind == DateTimeKind.Utc)
                            {
                                value = date.GetPHDateTimeFromUtc().ToString("MMM-dd-yyyy");
                            }
                            else
                            {
                                value = date.ToString("MMM-dd-yyyy");
                            }
                        }

                        worksheet.Cell(xrow, col + 1).Value = value;

                        col++;
                    }

                    xrow += 1;
                }

                worksheet.Columns().AdjustToContents();

                var outputStream = new MemoryStream();

                workBook.SaveAs(outputStream);

                return outputStream.ToArray();
            }
            catch (Exception)
            {
                throw;
            }
        }


        public byte[] GeneratePdfReport(string html)
        {
            try
            {
                var converter = new HtmlConverter();
                return converter.FromHtmlString(html);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
