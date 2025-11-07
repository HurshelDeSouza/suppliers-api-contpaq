namespace Suppliers.BL.Reports.Contpaq;

public class ContpaqFilterReport : FilterReport
{
    public string FolioFiscal { get; set; } = "";
    public string FechaInicial { get; set; } = "";
    public string FechaFinal { get; set; } = "";
    public string? TipoComprobante { get; set; }
    public string Rfc { get; set; } = "";
    public string Serie { get; set; } = "";
    public string Folio { get; set; } = "";
}
