using Common.API.Helpers;
using NPOI.SS.UserModel;
using NPOI.SS.UserModel.Charts;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;

namespace Suppliers.BL.Helpers.ExtensionMethods;

public static class ExcelMethods
{
    #region Escritura de celdas
    public static int AgregarEncabezado(this ISheet hoja, string titulo, int filaInicial = 0, int? totalRegistros = null)
    {

        //var fila = hoja.CreateRow(filaInicial);
        //var estilo = EstiloTituloTabla(hoja.Workbook);
        //var celdaIndice = 1;
        //hoja.AddMergedRegion(new CellRangeAddress(filaInicial, filaInicial, celdaIndice, celdaIndice + columnaFinal));
        //var celda = fila.CreateCell(celdaIndice);
        //celdaIndice += 3;
        //celda.SetCellValue(titulo);
        //celda.CellStyle = estilo;

        var estiloNegritas = hoja.Workbook.EstiloNegritaCentrado();
        var estiloFecha = hoja.Workbook.EstiloFormato(FormatoFecha);
        var filaIn = filaInicial;
        var fila = hoja.CreateRow(filaIn);
        var celdaInidce = 1;
        hoja.AddMergedRegion(new CellRangeAddress(filaIn, filaIn, celdaInidce, celdaInidce + 2));
        var celda = fila.CreateCell(celdaInidce);
        celda.SetCellValue(titulo);
        celda.CellStyle = estiloNegritas;
        filaIn++;
        fila = hoja.CreateRow(filaIn++);
        fila.EscribirValor(1, "Fecha de creaci\u00F3n:", estiloNegritas);
        fila.EscribirValor(2, $"{DateTime.Now}", estiloFecha);

        if (totalRegistros != null)
        {
            fila = hoja.CreateRow(filaIn++);
            fila.EscribirValor(1, "Total", estiloNegritas);
            fila.EscribirValor(2, $"{totalRegistros}");
        }
        return filaIn;
    }

    public static void EscribirValor(this IRow fila, int columna, string valor) =>
        fila.CreateCell(columna).SetCellValue(valor);
    public static void EscribirValorEnlace(this IRow fila, int columna, string valor, IWorkbook workbook, string url = "", ICellStyle estilo = null)
    {
        var cell = fila.CreateCell(columna);
        cell.SetCellValue(valor);
        estilo ??= workbook.CreateCellStyle();
        IFont fuente = workbook.CreateFont();
        fuente.Color = IndexedColors.Blue.Index;
        fuente.IsBold = true; // Establecer la fuente en negritas
        estilo.SetFont(fuente);
        cell.CellStyle = estilo;
        ICreationHelper creationHelper = workbook.GetCreationHelper();
        IHyperlink hyperlink = creationHelper.CreateHyperlink(HyperlinkType.Url);
        hyperlink.Address = string.IsNullOrEmpty(url) ? valor : url;
        cell.Hyperlink = hyperlink;
    }

    public static void EscribirValor(this IRow fila, int columna, double valor) =>
        fila.CreateCell(columna).SetCellValue(valor);

    public static void EscribirValor(this IRow fila, int columna, DateTime valor) =>
        fila.CreateCell(columna).SetCellValue(valor);
    public static void EscribirValorEstilo(this IRow fila, int columna, string valor)
    {
        var estilo = fila.Sheet.Workbook.CreateCellStyle();
        var celda = CrearCeldaEstilo(fila, columna, estilo);
        celda.SetCellValue(valor);
    }

    public static void EscribirValorEstilo(this IRow fila, int columna, double valor)
    {
        var estilo = fila.Sheet.Workbook.CreateCellStyle();
        var celda = CrearCeldaEstilo(fila, columna, estilo);
        celda.SetCellValue(valor);
    }

    public static void EscribirValorEstilo(this IRow fila, int columna, DateTime valor)
    {
        var estilo = fila.Sheet.Workbook.CreateCellStyle();
        var celda = CrearCeldaEstilo(fila, columna, estilo);
        celda.SetCellValue(valor);
    }

