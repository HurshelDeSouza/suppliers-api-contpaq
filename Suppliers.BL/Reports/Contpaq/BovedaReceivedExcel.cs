using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using Suppliers.BL.Helpers.ExtensionMethods;

namespace Suppliers.BL.Reports.Contpaq;

public class BovedaReceivedExcel
{
    public static async void Generate(BovedaReceivedData reportData, string fullPath)
    {
        if (reportData == null)
        {
            return;
        }

        IWorkbook book = new XSSFWorkbook();
        var sheet = book.CreateSheet("Boveda Received");

        var boldStyle = book.EstiloNegritas();
        var dateStyle = book.EstiloFormato(ExcelMethods.FormatoFecha);
        var moneyStyle = book.EstiloFormato(ExcelMethods.FormatoMonedaExcel("$"));

        var indexRow = 0;

        // Agregar encabezado
        indexRow = sheet.AgregarEncabezado("Boveda Received", indexRow, reportData.Rows.Count);
        indexRow++;

        // Definir headers (19 columnas)
        string[] headers = {
            "Movimiento",
            "Tipo de Comprobante Fiscal",
            "RFC",
            "Emisor",
            "Fecha",
            "Fecha de Pago",
            "Documento Relacionado",
            "Uso de CFDI",
            "Método de Pago",
            "Forma de Pago",
            "Regimen Fiscal",
            "UUID",
            "Moneda",
            "Tipo de Cambio",
            "Tasa de Impuesto",
            "Traslados",
            "Retenidos",
            "Subtotal",
            "Total"
        };

        var rowTable = indexRow;
        sheet.AgregarTituloTabla(headers, 0, indexRow++);

        // Agregar datos
        foreach (var row in reportData.Rows)
        {
            var excelRow = sheet.CreateRow(indexRow++);
            var col = 0;

            excelRow.EscribirValor(col++, row.Movimiento);
            excelRow.EscribirValor(col++, row.TipoComprobanteFiscal);
            excelRow.EscribirValor(col++, row.Rfc);
            excelRow.EscribirValor(col++, row.Emisor);
            excelRow.EscribirValor(col++, row.Fecha, dateStyle);
            if (row.FechaPago.HasValue)
            {
                excelRow.EscribirValor(col++, row.FechaPago.Value, dateStyle);
            }
            else
            {
                excelRow.EscribirValor(col++, "");
            }
            excelRow.EscribirValor(col++, row.DocumentoRelacionado);
            excelRow.EscribirValor(col++, row.UsoCfdi);
            excelRow.EscribirValor(col++, row.MetodoPago);
            excelRow.EscribirValor(col++, row.FormaPago);
            excelRow.EscribirValor(col++, row.RegimenFiscal);
            excelRow.EscribirValor(col++, row.Uuid);
            excelRow.EscribirValor(col++, row.Moneda);
            excelRow.EscribirValor(col++, (double)row.TipoCambio);
            excelRow.EscribirValor(col++, row.TasaImpuesto);
            excelRow.EscribirValor(col++, (double)row.Traslados, moneyStyle);
            excelRow.EscribirValor(col++, (double)row.Retenidos, moneyStyle);
            excelRow.EscribirValor(col++, (double)row.Subtotal, moneyStyle);
            excelRow.EscribirValor(col++, (double)row.Total, moneyStyle);
        }

        // Agregar fila de totales
        var totalRow = sheet.CreateRow(indexRow++);
        totalRow.EscribirValor(0, "Totales", boldStyle);
        totalRow.EscribirValor(15, (double)reportData.Rows.Sum(r => r.Traslados), moneyStyle);
        totalRow.EscribirValor(16, (double)reportData.Rows.Sum(r => r.Retenidos), moneyStyle);
        totalRow.EscribirValor(17, (double)reportData.Rows.Sum(r => r.Subtotal), moneyStyle);
        totalRow.EscribirValor(18, (double)reportData.Rows.Sum(r => r.Total), moneyStyle);

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
