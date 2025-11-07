namespace Suppliers.BL.Reports.Contpaq;

public class ContpaqData : DataReport
{
    public List<ContpaqRow> Rows { get; set; } = new();
}

public class ContpaqRow
{
    public string FolioFiscal { get; set; } = "";
    public DateTime? Fecha { get; set; }
    public string TipoComprobante { get; set; } = "";
    public string RfcEmisor { get; set; } = "";
    public string NombreEmisor { get; set; } = "";
    public string RfcReceptor { get; set; } = "";
    public string NombreReceptor { get; set; } = "";
    public string Serie { get; set; } = "";
    public string Folio { get; set; } = "";
    public decimal SubTotal { get; set; }
    public decimal Descuento { get; set; }
    public decimal Total { get; set; }
    public string Moneda { get; set; } = "";
    public string FormaPago { get; set; } = "";
    public string MetodoPago { get; set; } = "";
    public string UsoCfdi { get; set; } = "";
    public string Estatus { get; set; } = "";
}