    public static void EscribirValor(this IRow fila, int columna, string valor, ICellStyle estilo)
    {
        var celda = CrearCeldaEstilo(fila, columna, estilo);
        celda.SetCellValue(valor);
    }

    public static void EscribirValorColorTabla(this IRow fila, int columna, string valor, DateTime valorcolor, ICellStyle estilo)
    {
        var celda = fila.CreateCell(columna);
        celda.SetCellValue(columna);
        celda.CellStyle = estilo;
    }
    public static void EscribirValor(this IRow fila, int columna, double valor, ICellStyle estilo)
    {
        try
        {
            var celda = CrearCeldaEstilo(fila, columna, estilo);
            celda.SetCellValue(valor);
        }
        catch (Exception ex)
        {
            Loggers.MensajeError($"{ex} alv");
        }

    }
    public static void EscribirValor(this IRow fila, int columna, DateTime valor, ICellStyle estilo)
    {
        var celda = CrearCeldaEstilo(fila, columna, estilo);
        celda.SetCellValue(valor);
    }

    public static void EscribirRangoFechas(this ISheet hoja, int fila, string valor, ICellStyle estilo, int columna = 1)
    {
        IRow _rangoFechas = hoja.CreateRow(fila);
        _rangoFechas.EscribirValor(columna++, "Rango de Fechas: ", estilo);
        _rangoFechas.EscribirValor(columna++, $"{valor}", estilo);
    }

    #endregion

    #region Estilos de celda

    public const string FormatoFecha = "dd/mm/yyyy";
    public const string FormatoHora = "H:MM:SS;@";
    public static ICellStyle EstiloNegritas(this IWorkbook libro)
    {
        var estiloCelda = libro.CreateCellStyle();
        var fuente = libro.CreateFont();
        fuente.IsBold = true;
        estiloCelda.SetFont(fuente);
        return estiloCelda;
    }
    public static ICellStyle EstiloNegritaCentrado(this IWorkbook libro)
    {
        var estiloCelda = libro.CreateCellStyle();
        estiloCelda.Alignment = HorizontalAlignment.Center;
        var fuente = libro.CreateFont();
        fuente.IsBold = true;
        estiloCelda.SetFont(fuente);
        return estiloCelda;
    }
    public static ICellStyle EstiloCentrado(this IWorkbook libro)
    {
        var estiloCelda = libro.CreateCellStyle();
        estiloCelda.Alignment = HorizontalAlignment.Center;
        var fuente = libro.CreateFont();
        estiloCelda.SetFont(fuente);
        return estiloCelda;
    }
    public static ICellStyle EstiloTituloTabla(this IWorkbook libro)
    {
        var estiloCelda = libro.CreateCellStyle();
        estiloCelda.Alignment = HorizontalAlignment.Center;
        estiloCelda.VerticalAlignment = VerticalAlignment.Center;
        var fuente = libro.CreateFont();
        fuente.IsBold = true;
        fuente.Color = IndexedColors.White.Index;
        estiloCelda.SetFont(fuente);
        estiloCelda.BorderBottom = BorderStyle.Thin;
        estiloCelda.BorderTop = BorderStyle.Thin;
        estiloCelda.BorderLeft = BorderStyle.Thin;
        estiloCelda.BorderRight = BorderStyle.Thin;
        estiloCelda.FillPattern = FillPattern.SolidForeground;
        estiloCelda.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Red.Index;

        return estiloCelda;
    }
    public static ICellStyle EstiloTituloGris(this IWorkbook libro)
    {
        var estiloCelda = libro.CreateCellStyle();
        estiloCelda.Alignment = HorizontalAlignment.Center;
        var fuente = libro.CreateFont();
        fuente.IsBold = true;
        fuente.Color = IndexedColors.White.Index;
        estiloCelda.SetFont(fuente);
        estiloCelda.BorderBottom = BorderStyle.Thin;
        estiloCelda.BorderTop = BorderStyle.Thin;
        estiloCelda.BorderLeft = BorderStyle.Thin;
        estiloCelda.BorderRight = BorderStyle.Thin;
        estiloCelda.FillPattern = FillPattern.SolidForeground;
        estiloCelda.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Green.Index;

        return estiloCelda;
    }
    public static ICellStyle EstiloCeldaColor(this IWorkbook libro, short indexColor, short indexColorFuente, short? indexColorBorde = null, bool esNegrita = false, bool centrado = false)
    {
        var estiloCelda = libro.CreateCellStyle();
        var fuente = libro.CreateFont();
        fuente.Color = indexColorFuente;
        fuente.IsBold = esNegrita;
        estiloCelda.SetFont(fuente);
        estiloCelda.Alignment = centrado ? HorizontalAlignment.Center : HorizontalAlignment.Justify;
        estiloCelda.FillPattern = FillPattern.SolidForeground;
        estiloCelda.FillForegroundColor = indexColor;
        if (indexColorBorde != null)
        {
            estiloCelda.BorderBottom = BorderStyle.Thin;
            estiloCelda.BorderTop = BorderStyle.Thin;
            estiloCelda.BorderLeft = BorderStyle.Thin;
            estiloCelda.BorderRight = BorderStyle.Thin;
            estiloCelda.BottomBorderColor = indexColorBorde ?? 0;
            estiloCelda.TopBorderColor = indexColorBorde ?? 0;
            estiloCelda.LeftBorderColor = indexColorBorde ?? 0;
            estiloCelda.RightBorderColor = indexColorBorde ?? 0;
        }

        return estiloCelda;
    }

