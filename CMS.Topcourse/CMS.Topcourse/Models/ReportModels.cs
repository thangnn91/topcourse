using System.Data;

namespace CMS.Topcourse.Models
{

    public class ReportCommon
    {
        public ReportData reportTable { get; set; }
        public ReportDataLineChart LineChart { get; set; }
        public ReportDataPieChart PieChart { get; set; }
    }
    public class ReportData
    {
        public bool ShowTitle { get; set; }
        public int? CurrentPage { get; set; }
        public string ReportTitle { get; set; }
        public DataTable dataTable { get; set; }
        public DataRow rowFooter { get; set; }
        public short ReportTime { get; set; }
    }

    public class ReportDataLineChart
    {
        public string ReportTitle { get; set; }
        public string NameLine_ColumnName { get; set; }
        public string Time_Columnname { get; set; }
        public string Value_ColumnName { get; set; }
        public DataTable dataTable { get; set; }

    }
    public class ReportDataPieChart{
        public string ReportTitle { get; set; }
        public string NameLine_ColumnName { get; set; }
        public string Value_ColumnName { get; set; }
        public DataTable dataTable { get; set; }
    }
    public class LineChartSerie
    {
        public string name { get; set; }
        public long[,] data { get; set; }
    }
    public class PieChart
    {
        public string name { get; set; }
        public long y { get; set; }
    }
}