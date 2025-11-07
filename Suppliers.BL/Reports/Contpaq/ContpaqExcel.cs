using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using Suppliers.BL.Helpers.ExtensionMethods;

namespace Suppliers.BL.Reports.Contpaq;

public class ContpaqExcel
{
    public static async void Generate(ContpaqData reportData, string fullPath)
    {
        if (reportData == null)
        {
            return;
        }

        IWorkbook book = new XSSFWorkbook();
        var sheet = book.CreateSheet("CONTPAQ");
        
        var boldStyle = book.EstiloNegritas();
        var dateStyle = book.EstiloFormato(ExcelMethods.FormatoFecha);
        var moneyStyle = book.EstiloFormato(ExcelMethods.FormatoMonedaExcel("$"));

        var indexRow = 0;

        // Agregar encabezado
        indexRow = sheet.AgregarEncabezado("Reporte CONTPAQ", indexRow, reportData.Rows.Count);
        indexRow++;

        // Definir headers
        string[] headers = { 
            "Folio Fiscal (UUID)", 
            "Fecha", 
            "Tipo Comprobante", 
            "RFC Emisor", 
            "Nombre Emisor",
            "RFC Receptor",
            "Nombre Receptor",
            "Serie",
            "Folio",
            "SubTotal",
            "Descuento",
            "Total",
            "Moneda",
            "Forma Pago",
            "Método Pago",
            "Uso CFDI",
            "Estatus"
        };

        var rowTable = indexRow;
        sheet.AgregarTituloTabla(headers, 0, indexRow++);

        // Agregar datos
        foreach (var row in reportData.Rows)
        {
            var excelRow = sheet.CreateRow(indexRow++);
            var col = 0;
            
            excelRow.EscribirValor(col++, row.FolioFiscal);
            if (row.Fecha.HasValue)
            {
                excelRow.EscribirValor(col++, row.Fecha.Value, dateStyle);
            }
            else
            {
                excelRow.EscribirValor(col++, "");
            }
            excelRow.EscribirValor(col++, row.TipoComprobante);
            excelRow.EscribirValor(col++, row.RfcEmisor);
            excelRow.EscribirValor(col++, row.NombreEmisor);
            excelRow.EscribirValor(col++, row.RfcReceptor);
            excelRow.EscribirValor(col++, row.NombreReceptor);
            excelRow.EscribirValor(col++, row.Serie);
            excelRow.EscribirValor(col++, row.Folio);
            excelRow.EscribirValor(col++, (double)row.SubTotal, moneyStyle);
            excelRow.EscribirValor(col++, (double)row.Descuento, moneyStyle);
            excelRow.EscribirValor(col++, (double)row.Total, moneyStyle);
            excelRow.EscribirValor(col++, row.Moneda);
            excelRow.EscribirValor(col++, row.FormaPago);
            excelRow.EscribirValor(col++, row.MetodoPago);
            excelRow.EscribirValor(col++, row.UsoCfdi);
            excelRow.EscribirValor(col++, row.Estatus);
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