    public static ICellStyle EstiloFormato(this IWorkbook libro, string formato)
    {
        var estiloCelda = libro.CreateCellStyle();
        estiloCelda.DataFormat = libro.CreateDataFormat().GetFormat(formato);
        return estiloCelda;
    }

    public static ICellStyle EstiloFormatoNegrita(this IWorkbook libro, string formato)
    {
        var estiloCelda = libro.CreateCellStyle();
        var fuente = libro.CreateFont();
        fuente.IsBold = true;
        estiloCelda.SetFont(fuente);
        estiloCelda.DataFormat = libro.CreateDataFormat().GetFormat(formato);
        return estiloCelda;
    }

    public static ICellStyle EstiloFormatoCorteCaja(this IWorkbook libro, string formato)
    {
        var estiloCelda = libro.CreateCellStyle();
        var fuente = libro.CreateFont();
        fuente.IsBold = true;
        fuente.Color = IndexedColors.White.Index;
        estiloCelda.Alignment = HorizontalAlignment.Center;
        estiloCelda.SetFont(fuente);
        estiloCelda.BorderBottom = BorderStyle.Thin;
        estiloCelda.BorderTop = BorderStyle.Thin;
        estiloCelda.BorderLeft = BorderStyle.Thin;
        estiloCelda.BorderRight = BorderStyle.Thin;
        estiloCelda.FillPattern = FillPattern.SolidForeground;
        estiloCelda.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Green.Index;
        estiloCelda.DataFormat = libro.CreateDataFormat().GetFormat(formato);
        return estiloCelda;
    }

    public static ICellStyle EstiloTramitesVentanilla(this IWorkbook libro, string formato)
    {
        var estiloCelda = libro.CreateCellStyle();
        var fuente = libro.CreateFont();
        fuente.IsBold = true;
        fuente.Color = IndexedColors.White.Index;
        estiloCelda.Alignment = HorizontalAlignment.Center;
        estiloCelda.SetFont(fuente);
        estiloCelda.BorderBottom = BorderStyle.Thin;
        estiloCelda.BorderTop = BorderStyle.Thin;
        estiloCelda.BorderLeft = BorderStyle.Thin;
        estiloCelda.BorderRight = BorderStyle.Thin;
        estiloCelda.FillPattern = FillPattern.SolidForeground;
        estiloCelda.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.DarkRed.Index;
        estiloCelda.DataFormat = libro.CreateDataFormat().GetFormat(formato);
        return estiloCelda;
    }

    #endregion

    #region Metodos de ayuda

    public static ICell CrearCeldaEstilo(IRow fila, int columna, ICellStyle estilo)
    {
        var celda = fila.CreateCell(columna);
        celda.CellStyle = estilo;
        return celda;
    }

