using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using Suppliers.BL.Helpers.ExtensionMethods;

namespace Suppliers.BL.Reports.Contpaq;

public class EmitidasContpaqExcel
{
    public static async void Generate(EmitidasContpaqData reportData, string fullPath)
    {
        if (reportData == null)
        {
            return;
        }

        IWorkbook book = new XSSFWorkbook();
        var sheet = book.CreateSheet("Emitidas CONTPAQ");

        var boldStyle = book.EstiloNegritas();
        var dateStyle = book.EstiloFormato(ExcelMethods.FormatoFecha);
        var moneyStyle = book.EstiloFormato(ExcelMethods.FormatoMonedaExcel("$"));

        var indexRow = 0;

        // Agregar encabezado
        indexRow = sheet.AgregarEncabezado("Emitidas CONTPAQ", indexRow, reportData.Rows.Count);
        indexRow++;

        // Definir headers (32 columnas)
        string[] headers = {
            "Fecha Comprobante",
            "Tipo Comprobante Desc",
            "Serie",
            "Folio",
            "RFC Receptor",
            "Nombre Receptor",
            "Régimen Fiscal",
            "Moneda",
            "Tipo Cambio",
            "Total",
            "Referencia",
            "Observaciones",
            "Asociado Contabilidad",
            "Asociado Bancos",
            "Asociado Comercial",
            "Estatus",
            "UUID",
            "Forma Pago",
            "Método Pago",
            "Estado de Cancelación del Documento",
            "Fecha de Cancelación del Documento",
            "Clave Prod/Serv",
            "Descripción",
            "Unidad",
            "Importe",
            "Clave Unidad",
            "Clave Prod/Serv Desc",
            "Núm. Identificación",
            "Clave Unidad Desc",
            "Descuento",
            "Valor Unitario",
            "Cantidad"
        };

        var rowTable = indexRow;
        sheet.AgregarTituloTabla(headers, 0, indexRow++);

        // Agregar datos (32 columnas)
        foreach (var row in reportData.Rows)
        {
            var excelRow = sheet.CreateRow(indexRow++);
            var col = 0;

            excelRow.EscribirValor(col++, row.FechaComprobante, dateStyle);
            excelRow.EscribirValor(col++, row.TipoComprobanteDesc);
            excelRow.EscribirValor(col++, row.Serie);
            excelRow.EscribirValor(col++, row.Folio);
            excelRow.EscribirValor(col++, row.RfcReceptor);
            excelRow.EscribirValor(col++, row.NombreReceptor);
            excelRow.EscribirValor(col++, row.RegimenFiscal);
            excelRow.EscribirValor(col++, row.Moneda);
            excelRow.EscribirValor(col++, row.TipoCambio);
            excelRow.EscribirValor(col++, (double)row.Total, moneyStyle);
            excelRow.EscribirValor(col++, row.Referencia);
            excelRow.EscribirValor(col++, row.Observaciones);
            excelRow.EscribirValor(col++, row.AsociadoContabilidad);
            excelRow.EscribirValor(col++, row.AsociadoBancos);
            excelRow.EscribirValor(col++, row.AsociadoComercial);
            excelRow.EscribirValor(col++, row.Estatus);
            excelRow.EscribirValor(col++, row.Uuid);
            excelRow.EscribirValor(col++, row.FormaPago);
            excelRow.EscribirValor(col++, row.MetodoPago);
            excelRow.EscribirValor(col++, row.EstadoCancelacionDocumento);
            excelRow.EscribirValor(col++, row.FechaCancelacionDocumento);
            excelRow.EscribirValor(col++, row.ClaveProdServ);
            excelRow.EscribirValor(col++, row.Descripcion);
            excelRow.EscribirValor(col++, row.Unidad);
            excelRow.EscribirValor(col++, (double)row.Importe, moneyStyle);
            excelRow.EscribirValor(col++, row.ClaveUnidad);
            excelRow.EscribirValor(col++, row.ClaveProdServDesc);
            excelRow.EscribirValor(col++, row.NumIdentificacion);
            excelRow.EscribirValor(col++, row.ClaveUnidadDesc);
            excelRow.EscribirValor(col++, (double)row.Descuento, moneyStyle);
            excelRow.EscribirValor(col++, (double)row.ValorUnitario, moneyStyle);
            excelRow.EscribirValor(col++, (double)row.Cantidad);
        }

        // Aplicar autofiltro
        sheet.SetAutoFilter(new NPOI.SS.Util.CellRangeAddress(rowTable, indexRow - 1, 0, headers.Length - 1));

        // Ajustar ancho de columnas
        for (int i = 0; i < headers.Length; i++)
        {
            sheet.AutoSizeColumn(i);
        }

        await using var fs = new FileStream(fullPath, FileMode.Create, FileAccess.Write);
        book.Write(fs, false);
        fs.Close();
    }
}
