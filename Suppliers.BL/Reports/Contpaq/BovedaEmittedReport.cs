using Microsoft.EntityFrameworkCore;

namespace Suppliers.BL.Reports.Contpaq;

public class BovedaEmittedReport : CommonReportConfig<BovedaEmittedData, ContpaqFilterReport>
{
    public BovedaEmittedReport(ContpaqFilterReport filter, DescargaCfdiGfpContext context) : base(filter, context)
    {
    }

    public BovedaEmittedReport(ContpaqFilterReport filter, DescargaCfdiGfpContext context, string keyLogo, string keyPath, string customLogo = "", string customPath = "")
        : base(filter, context, keyLogo, keyPath, customLogo, customPath)
    {
    }

    protected override async Task SetData()
    {
        var query = Context.CfdiComprobantes
            .Include(c => c.CfdiEmisor)
            .Include(c => c.CfdiReceptor)
            .Include(c => c.CfdiConceptos)
                .ThenInclude(co => co.CfdiTrasladoConceptos)
            .Include(c => c.CfdiConceptos)
                .ThenInclude(co => co.CfdiRetencionConceptos)
            .Include(c => c.PagosPagos)
                .ThenInclude(p => p.PagosDoctoRelacionados)
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

        var rows = new List<BovedaEmittedRow>();
        foreach (var comprobante in comprobantes)
        {
            // Calcular traslados y retenidos por comprobante
            decimal totalTraslados = 0;
            decimal totalRetenidos = 0;
            string tasaImpuesto = "";

            foreach (var concepto in comprobante.CfdiConceptos)
            {
                if (concepto.CfdiTrasladoConceptos != null && concepto.CfdiTrasladoConceptos.Any())
                {
                    foreach (var traslado in concepto.CfdiTrasladoConceptos)
                    {
                        totalTraslados += traslado.Importe ?? 0m;
                    }
                }
                if (concepto.CfdiRetencionConceptos != null && concepto.CfdiRetencionConceptos.Any())
                {
                    foreach (var retencion in concepto.CfdiRetencionConceptos)
                    {
                        totalRetenidos += retencion.Importe;
                    }
                }

                // Obtener tasa de impuesto del primer traslado encontrado
                if (string.IsNullOrEmpty(tasaImpuesto))
                {
                    var primerTraslado = concepto.CfdiTrasladoConceptos?.FirstOrDefault();
                    if (primerTraslado != null)
                    {
                        tasaImpuesto = primerTraslado.TasaOcuota.ToString("0.000000");
                    }
                }
            }

            // Obtener información de pagos si existen
            DateTime? fechaPago = null;
            string documentoRelacionado = "";
            
            var primerPago = comprobante.PagosPagos?.FirstOrDefault();
            if (primerPago != null)
            {
                fechaPago = primerPago.FechaPago;
                var docRelacionado = primerPago.PagosDoctoRelacionados?.FirstOrDefault();
                if (docRelacionado != null)
                {
                    documentoRelacionado = docRelacionado.IdDocumento ?? "";
                }
            }

            rows.Add(new BovedaEmittedRow
            {
                Movimiento = $"{comprobante.Serie ?? ""}{comprobante.Folio ?? ""}",
                TipoComprobanteFiscal = comprobante.TipoDeComprobante switch
                {
                    "I" => "Ingreso",
                    "E" => "Egreso",
                    "T" => "Traslado",
                    "N" => "Nómina",
                    "P" => "Pago",
                    _ => comprobante.TipoDeComprobante
                },
                Rfc = comprobante.CfdiReceptor?.Rfc ?? "",
                Emisor = comprobante.CfdiReceptor?.Nombre ?? "",
                Fecha = comprobante.Fecha,
                FechaPago = fechaPago,
                DocumentoRelacionado = documentoRelacionado,
                UsoCfdi = comprobante.CfdiReceptor?.UsoCfdi ?? "",
                MetodoPago = comprobante.MetodoPago ?? "",
                FormaPago = comprobante.FormaPago ?? "",
                RegimenFiscal = comprobante.CfdiReceptor?.RegimenFiscalReceptor ?? "",
                Uuid = comprobante.Uuid,
                Moneda = comprobante.Moneda ?? "",
                TipoCambio = comprobante.TipoCambio ?? 1m,
                TasaImpuesto = tasaImpuesto,
                Traslados = totalTraslados,
                Retenidos = totalRetenidos,
                Subtotal = comprobante.SubTotal,
                Total = comprobante.Total
            });
        }

        Data = new BovedaEmittedData
        {
            Rows = rows
        };
    }

    public override string GenerateExcel(string fileName)
    {
        var fullPath = Path.Combine(FilePath, fileName);
        BovedaEmittedExcel.Generate(Data, fullPath);
        return fullPath;
    }
}