    public static void AgregarTituloTabla(this ISheet hoja, IReadOnlyList<string> titulos, int columnaInicial, int filaInicial)
    {
        var fila = hoja.CreateRow(filaInicial);
        var estilo = hoja.Workbook.EstiloTituloTabla();
        for (var columna = 0; columna < titulos.Count; columna++)
        {
            var celda = fila.CreateCell(columna + columnaInicial);
            celda.SetCellValue(titulos[columna]);
            celda.CellStyle = estilo;
        }
    }
    public static IRow AgregarTituloTablaColor(this IRow fila, IReadOnlyList<string> titulos, ICellStyle estilo, int columnaInicial = 0, int filaInicial = 0)
    {
        for (var columna = 0; columna < titulos.Count; columna++)
        {
            var celda = fila.CreateCell(columnaInicial++);
            celda.SetCellValue(titulos[columna]);
            celda.CellStyle = estilo;
        }
        return fila;
    }
    public static void AgregarTituloTablaGris(this ISheet hoja, IReadOnlyList<string> titulos, int columnaInicial, int filaInicial)
    {
        var fila = hoja.CreateRow(filaInicial);
        var estilo = hoja.Workbook.EstiloTituloTablaGris();
        for (var columna = 0; columna < titulos.Count; columna++)
        {
            var celda = fila.CreateCell(columna);
            celda.SetCellValue(titulos[columna + columnaInicial]);
            celda.CellStyle = estilo;
        }
    }
    public static ICell CeldaSumatoria(this IRow fila, int columna, CellRangeAddress rango, string formato, ICellStyle estilo)
    {
        estilo.DataFormat = fila.Sheet.Workbook.CreateDataFormat().GetFormat(formato);
        var celda = fila.CreateCell(columna);
        celda.SetCellType(CellType.Formula);
        celda.CellFormula = $"SUM({rango.FormatAsString()})";
        celda.SetCellType(CellType.Formula);
        celda.CellStyle = estilo;
        return celda;
    }
    public static ICell CeldaSumatoria(this IRow fila, int columna, CellRangeAddress rango, string formato)
    {
        var estilo = fila.Sheet.Workbook.EstiloFormato(formato);
        var fuente = fila.Sheet.Workbook.CreateFont();
        fuente.IsBold = true;
        estilo.SetFont(fuente);
        var celda = fila.CreateCell(columna);
        celda.SetCellType(CellType.Formula);
        celda.CellFormula = $"SUM({rango.FormatAsString()})";
        celda.CellStyle = estilo;
        return celda;
    }
    public static string FormatoMoneda(string simbolo) => $"_-\"{simbolo}\"* #,##0.00_-;-\"{simbolo}\" * #,##0.00_-;_-@_-";
    public static string FormatoPorcentaje(string simbolo) => $"_-#,##0.00 \"{simbolo}\"_-;-#,##0.00 \"{simbolo}\"_-;_-@_-";

    public static void CambiarColorCelda(this IRow fila, short color)
    {
        foreach (var celda in fila.Cells)
        {
            var estilo = celda.CellStyle;
            estilo.FillPattern = FillPattern.SolidForeground;
            estilo.FillForegroundColor = color;
            celda.CellStyle = estilo;
        }
    }

    #endregion
    public static ICell CeldaSumatoriaPoliza(this IRow fila, int columna, CellRangeAddress rango, string formato)
    {
        var estilo = fila.Sheet.Workbook.EstiloFormato(formato);
        var fuente = fila.Sheet.Workbook.CreateFont();
        fuente.IsBold = true;
        estilo.SetFont(fuente);
        var celda = fila.CreateCell(columna);
        celda.SetCellType(CellType.Formula);
        celda.CellFormula = $"SUM({rango.FormatAsString()})";
        celda.CellStyle = estilo;
        return celda;
    }
    public static string FormatoMonedaPoliza(string simbolo) => $"_-\"{simbolo}\"* #,##0.00_-;-\"{simbolo}\" * #,##0.00_-;_-@_-";
    public static string FormatoMonedaExcel(string simbolo) => $"_-\"{simbolo}\"* #,##0.00_-;-\"{simbolo}\" * #,##0.00_-;_-\"{simbolo}\" * \" - \" ?? _-;_-@_-";
    public static string FormatoPorcentajePoliza(string simbolo) => $"_-#,##0.00 \"{simbolo}\"_-;-#,##0.00 \"{simbolo}\"_-;_-@_-";

