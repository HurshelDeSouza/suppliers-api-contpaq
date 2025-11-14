namespace Suppliers.BL.Reports.Contpaq;

public class BovedaReceivedData : DataReport
{
    public List<BovedaReceivedRow> Rows { get; set; } = new();
}

public class BovedaReceivedRow
{
    public string Movimiento { get; set; } = "";
    public string TipoComprobanteFiscal { get; set; } = "";
    public string Rfc { get; set; } = "";
    public string Emisor { get; set; } = "";
    public DateTime Fecha { get; set; }
    public DateTime? FechaPago { get; set; }
    public string DocumentoRelacionado { get; set; } = "";
    public string UsoCfdi { get; set; } = "";
    public string MetodoPago { get; set; } = "";
    public string FormaPago { get; set; } = "";
    public string RegimenFiscal { get; set; } = "";
    public string Uuid { get; set; } = "";
    public string Moneda { get; set; } = "";
    public decimal TipoCambio { get; set; }
    public string TasaImpuesto { get; set; } = "";
    public decimal Traslados { get; set; }
    public decimal Retenidos { get; set; }
    public decimal Subtotal { get; set; }
    public decimal Total { get; set; }
}
