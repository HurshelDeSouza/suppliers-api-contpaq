namespace Suppliers.BL.Reports.Contpaq;

public class RecibidasContpaqData : DataReport
{
    public List<RecibidasContpaqRow> Rows { get; set; } = new();
}

public class RecibidasContpaqRow
{
    public DateTime FechaComprobante { get; set; }
    public string TipoComprobanteDesc { get; set; } = "";
    public string Serie { get; set; } = "";
    public string Folio { get; set; } = "";
    public string RfcEmisor { get; set; } = "";
    public string NombreEmisor { get; set; } = "";
    public string Moneda { get; set; } = "";
    public string TipoCambio { get; set; } = "";
    public decimal Total { get; set; }
    public string Responsable { get; set; } = "";
    public string Proceso { get; set; } = "";
    public string Referencia { get; set; } = "";
    public string Observaciones { get; set; } = "";
    public string AsociadoContabilidad { get; set; } = "";
    public string AsociadoBancos { get; set; } = "";
    public string AsociadoComercial { get; set; } = "";
    public string Estatus { get; set; } = "";
    public string Uuid { get; set; } = "";
    public string EstadoPagado { get; set; } = "";
    public string Validez { get; set; } = "";
    public string FormaPago { get; set; } = "";
    public string MetodoPago { get; set; } = "";
    public string EstadoCancelacionDocumento { get; set; } = "";
    public string FechaCancelacionDocumento { get; set; } = "";
    public string ClaveProdServ { get; set; } = "";
    public string ClaveProdServDesc { get; set; } = "";
    public string ClaveUnidad { get; set; } = "";
    public decimal Importe { get; set; }
    public string Unidad { get; set; } = "";
    public string Descripcion { get; set; } = "";
    public string NumIdentificacion { get; set; } = "";
    public string ClaveUnidadDesc { get; set; } = "";
    public decimal Descuento { get; set; }
    public decimal ValorUnitario { get; set; }
    public decimal Cantidad { get; set; }
}