    public static void CambiarColorCeldaPoliza(this IRow fila, short color)
    {
        foreach (var celda in fila.Cells)
        {
            var estilo = celda.CellStyle;
            estilo.FillPattern = FillPattern.SolidForeground;
            estilo.FillForegroundColor = color;
            celda.CellStyle = estilo;
        }
    }

    public static void AgregarTituloTablaCreacionExcel(this ISheet hoja, IReadOnlyList<string> titulos, int columnaInicial, int filaInicial)
    {
        var fila = hoja.CreateRow(filaInicial);
        var estilo = hoja.Workbook.EstiloTituloTabla();
        for (var columna = 0; columna < titulos.Count; columna++)
        {
            var celda = fila.CreateCell(columna + columnaInicial);
            celda.SetCellValue(titulos[columna]);
            celda.CellStyle = estilo;
        }
    }

    public static void AgregarTitulo(this ISheet hoja, string titulo, int columnaInicial, int filaInicial, int columnaFinal = 2)
    {
        var fila = hoja.CreateRow(filaInicial);
        var estilo = hoja.Workbook.EstiloTituloTabla();
        var celdaIndice = 1;
        hoja.AddMergedRegion(new CellRangeAddress(filaInicial, filaInicial, celdaIndice, celdaIndice + columnaFinal));
        var celda = fila.CreateCell(celdaIndice);
        celdaIndice += 3;
        celda.SetCellValue(titulo);
        celda.CellStyle = estilo;
    }

    public static void AgregarTituloTablaRepUtilidades(this ISheet hoja, IReadOnlyList<string> titulos, int columnaInicial, int filaInicial, int v)
    {
        var fila = hoja.CreateRow(filaInicial);
        var estilo = hoja.Workbook.EstiloTituloTabla();
        for (var columna = 0; columna < titulos.Count; columna++)
        {
            var celda = fila.CreateCell(columna);
            celda.SetCellValue(titulos[columna + columnaInicial]);
            celda.CellStyle = estilo;
        }
    }
    public static string FormatoMonedaRepUtilidades(string simbolo) => $"_-\"{simbolo}\"* #,##0.00_-;-\"{simbolo}\" * #,##0.00_-;_-@_-";
    public static string FormatoPorcentajeRepUtilidades(string simbolo) => $"_-#,##0.00 \"{simbolo}\"_-;-#,##0.00 \"{simbolo}\"_-;_-@_-";

    public static string FormatoMonedaRepUnidades(string simbolo) => $"_-\"{simbolo}\"* #,##0.00_-;-\"{simbolo}\" * #,##0.00_-;_-@_-";
    public static string FormatoPorcentajeRepUnidades(string simbolo) => $"_-#,##0.00 \"{simbolo}\"_-;-#,##0.00 \"{simbolo}\"_-;_-@_-";
    public static void CambiarColorCeldaCreacionExcel(this IRow fila, short color)
    {
        foreach (var celda in fila.Cells)
        {
            var estilo = celda.CellStyle;
            estilo.FillPattern = FillPattern.SolidForeground;
            estilo.FillForegroundColor = color;
            celda.CellStyle = estilo;
        }
    }
    public static void InsertarCeldaTitulo(this ISheet hoja, string valor, int filaIndex, int colInicio, int colFin, ICellStyle estilo = null, IRow fila = null)
    {
        fila ??= hoja.CreateRow(filaIndex);
        hoja.AddMergedRegion(new CellRangeAddress(filaIndex, filaIndex, colInicio, colFin));
        fila.EscribirValor(colInicio, valor, estilo);
    }

