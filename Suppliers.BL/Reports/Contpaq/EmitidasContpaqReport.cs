using Microsoft.EntityFrameworkCore;

namespace Suppliers.BL.Reports.Contpaq;

public class EmitidasContpaqReport : CommonReportConfig<EmitidasContpaqData, ContpaqFilterReport>
{
    public EmitidasContpaqReport(ContpaqFilterReport filter, DescargaCfdiGfpContext context) : base(filter, context)
    {
    }

    public EmitidasContpaqReport(ContpaqFilterReport filter, DescargaCfdiGfpContext context, string keyLogo, string keyPath, string customLogo = "", string customPath = "")
        : base(filter, context, keyLogo, keyPath, customLogo, customPath)
    {
    }

    protected override async Task SetData()
    {
        var query = Context.CfdiComprobantes
            .Include(c => c.CfdiEmisor)
            .Include(c => c.CfdiReceptor)
            .Include(c => c.CfdiConceptos)
            .AsQueryable();

        // Aplicar filtros
        if (!string.IsNullOrEmpty(Filter.FolioFiscal))
        {
            query = query.Where(c => c.Uuid.Contains(Filter.FolioFiscal));
        }

        if (!string.IsNullOrEmpty(Filter.FechaInicial) && DateTime.TryParseExact(Filter.FechaInicial, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out var fechaInicial))
        {
            query = query.Where(c => c.Fecha >= fechaInicial);
        }

        if (!string.IsNullOrEmpty(Filter.FechaFinal) && DateTime.TryParseExact(Filter.FechaFinal, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out var fechaFinal))
        {
            query = query.Where(c => c.Fecha <= fechaFinal);
        }

        if (!string.IsNullOrEmpty(Filter.TipoComprobante))
        {
            query = query.Where(c => c.TipoDeComprobante == Filter.TipoComprobante);
        }

        if (!string.IsNullOrEmpty(Filter.Rfc))
        {
            query = query.Where(c => c.CfdiEmisor.Rfc.Contains(Filter.Rfc));
        }

        if (!string.IsNullOrEmpty(Filter.Serie))
        {
            query = query.Where(c => c.Serie.Contains(Filter.Serie));
        }

        if (!string.IsNullOrEmpty(Filter.Folio))
        {
            query = query.Where(c => c.Folio.Contains(Filter.Folio));
        }

        var comprobantes = await query.ToListAsync();

        var rows = new List<EmitidasContpaqRow>();
        foreach (var comprobante in comprobantes)
        {
            foreach (var concepto in comprobante.CfdiConceptos)
            {
                rows.Add(new EmitidasContpaqRow
                {
                    FechaComprobante = comprobante.Fecha,
                    TipoComprobanteDesc = comprobante.TipoDeComprobante switch
                    {
                        "I" => "Ingreso",
                        "E" => "Egreso",
                        "T" => "Traslado",
                        "N" => "Nómina",
                        "P" => "Pago",
                        _ => comprobante.TipoDeComprobante
                    },
                    Serie = comprobante.Serie ?? "",
                    Folio = comprobante.Folio ?? "",
                    RfcReceptor = comprobante.CfdiReceptor?.Rfc ?? "",
                    NombreReceptor = comprobante.CfdiReceptor?.Nombre ?? "",
                    RegimenFiscal = comprobante.CfdiReceptor?.RegimenFiscalReceptor ?? "",
                    Moneda = comprobante.Moneda ?? "",
                    TipoCambio = comprobante.TipoCambio?.ToString() ?? "",
                    Total = comprobante.Total,
                    Referencia = "",
                    Observaciones = "",
                    AsociadoContabilidad = "0",
                    AsociadoBancos = "0",
                    AsociadoComercial = "0",
                    Estatus = comprobante.Estatus ?? "",
                    Uuid = comprobante.Uuid,
                    FormaPago = comprobante.FormaPago ?? "",
                    MetodoPago = comprobante.MetodoPago ?? "",
                    EstadoCancelacionDocumento = "",
                    FechaCancelacionDocumento = "",
                    ClaveProdServ = concepto.ClaveProdServ ?? "",
                    Descripcion = concepto.Descripcion ?? "",
                    Unidad = concepto.Unidad ?? "",
                    Importe = concepto.Importe,
                    ClaveUnidad = concepto.ClaveUnidad ?? "",
                    ClaveProdServDesc = concepto.ClaveProdServ ?? "",
                    NumIdentificacion = concepto.NoIdentificacion ?? "",
                    ClaveUnidadDesc = concepto.ClaveUnidad ?? "",
                    Descuento = concepto.Descuento ?? 0,
                    ValorUnitario = concepto.ValorUnitario,
                    Cantidad = concepto.Cantidad
                });
            }
        }

        Data = new EmitidasContpaqData
        {
            Rows = rows
        };
    }

    public override string GenerateExcel(string fileName)
    {
        var fullPath = Path.Combine(FilePath, fileName);
        EmitidasContpaqExcel.Generate(Data, fullPath);
        return fullPath;
    }
}
