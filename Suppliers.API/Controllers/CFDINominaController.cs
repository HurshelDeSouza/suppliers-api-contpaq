using Common.API.Authorization;
using Common.API.Helpers.Utils;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Suppliers.BL.Entities.DTOs;
using Suppliers.BL.Interfaces;

namespace Suppliers.API.Controllers;

[ApiController]
[EnableCors("AnotherPolicy")]
[Route("[controller]")]
[Authorize<uint>]
public class CFDINominaController : ControllerBase
{
    private readonly ICFDINominaBl _bl;

    public CFDINominaController(ICFDINominaBl bl)
    {
        _bl = bl;
    }

    /// <summary>
    /// Obtiene un CFDI de Nómina por ID
    /// </summary>
    /// <param name="id">ID del comprobante</param>
    /// <returns>CFDI de Nómina</returns>
    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<ActionResult<ApiResponse<CFDINominaDTO>>> GetById(int id)
    {
        var response = await _bl.GetById(id);
        return StatusCode(response.StatusCode, response);
    }

    /// <summary>
    /// Obtiene todos los CFDIs de Nómina con paginación
    /// </summary>
    /// <param name="page">Número de página</param>
    /// <param name="pageSize">Tamaño de página</param>
    /// <returns>Lista de CFDIs de Nómina</returns>
    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<ApiResponse<List<CFDINominaDTO>>>> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var response = await _bl.GetAll(page, pageSize);
        return StatusCode(response.StatusCode, response);
    }

    /// <summary>
    /// Busca CFDIs de Nómina por filtros
    /// </summary>
    /// <param name="uuid">UUID del comprobante</param>
    /// <param name="rfcEmisor">RFC del emisor</param>
    /// <param name="rfcReceptor">RFC del receptor</param>
    /// <param name="fechaInicio">Fecha de inicio</param>
    /// <param name="fechaFin">Fecha de fin</param>
    /// <param name="page">Número de página</param>
    /// <param name="pageSize">Tamaño de página</param>
    /// <returns>Lista de CFDIs de Nómina filtrados</returns>
    [HttpGet("search")]
    [AllowAnonymous]
    public async Task<ActionResult<ApiResponse<List<CFDINominaDTO>>>> Search(
        [FromQuery] string? uuid = null,
        [FromQuery] string? rfcEmisor = null,
        [FromQuery] string? rfcReceptor = null,
        [FromQuery] DateTime? fechaInicio = null,
        [FromQuery] DateTime? fechaFin = null,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
    {
        var response = await _bl.GetByFilters(uuid, rfcEmisor, rfcReceptor, fechaInicio, fechaFin, page, pageSize);
        return StatusCode(response.StatusCode, response);
    }

    /// <summary>
    /// Crea un nuevo CFDI de Nómina
    /// </summary>
    /// <param name="nominaDto">Datos del CFDI de Nómina</param>
    /// <returns>CFDI de Nómina creado</returns>
    [HttpPost]
    [AllowAnonymous]
    public async Task<ActionResult<ApiResponse<CFDINominaDTO>>> Create([FromBody] CFDINominaDTO nominaDto)
    {
        var response = await _bl.Create(nominaDto);
        return StatusCode(response.StatusCode, response);
    }

    /// <summary>
    /// Actualiza un CFDI de Nómina existente
    /// </summary>
    /// <param name="id">ID del comprobante</param>
    /// <param name="nominaDto">Datos actualizados del CFDI de Nómina</param>
    /// <returns>CFDI de Nómina actualizado</returns>
    [HttpPut("{id}")]
    [AllowAnonymous]
    public async Task<ActionResult<ApiResponse<CFDINominaDTO>>> Update(int id, [FromBody] CFDINominaDTO nominaDto)
    {
        var response = await _bl.Update(id, nominaDto);
        return StatusCode(response.StatusCode, response);
    }

    /// <summary>
    /// Elimina un CFDI de Nómina
    /// </summary>
    /// <param name="id">ID del comprobante</param>
    /// <returns>Resultado de la operación</returns>
    [HttpDelete("{id}")]
    [AllowAnonymous]
    public async Task<ActionResult<ApiResponse<bool>>> Delete(int id)
    {
        var response = await _bl.Delete(id);
        return StatusCode(response.StatusCode, response);
    }
}
