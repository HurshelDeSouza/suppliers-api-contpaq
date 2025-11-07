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

        // Definir headers según CONTPAQ.xlsx
        string[] headers = { 
            "Tarjeta Tipo", 
            "Comprobante Comprobante", 
            "Serie", 
            "Folio", 
            "RFC Emisor",
            "Nombre Emisor",
            "Marca",
            "Tipo Cambio",
            "Total",
            "Responsable",
            "Proveedor",
            "Referencia",
            "Observaciones",
            "Abonado Contabilidad",
            "Abonado Bancos",
            "Abonado Comercial",
            "Estatus",
            "Listo",
            "Estado Pagado",
            "Validez",
            "Forma",
            "Método",
            "Listado de Cancelación del Documento",
            "Fecha de Cancelación del Documento"
        };

        var rowTable = indexRow;
        sheet.AgregarTituloTabla(headers, 0, indexRow++);

        // Agregar datos
        foreach (var row in reportData.Rows)
        {
            var excelRow = sheet.CreateRow(indexRow++);
            var col = 0;
            
            excelRow.EscribirValor(col++, row.TarjetaTipo);
            excelRow.EscribirValor(col++, row.ComprobanteComprobante);
            excelRow.EscribirValor(col++, row.Serie);
            excelRow.EscribirValor(col++, row.Folio);
            excelRow.EscribirValor(col++, row.RfcEmisor);
            excelRow.EscribirValor(col++, row.NombreEmisor);
            excelRow.EscribirValor(col++, row.Marca);
            excelRow.EscribirValor(col++, row.TipoCambio);
            excelRow.EscribirValor(col++, (double)row.Total, moneyStyle);
            excelRow.EscribirValor(col++, row.Responsable);
            excelRow.EscribirValor(col++, row.Proveedor);
            excelRow.EscribirValor(col++, row.Referencia);
            excelRow.EscribirValor(col++, row.Observaciones);
            excelRow.EscribirValor(col++, row.AbonadoContabilidad);
            excelRow.EscribirValor(col++, row.AbonadoBancos);
            excelRow.EscribirValor(col++, row.AbonadoComercial);
            excelRow.EscribirValor(col++, row.Estatus);
            excelRow.EscribirValor(col++, row.Listo);
            excelRow.EscribirValor(col++, row.EstadoPagado);
            excelRow.EscribirValor(col++, row.Validez);
            excelRow.EscribirValor(col++, row.Forma);
            excelRow.EscribirValor(col++, row.Metodo);
            excelRow.EscribirValor(col++, row.ListadoCancelacionDocumento);
            excelRow.EscribirValor(col++, row.FechaCancelacionDocumento);
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
