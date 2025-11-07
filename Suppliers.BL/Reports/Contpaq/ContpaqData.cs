namespace Suppliers.BL.Reports.Contpaq;

public class ContpaqData : DataReport
{
    public List<ContpaqRow> Rows { get; set; } = new();
}

public class ContpaqRow
{
    public string TarjetaTipo { get; set; } = "";
    public string ComprobanteComprobante { get; set; } = "";
    public string Serie { get; set; } = "";
    public string Folio { get; set; } = "";
    public string RfcEmisor { get; set; } = "";
    public string NombreEmisor { get; set; } = "";
    public string Marca { get; set; } = "";
    public string TipoCambio { get; set; } = "";
    public decimal Total { get; set; }
    public string Responsable { get; set; } = "";
    public string Proveedor { get; set; } = "";
    public string Referencia { get; set; } = "";
    public string Observaciones { get; set; } = "";
    public string AbonadoContabilidad { get; set; } = "";
    public string AbonadoBancos { get; set; } = "";
    public string AbonadoComercial { get; set; } = "";
    public string Estatus { get; set; } = "";
    public string Listo { get; set; } = "";
    public string EstadoPagado { get; set; } = "";
    public string Validez { get; set; } = "";
    public string Forma { get; set; } = "";
    public string Metodo { get; set; } = "";
    public string ListadoCancelacionDocumento { get; set; } = "";
    public string FechaCancelacionDocumento { get; set; } = "";
}