    public static void Create2DLineChart(this ISheet hoja, IDrawing drawing, IClientAnchor anchor, string serie1,
        int x, int y, int xtam1, int xtam2, int ytam1, int ytam2)
    {
        var firstRow = hoja.GetRow(x);
        IChart chart = drawing.CreateChart(anchor);
        IChartLegend legend = chart.GetOrCreateLegend();
        legend.Position = LegendPosition.TopRight;

        ILineChartData<double, double> data = chart.ChartDataFactory.CreateLineChartData<double, double>();

        // Use a category axis for the bottom axis.
        IChartAxis bottomAxis = chart.ChartAxisFactory.CreateCategoryAxis(AxisPosition.Bottom);
        IValueAxis leftAxis = chart.ChartAxisFactory.CreateValueAxis(AxisPosition.Left);
        leftAxis.Crosses = AxisCrosses.AutoZero;

        IChartDataSource<double> xs = DataSources.FromNumericCellRange(hoja, new CellRangeAddress(x, y, xtam1, ytam1));

        for (var colIndex = xtam2; colIndex <= ytam2; colIndex++)
        {
            IChartDataSource<double> ys1 = DataSources.FromNumericCellRange(hoja, new CellRangeAddress(x, y, colIndex, colIndex));
            var series1 = data.AddSeries(xs, ys1);
            series1.SetTitle($"{firstRow.Cells[colIndex].StringCellValue}");
        }

        bottomAxis.MajorTickMark = AxisTickMark.Cross;
        bottomAxis.MinorTickMark = AxisTickMark.In;
        chart.Plot(data, bottomAxis, leftAxis);

        XSSFChart xssfChart = (XSSFChart)chart;

        var plotArea = xssfChart.GetCTChart().plotArea;
        var c = new NPOI.OpenXmlFormats.Dml.Chart.CT_Boolean
        {
            val = 0
        };
        plotArea.lineChart[0].smooth = c;

        foreach (var ser in plotArea.lineChart[0].ser)
        {
            ser.smooth = c;
        }

        plotArea.valAx[0].AddNewMajorGridlines();

    }

    public static void AgregarTituloEncabezadoGris(this ISheet hoja, IReadOnlyList<string> titulos, int columnaInicial, int filaInicial)
    {
        var fila = hoja.CreateRow(filaInicial);
        var estilo = hoja.Workbook.EstiloTituloTablaGris();
        for (var columna = 0; columna < titulos.Count; columna++)
        {
            var celda = fila.CreateCell(columna + columnaInicial);
            celda.SetCellValue(titulos[columna]);
            celda.CellStyle = estilo;
        }
    }
    public static void AgregarFirma(this IRow fila, IReadOnlyList<string> titulos, int columnaInicial)
    {
        var estilo = fila.Sheet.Workbook.LineaFirma();
        for (var columna = 0; columna < titulos.Count; columna++)
        {
            var celda = fila.CreateCell(columna + columnaInicial);
            celda.SetCellValue(titulos[columna]);
            celda.CellStyle = estilo;
        }
    }
    public static ICellStyle LineaFirma(this IWorkbook libro)
    {
        var estiloCelda = libro.CreateCellStyle();
        estiloCelda.Alignment = HorizontalAlignment.Center;
        var fuente = libro.CreateFont();
        estiloCelda.SetFont(fuente);
        estiloCelda.BorderTop = BorderStyle.Thin;

        return estiloCelda;
    }
    public static ICellStyle EstiloTituloTablaGris(this IWorkbook libro)
    {
        var estiloCelda = libro.CreateCellStyle();
        estiloCelda.Alignment = HorizontalAlignment.Center;
        var fuente = libro.CreateFont();
        fuente.IsBold = true;
        fuente.Color = IndexedColors.White.Index;
        estiloCelda.SetFont(fuente);
        estiloCelda.BorderBottom = BorderStyle.Thin;
        estiloCelda.BorderTop = BorderStyle.Thin;
        estiloCelda.BorderLeft = BorderStyle.Thin;
        estiloCelda.BorderRight = BorderStyle.Thin;
        estiloCelda.FillPattern = FillPattern.SolidForeground;
        estiloCelda.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Grey25Percent.Index;

        return estiloCelda;
    }

}
