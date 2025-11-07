using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using Suppliers.BL.Helpers.ExtensionMethods;

namespace Suppliers.BL.Reports.Example;

public class ExampleExcel
{
    public static async void Generate(ExampleData reportData, string fullPath)
    {
        if (reportData == null)
        {
            return;
        }

        IWorkbook book = new XSSFWorkbook();

        var sheet = book.CreateSheet("Example");
        var boldStyle = book.EstiloNegritas();

        var indexRow = 1;

        indexRow = sheet.AgregarEncabezado("Reporte de de ejemplo");

        string[] headers = { "Encabezado 1*", "Encabezado 2" };

        var rowTable = indexRow;
        sheet.AgregarTituloTabla(headers, 1, indexRow++);
        IRow row;
        row = sheet.CreateRow(indexRow++);
        row.EscribirValor(1, "1");
        row.EscribirValor(2, 2);

        sheet.SetAutoFilter(new NPOI.SS.Util.CellRangeAddress(rowTable, indexRow, 1, headers.Length));

        await using var fs = new FileStream(fullPath, FileMode.Create, FileAccess.Write);
        book.Write(fs, false);
        fs.Close();
    }
}
