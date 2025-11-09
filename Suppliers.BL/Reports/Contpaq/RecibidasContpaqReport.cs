using Microsoft.EntityFrameworkCore;

namespace Suppliers.BL.Reports.Contpaq;

public class RecibidasContpaqReport : CommonReportConfig<RecibidasContpaqData, ContpaqFilterReport>
{
    public RecibidasContpaqReport(ContpaqFilterReport filter, DescargaCfdiGfpContext context) : base(filter, context)
    {
    }

    public RecibidasContpaqReport(ContpaqFilterReport filter, DescargaCfdiGfpContext context, string keyLogo, string keyPath, string customLogo = "", string customPath = "")
        : base(filter, context, keyLogo, keyPath, customLogo, customPath)
    {
    }

    protected override async Task SetData()
    {
        var query = Context.CfdiComprobantes
            .Include(c => c.CfdiEmisor)
            .Include(c => c.CfdiReceptor)
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
            query = query.Where(c => c.CfdiReceptor.Rfc.Contains(Filter.Rfc));
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

        var rows = new List<RecibidasContpaqRow>();
        foreach (var comprobante in comprobantes)
        {
            foreach (var concepto in comprobante.CfdiConceptos)
            {
                rows.Add(new RecibidasContpaqRow
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
                    RfcEmisor = comprobante.CfdiEmisor?.Rfc ?? "",
                    NombreEmisor = comprobante.CfdiEmisor?.Nombre ?? "",
                    Moneda = comprobante.Moneda ?? "",
                    TipoCambio = comprobante.TipoCambio?.ToString() ?? "",
                    Total = comprobante.Total,
                    Responsable = "",
                    Proceso = "",
                    Referencia = "",
                    Observaciones = "",
                    AsociadoContabilidad = "0",
                    AsociadoBancos = "0",
                    AsociadoComercial = "0",
                    Estatus = comprobante.Estatus ?? "",
                    Uuid = comprobante.Uuid,
                    EstadoPagado = "",
                    Validez = "",
                    FormaPago = comprobante.FormaPago ?? "",
                    MetodoPago = comprobante.MetodoPago ?? "",
                    EstadoCancelacionDocumento = "",
                    FechaCancelacionDocumento = "",
                    ClaveProdServ = concepto.ClaveProdServ ?? "",
                    ClaveProdServDesc = concepto.ClaveProdServ ?? "",
                    ClaveUnidad = concepto.ClaveUnidad ?? "",
                    Importe = concepto.Importe,
                    Unidad = concepto.Unidad ?? "",
                    Descripcion = concepto.Descripcion ?? "",
                    NumIdentificacion = concepto.NoIdentificacion ?? "",
                    ClaveUnidadDesc = concepto.ClaveUnidad ?? "",
                    Descuento = concepto.Descuento ?? 0,
                    ValorUnitario = concepto.ValorUnitario,
                    Cantidad = concepto.Cantidad
                });
            }
        }

        Data = new RecibidasContpaqData
        {
            Rows = rows
        };
    }

    public override string GenerateExcel(string fileName)
    {
        var fullPath = Path.Combine(FilePath, fileName);
        RecibidasContpaqExcel.Generate(Data, fullPath);
        return fullPath;
    }
}
