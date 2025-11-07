using Common.API.Bl;
using Common.API.Helpers;
using Common.API.Helpers.Utils;
using Microsoft.AspNetCore.Http;
using Suppliers.BL.Helpers;
using Suppliers.BL.Helpers.Constants;
using Suppliers.BL.Interfaces;
using Suppliers.BL.Reports.Example;
using Suppliers.BL.Reports.Contpaq;

namespace Suppliers.BL.Bl;

public class ReportBl(DescargaCfdiGfpContext context, IHttpContextAccessor httpContext) : CommonBl<InfoToken, DescargaCfdiGfpContext, uint>(context, httpContext), IReportBl
{
    public async Task<ApiResponse<string>> ExampleExcel(ExampleFilterReport filter, string customLogo = "", string customPath = "")
    {
        var response = new ApiResponse<string>();
        try
        {
            var report = new ExampleReport(filter, _context, "path_logo", ConfigurationKey.ReportPath, customLogo, customPath);
            await report.Init();
            var path = report.GenerateExcel($"AccountStatements_{DateTime.Now.Ticks}.xlsx");
            response.SetComplete(path);
        }
        catch (Exception ex)
        {
            Loggers.Error(GetType(), _infoToken.UserId, nameof(ExampleExcel), $"{ex}");
        }
        return response;
    }

    public async Task<ApiResponse<string>> ContpaqExcel(ContpaqFilterReport filter, string customLogo = "", string customPath = "")
    {
        var response = new ApiResponse<string>();
        try
        {
            var report = new ContpaqReport(filter, _context, "path_logo", ConfigurationKey.ReportPath, customLogo, customPath);
            await report.Init();
            var path = report.GenerateExcel($"CONTPAQ_{DateTime.Now.Ticks}.xlsx");
            response.SetComplete(path);
        }
        catch (Exception ex)
        {
            Loggers.Error(GetType(), _infoToken.UserId, nameof(ContpaqExcel), $"{ex}");
        }
        return response;
    }
}
