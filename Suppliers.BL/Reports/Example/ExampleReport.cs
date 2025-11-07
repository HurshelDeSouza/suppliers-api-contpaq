namespace Suppliers.BL.Reports.Example;

public class ExampleReport : CommonReportConfig<ExampleData, ExampleFilterReport>
{
    public ExampleReport(ExampleFilterReport filter, DescargaCfdiGfpContext context) : base(filter, context)
    {
    }

    public ExampleReport(ExampleFilterReport filter, DescargaCfdiGfpContext context, string keyLogo, string keyPath, string customLogo = "", string customPath = "") : base(filter, context, keyLogo, keyPath, customLogo, customPath)
    {
    }

    protected override async Task SetData()
    {
        Data = new ExampleData
        {
            // Populate with actual data as needed
        };
    }

    public override string GenerateExcel(string fileName)
    {
        var fullPath = Path.Combine(FilePath, fileName);
        ExampleExcel.Generate(Data, fullPath);
        return fullPath;
    }

    public override string GeneratePdf(string fileName)
    {
        var fullPath = Path.Combine(FilePath, fileName);
        ExamplePdf.Generate(Data, fullPath);
        return fullPath;
    }
}
