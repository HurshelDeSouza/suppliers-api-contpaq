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

    public async Task<ApiResponse<string>> RecibidasContpaqExcel(ContpaqFilterReport filter, string customLogo = "", string customPath = "")
    {
        var response = new ApiResponse<string>();
        try
        {
            var report = new RecibidasContpaqReport(filter, _context, "path_logo", ConfigurationKey.ReportPath, customLogo, customPath);
            await report.Init();
            var path = report.GenerateExcel($"Recibidas_CONTPAQ_{DateTime.Now.Ticks}.xlsx");
            response.SetComplete(path);
        }
        catch (Exception ex)
        {
            Loggers.Error(GetType(), _infoToken.UserId, nameof(RecibidasContpaqExcel), $"{ex}");
        }
        return response;
    }

    public async Task<ApiResponse<string>> EmitidasContpaqExcel(ContpaqFilterReport filter, string customLogo = "", string customPath = "")
    {
        var response = new ApiResponse<string>();
        try
        {
            var report = new EmitidasContpaqReport(filter, _context, "path_logo", ConfigurationKey.ReportPath, customLogo, customPath);
            await report.Init();
            var path = report.GenerateExcel($"Emitidas_CONTPAQ_{DateTime.Now.Ticks}.xlsx");
            response.SetComplete(path);
        }
        catch (Exception ex)
        {
            Loggers.Error(GetType(), _infoToken.UserId, nameof(EmitidasContpaqExcel), $"{ex}");
        }
        return response;
    }

    public async Task<ApiResponse<string>> BovedaEmittedExcel(ContpaqFilterReport filter, string customLogo = "", string customPath = "")
    {
        var response = new ApiResponse<string>();
        try
        {
            var report = new BovedaEmittedReport(filter, _context, "path_logo", ConfigurationKey.ReportPath, customLogo, customPath);
            await report.Init();
            var path = report.GenerateExcel($"Boveda_Emitted_{DateTime.Now.Ticks}.xlsx");
            response.SetComplete(path);
        }
        catch (Exception ex)
        {
            Loggers.Error(GetType(), _infoToken.UserId, nameof(BovedaEmittedExcel), $"{ex}");
        }
        return response;
    }

    public async Task<ApiResponse<string>> BovedaReceivedExcel(ContpaqFilterReport filter, string customLogo = "", string customPath = "")
    {
        var response = new ApiResponse<string>();
        try
        {
            var report = new BovedaReceivedReport(filter, _context, "path_logo", ConfigurationKey.ReportPath, customLogo, customPath);
            await report.Init();
            var path = report.GenerateExcel($"Boveda_Received_{DateTime.Now.Ticks}.xlsx");
            response.SetComplete(path);
        }
        catch (Exception ex)
        {
            Loggers.Error(GetType(), _infoToken.UserId, nameof(BovedaReceivedExcel), $"{ex}");
        }
        return response;
    }
}
