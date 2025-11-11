using Common.API.Helpers.Utils;
using Suppliers.BL.Entities.DTOs;

namespace Suppliers.BL.Interfaces;

public interface ICFDINominaBl
{
    Task<ApiResponse<CFDINominaDTO>> GetById(int idComprobante);
    Task<ApiResponse<List<CFDINominaDTO>>> GetAll(int page = 1, int pageSize = 10);
    Task<ApiResponse<List<CFDINominaDTO>>> GetByFilters(string? uuid = null, string? rfcEmisor = null, 
        string? rfcReceptor = null, DateTime? fechaInicio = null, DateTime? fechaFin = null, 
        int page = 1, int pageSize = 10);
    Task<ApiResponse<CFDINominaDTO>> Create(CFDINominaDTO nominaDto);
    Task<ApiResponse<CFDINominaDTO>> Update(int idComprobante, CFDINominaDTO nominaDto);
    Task<ApiResponse<bool>> Delete(int idComprobante);
}
