namespace BBIS.Application.DTOs.Reports
{
    public class ExportReportDto<T>
    {
        public List<Columns> Columns { get; set; }
        public List<T> Rows { get; set; }
    }

    public class Columns
    {
        public string Label { get; set; }
        public string Field { get; set; }
    }
}
