using Common.API.Helpers.Utils;
using Suppliers.BL.Reports.Example;
using Suppliers.BL.Reports.Contpaq;

namespace Suppliers.BL.Interfaces;

public interface IReportBl
{
    public Task<ApiResponse<string>> ExampleExcel(ExampleFilterReport filter, string customLogo = "", string customPath = "");
    public Task<ApiResponse<string>> ContpaqExcel(ContpaqFilterReport filter, string customLogo = "", string customPath = "");
    public Task<ApiResponse<string>> RecibidasContpaqExcel(ContpaqFilterReport filter, string customLogo = "", string customPath = "");
    public Task<ApiResponse<string>> EmitidasContpaqExcel(ContpaqFilterReport filter, string customLogo = "", string customPath = "");
    public Task<ApiResponse<string>> BovedaEmittedExcel(ContpaqFilterReport filter, string customLogo = "", string customPath = "");
    public Task<ApiResponse<string>> BovedaReceivedExcel(ContpaqFilterReport filter, string customLogo = "", string customPath = "");
}
