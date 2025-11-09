using Common.API.Authorization;
using Common.API.Helpers.Utils;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Suppliers.BL.Interfaces;
using Suppliers.BL.Reports.Example;
using Suppliers.BL.Reports.Contpaq;

namespace Suppliers.API.Controllers;

[ApiController]
[EnableCors("AnotherPolicy")]
[Route("[controller]")]
[Authorize<uint>]
public class ReportController(IReportBl bl) : ControllerBase
{
    private readonly IReportBl _bl = bl;

    private ActionResult GetFile(string path)
    {
        if (System.IO.File.Exists(path))
        {
            return File(System.IO.File.ReadAllBytes(path), "application/octet-stream");
        }
        return StatusCode(206, "File Not Found");
    }

    private ActionResult ReportResponse(ApiResponse<string> response, bool download)
    {
        if (response.Success)
        {
            return download ? GetFile(response.Object) : Ok(response.Object);
        }
        else
        {
            return StatusCode(response.StatusCode, response.Message);
        }
    }

    [AllowAnonymous]
    [HttpPost("ExampleExcel")]
    public async Task<ActionResult> ExampleExcel(ExampleFilterReport filter)
    {
        var response = await _bl.ExampleExcel(filter, customLogo: filter.CustomLogo, customPath: filter.CustomPath);
        return ReportResponse(response, filter.Download);
    }

    [AllowAnonymous]
    [HttpPost("ContpaqExcel")]
    public async Task<ActionResult> ContpaqExcel(ContpaqFilterReport filter)
    {
        var response = await _bl.ContpaqExcel(filter, customLogo: filter.CustomLogo, customPath: filter.CustomPath);
        return ReportResponse(response, filter.Download);
    }

    [AllowAnonymous]
    [HttpPost("RecibidasContpaqExcel")]
    public async Task<ActionResult> RecibidasContpaqExcel(ContpaqFilterReport filter)
    {
        var response = await _bl.RecibidasContpaqExcel(filter, customLogo: filter.CustomLogo, customPath: filter.CustomPath);
        return ReportResponse(response, filter.Download);
    }

    [AllowAnonymous]
    [HttpPost("EmitidasContpaqExcel")]
    public async Task<ActionResult> EmitidasContpaqExcel(ContpaqFilterReport filter)
    {
        var response = await _bl.EmitidasContpaqExcel(filter, customLogo: filter.CustomLogo, customPath: filter.CustomPath);
        return ReportResponse(response, filter.Download);
    }

}
