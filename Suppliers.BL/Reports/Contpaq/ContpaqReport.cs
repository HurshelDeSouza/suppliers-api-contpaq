using Microsoft.EntityFrameworkCore;

namespace Suppliers.BL.Reports.Contpaq;

public class ContpaqReport : CommonReportConfig<ContpaqData, ContpaqFilterReport>
{
    public ContpaqReport(ContpaqFilterReport filter, DescargaCfdiGfpContext context) : base(filter, context)
    {
    }

    public ContpaqReport(ContpaqFilterReport filter, DescargaCfdiGfpContext context, string keyLogo, string keyPath, string customLogo = "", string customPath = "") 
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
            query = query.Where(c => c.CfdiEmisor.Rfc.Contains(Filter.Rfc) || c.CfdiReceptor.Rfc.Contains(Filter.Rfc));
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

        Data = new ContpaqData
        {
            Rows = comprobantes.Select(c => new ContpaqRow
            {
                TarjetaTipo = c.Fecha.ToString("dd/MM/yyyy"),
                ComprobanteComprobante = c.TipoDeComprobante switch
                {
                    "I" => "Ingreso",
                    "E" => "Egreso",
                    "T" => "Traslado",
                    "N" => "Nómina",
                    "P" => "Pago",
                    _ => c.TipoDeComprobante
                },
                Serie = c.Serie ?? "",
                Folio = c.Folio ?? "",
                RfcEmisor = c.CfdiEmisor?.Rfc ?? "",
                NombreEmisor = c.CfdiEmisor?.Nombre ?? "",
                Marca = c.Moneda ?? "",
                TipoCambio = c.TipoCambio?.ToString() ?? "",
                Total = c.Total,
                Responsable = "",
                Proveedor = "",
                Referencia = "",
                Observaciones = "",
                AbonadoContabilidad = "0",
                AbonadoBancos = "0",
                AbonadoComercial = "0",
                Estatus = c.Estatus ?? "",
                Listo = "",
                EstadoPagado = "",
                Validez = "",
                Forma = c.FormaPago ?? "",
                Metodo = c.MetodoPago ?? "",
                ListadoCancelacionDocumento = "",
                FechaCancelacionDocumento = ""
            }).ToList()
        };
    }

    public override string GenerateExcel(string fileName)
    {
        var fullPath = Path.Combine(FilePath, fileName);
        ContpaqExcel.Generate(Data, fullPath);
        return fullPath;
    }
}
