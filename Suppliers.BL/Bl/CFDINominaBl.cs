using Common.API.Bl;
using Common.API.Helpers;
using Common.API.Helpers.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Suppliers.BL.Entities;
using Suppliers.BL.Entities.DTOs;
using Suppliers.BL.Helpers;
using Suppliers.BL.Interfaces;

namespace Suppliers.BL.Bl;

public class CFDINominaBl : CommonBl<InfoToken, DescargaCfdiGfpContext, uint>, ICFDINominaBl
{
    public CFDINominaBl(DescargaCfdiGfpContext context, IHttpContextAccessor httpContext) 
        : base(context, httpContext)
    {
    }

    public async Task<ApiResponse<CFDINominaDTO>> GetById(int idComprobante)
    {
        var response = new ApiResponse<CFDINominaDTO>();
        try
        {
            var comprobante = await _context.CfdiComprobantes
                .Include(c => c.CfdiEmisor)
                .Include(c => c.CfdiReceptor)
                .Include(c => c.NominaDetalle)
                .Include(c => c.NominaPercepciones)
                .Include(c => c.NominaDeducciones)
                .Include(c => c.NominaOtrosPagos)
                .FirstOrDefaultAsync(c => c.IdComprobante == idComprobante && c.TipoDeComprobante == "N");

            if (comprobante == null)
            {
                response.SetError("No se encontró el CFDI de Nómina", 404);
                return response;
            }

            var dto = MapToDTO(comprobante);
            response.SetComplete(dto);
        }
        catch (Exception ex)
        {
            Loggers.Error(GetType(), _infoToken.UserId, nameof(GetById), $"{ex}");
            response.SetError("Error al obtener el CFDI de Nómina");
        }
        return response;
    }

    public async Task<ApiResponse<List<CFDINominaDTO>>> GetAll(int page = 1, int pageSize = 10)
    {
        var response = new ApiResponse<List<CFDINominaDTO>>();
        try
        {
            var query = _context.CfdiComprobantes
                .Include(c => c.CfdiEmisor)
                .Include(c => c.CfdiReceptor)
                .Include(c => c.NominaDetalle)
                .Include(c => c.NominaPercepciones)
                .Include(c => c.NominaDeducciones)
                .Include(c => c.NominaOtrosPagos)
                .Where(c => c.TipoDeComprobante == "N")
                .OrderByDescending(c => c.Fecha);

            var comprobantes = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var dtos = comprobantes.Select(MapToDTO).ToList();
            response.SetComplete(dtos);
        }
        catch (Exception ex)
        {
            Loggers.Error(GetType(), _infoToken.UserId, nameof(GetAll), $"{ex}");
            response.SetError("Error al obtener los CFDIs de Nómina");
        }
        return response;
    }

    public async Task<ApiResponse<List<CFDINominaDTO>>> GetByFilters(string? uuid = null, string? rfcEmisor = null,
        string? rfcReceptor = null, DateTime? fechaInicio = null, DateTime? fechaFin = null,
        int page = 1, int pageSize = 10)
    {
        var response = new ApiResponse<List<CFDINominaDTO>>();
        try
        {
            var query = _context.CfdiComprobantes
                .Include(c => c.CfdiEmisor)
                .Include(c => c.CfdiReceptor)
                .Include(c => c.NominaDetalle)
                .Include(c => c.NominaPercepciones)
                .Include(c => c.NominaDeducciones)
                .Include(c => c.NominaOtrosPagos)
                .Where(c => c.TipoDeComprobante == "N");

            if (!string.IsNullOrEmpty(uuid))
                query = query.Where(c => c.Uuid.Contains(uuid));

            if (!string.IsNullOrEmpty(rfcEmisor))
                query = query.Where(c => c.CfdiEmisor.Rfc.Contains(rfcEmisor));

            if (!string.IsNullOrEmpty(rfcReceptor))
                query = query.Where(c => c.CfdiReceptor.Rfc.Contains(rfcReceptor));

            if (fechaInicio.HasValue)
                query = query.Where(c => c.Fecha >= fechaInicio.Value);

            if (fechaFin.HasValue)
                query = query.Where(c => c.Fecha <= fechaFin.Value);

            query = query.OrderByDescending(c => c.Fecha);

            var comprobantes = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var dtos = comprobantes.Select(MapToDTO).ToList();
            response.SetComplete(dtos);
        }
        catch (Exception ex)
        {
            Loggers.Error(GetType(), _infoToken.UserId, nameof(GetByFilters), $"{ex}");
            response.SetError("Error al filtrar los CFDIs de Nómina");
        }
        return response;
    }

    public async Task<ApiResponse<CFDINominaDTO>> Create(CFDINominaDTO nominaDto)
    {
        var response = new ApiResponse<CFDINominaDTO>();
        try
        {
            // Validaciones
            var validationError = ValidateNomina(nominaDto);
            if (!string.IsNullOrEmpty(validationError))
            {
                response.SetError(validationError, 400);
                return response;
            }

            // Verificar si ya existe el UUID
            var existeUuid = await _context.CfdiComprobantes
                .AnyAsync(c => c.Uuid == nominaDto.Uuid);

            if (existeUuid)
            {
                response.SetError("Ya existe un comprobante con este UUID", 409);
                return response;
            }

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // Crear Comprobante
                var comprobante = new CfdiComprobante
                {
                    Uuid = nominaDto.Uuid,
                    Version = nominaDto.Version,
                    Serie = nominaDto.Serie,
                    Folio = nominaDto.Folio,
                    Fecha = nominaDto.Fecha,
                    FechaTimbrado = nominaDto.FechaTimbrado,
                    TipoDeComprobante = "N", // Nómina
                    LugarExpedicion = nominaDto.LugarExpedicion,
                    Moneda = nominaDto.Moneda,
                    TipoCambio = nominaDto.TipoCambio,
                    Total = nominaDto.Total,
                    SubTotal = nominaDto.SubTotal,
                    Descuento = nominaDto.Descuento,
                    MetodoPago = nominaDto.MetodoPago,
                    FormaPago = nominaDto.FormaPago,
                    Estatus = nominaDto.Estatus,
                    SelloDigital = nominaDto.SelloDigital,
                    Certificado = nominaDto.Certificado
                };

                _context.CfdiComprobantes.Add(comprobante);
                await _context.SaveChangesAsync();

                // Crear Emisor
                var emisor = new CfdiEmisor
                {
                    IdComprobante = comprobante.IdComprobante,
                    Rfc = nominaDto.Emisor.Rfc,
                    Nombre = nominaDto.Emisor.Nombre,
                    RegimenFiscal = nominaDto.Emisor.RegimenFiscal
                };
                _context.CfdiEmisors.Add(emisor);

                // Crear Receptor
                var receptor = new CfdiReceptor
                {
                    IdComprobante = comprobante.IdComprobante,
                    Rfc = nominaDto.Receptor.Rfc,
                    Nombre = nominaDto.Receptor.Nombre,
                    UsoCfdi = nominaDto.Receptor.UsoCfdi,
                    DomicilioFiscalReceptor = nominaDto.Receptor.DomicilioFiscalReceptor,
                    RegimenFiscalReceptor = nominaDto.Receptor.RegimenFiscalReceptor
                };
                _context.CfdiReceptors.Add(receptor);

                // Crear Detalle de Nómina
                var nominaDetalle = new NominaDetalle
                {
                    IdComprobante = comprobante.IdComprobante,
                    TipoNomina = nominaDto.NominaDetalle.TipoNomina,
                    FechaPago = nominaDto.NominaDetalle.FechaPago,
                    FechaInicialPago = nominaDto.NominaDetalle.FechaInicialPago,
                    FechaFinalPago = nominaDto.NominaDetalle.FechaFinalPago,
                    NumDiasPagados = nominaDto.NominaDetalle.NumDiasPagados,
                    TotalPercepciones = nominaDto.NominaDetalle.TotalPercepciones,
                    TotalDeducciones = nominaDto.NominaDetalle.TotalDeducciones,
                    TotalOtrosPagos = nominaDto.NominaDetalle.TotalOtrosPagos,
                    CurpEmpleado = nominaDto.NominaDetalle.CurpEmpleado,
                    NumSeguridadSocial = nominaDto.NominaDetalle.NumSeguridadSocial,
                    FechaInicioRelLaboral = nominaDto.NominaDetalle.FechaInicioRelLaboral,
                    Antiguedad = nominaDto.NominaDetalle.Antiguedad,
                    TipoContrato = nominaDto.NominaDetalle.TipoContrato,
                    NumEmpleado = nominaDto.NominaDetalle.NumEmpleado,
                    Departamento = nominaDto.NominaDetalle.Departamento,
                    Puesto = nominaDto.NominaDetalle.Puesto,
                    SalarioBaseCotApor = nominaDto.NominaDetalle.SalarioBaseCotApor,
                    SalarioDiarioIntegrado = nominaDto.NominaDetalle.SalarioDiarioIntegrado,
                    PeriodicidadPago = nominaDto.NominaDetalle.PeriodicidadPago,
                    CuentaBancaria = nominaDto.NominaDetalle.CuentaBancaria,
                    ClaveEntFed = nominaDto.NominaDetalle.ClaveEntFed
                };
                _context.NominaDetalles.Add(nominaDetalle);

                // Crear Percepciones
                foreach (var percepcion in nominaDto.Percepciones)
                {
                    var nominaPercepcion = new NominaPercepcione
                    {
                        IdComprobante = comprobante.IdComprobante,
                        TipoPercepcion = percepcion.TipoPercepcion,
                        Clave = percepcion.Clave,
                        Concepto = percepcion.Concepto,
                        ImporteGravado = percepcion.ImporteGravado,
                        ImporteExento = percepcion.ImporteExento
                    };
                    _context.NominaPercepciones.Add(nominaPercepcion);
                }

                // Crear Deducciones
                foreach (var deduccion in nominaDto.Deducciones)
                {
                    var nominaDeduccion = new NominaDeduccione
                    {
                        IdComprobante = comprobante.IdComprobante,
                        TipoDeduccion = deduccion.TipoDeduccion,
                        Clave = deduccion.Clave,
                        Concepto = deduccion.Concepto,
                        Importe = deduccion.Importe
                    };
                    _context.NominaDeducciones.Add(nominaDeduccion);
                }

                // Crear Otros Pagos
                foreach (var otroPago in nominaDto.OtrosPagos)
                {
                    var nominaOtroPago = new NominaOtrosPago
                    {
                        IdComprobante = comprobante.IdComprobante,
                        TipoOtroPago = otroPago.TipoOtroPago,
                        Clave = otroPago.Clave,
                        Concepto = otroPago.Concepto,
                        Importe = otroPago.Importe,
                        SubsidioCausado = otroPago.SubsidioCausado
                    };
                    _context.NominaOtrosPagos.Add(nominaOtroPago);
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                // Obtener el comprobante completo
                var comprobanteCreado = await _context.CfdiComprobantes
                    .Include(c => c.CfdiEmisor)
                    .Include(c => c.CfdiReceptor)
                    .Include(c => c.NominaDetalle)
                    .Include(c => c.NominaPercepciones)
                    .Include(c => c.NominaDeducciones)
                    .Include(c => c.NominaOtrosPagos)
                    .FirstAsync(c => c.IdComprobante == comprobante.IdComprobante);

                var dto = MapToDTO(comprobanteCreado);
                response.SetComplete(dto);
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
        catch (Exception ex)
        {
            Loggers.Error(GetType(), _infoToken.UserId, nameof(Create), $"{ex}");
            response.SetError("Error al crear el CFDI de Nómina");
        }
        return response;
    }

    public async Task<ApiResponse<CFDINominaDTO>> Update(int idComprobante, CFDINominaDTO nominaDto)
    {
        var response = new ApiResponse<CFDINominaDTO>();
        try
        {
            // Validaciones
            var validationError = ValidateNomina(nominaDto);
            if (!string.IsNullOrEmpty(validationError))
            {
                response.SetError(validationError, 400);
                return response;
            }

            var comprobante = await _context.CfdiComprobantes
                .Include(c => c.CfdiEmisor)
                .Include(c => c.CfdiReceptor)
                .Include(c => c.NominaDetalle)
                .Include(c => c.NominaPercepciones)
                .Include(c => c.NominaDeducciones)
                .Include(c => c.NominaOtrosPagos)
                .FirstOrDefaultAsync(c => c.IdComprobante == idComprobante && c.TipoDeComprobante == "N");

            if (comprobante == null)
            {
                response.SetError("No se encontró el CFDI de Nómina", 404);
                return response;
            }

            // Verificar si el UUID cambió y ya existe
            if (comprobante.Uuid != nominaDto.Uuid)
            {
                var existeUuid = await _context.CfdiComprobantes
                    .AnyAsync(c => c.Uuid == nominaDto.Uuid && c.IdComprobante != idComprobante);

                if (existeUuid)
                {
                    response.SetError("Ya existe un comprobante con este UUID", 409);
                    return response;
                }
            }

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // Actualizar Comprobante
                comprobante.Uuid = nominaDto.Uuid;
                comprobante.Version = nominaDto.Version;
                comprobante.Serie = nominaDto.Serie;
                comprobante.Folio = nominaDto.Folio;
                comprobante.Fecha = nominaDto.Fecha;
                comprobante.FechaTimbrado = nominaDto.FechaTimbrado;
                comprobante.LugarExpedicion = nominaDto.LugarExpedicion;
                comprobante.Moneda = nominaDto.Moneda;
                comprobante.TipoCambio = nominaDto.TipoCambio;
                comprobante.Total = nominaDto.Total;
                comprobante.SubTotal = nominaDto.SubTotal;
                comprobante.Descuento = nominaDto.Descuento;
                comprobante.MetodoPago = nominaDto.MetodoPago;
                comprobante.FormaPago = nominaDto.FormaPago;
                comprobante.Estatus = nominaDto.Estatus;
                comprobante.SelloDigital = nominaDto.SelloDigital;
                comprobante.Certificado = nominaDto.Certificado;
                _context.Entry(comprobante).State = EntityState.Modified;

                // Actualizar Emisor
                if (comprobante.CfdiEmisor != null)
                {
                    comprobante.CfdiEmisor.Rfc = nominaDto.Emisor.Rfc;
                    comprobante.CfdiEmisor.Nombre = nominaDto.Emisor.Nombre;
                    comprobante.CfdiEmisor.RegimenFiscal = nominaDto.Emisor.RegimenFiscal;
                    _context.Entry(comprobante.CfdiEmisor).State = EntityState.Modified;
                }

                // Actualizar Receptor
                if (comprobante.CfdiReceptor != null)
                {
                    comprobante.CfdiReceptor.Rfc = nominaDto.Receptor.Rfc;
                    comprobante.CfdiReceptor.Nombre = nominaDto.Receptor.Nombre;
                    comprobante.CfdiReceptor.UsoCfdi = nominaDto.Receptor.UsoCfdi;
                    comprobante.CfdiReceptor.DomicilioFiscalReceptor = nominaDto.Receptor.DomicilioFiscalReceptor;
                    comprobante.CfdiReceptor.RegimenFiscalReceptor = nominaDto.Receptor.RegimenFiscalReceptor;
                    _context.Entry(comprobante.CfdiReceptor).State = EntityState.Modified;
                }

                // Actualizar Detalle de Nómina
                if (comprobante.NominaDetalle != null)
                {
                    comprobante.NominaDetalle.TipoNomina = nominaDto.NominaDetalle.TipoNomina;
                    comprobante.NominaDetalle.FechaPago = nominaDto.NominaDetalle.FechaPago;
                    comprobante.NominaDetalle.FechaInicialPago = nominaDto.NominaDetalle.FechaInicialPago;
                    comprobante.NominaDetalle.FechaFinalPago = nominaDto.NominaDetalle.FechaFinalPago;
                    comprobante.NominaDetalle.NumDiasPagados = nominaDto.NominaDetalle.NumDiasPagados;
                    comprobante.NominaDetalle.TotalPercepciones = nominaDto.NominaDetalle.TotalPercepciones;
                    comprobante.NominaDetalle.TotalDeducciones = nominaDto.NominaDetalle.TotalDeducciones;
                    comprobante.NominaDetalle.TotalOtrosPagos = nominaDto.NominaDetalle.TotalOtrosPagos;
                    comprobante.NominaDetalle.CurpEmpleado = nominaDto.NominaDetalle.CurpEmpleado;
                    comprobante.NominaDetalle.NumSeguridadSocial = nominaDto.NominaDetalle.NumSeguridadSocial;
                    comprobante.NominaDetalle.FechaInicioRelLaboral = nominaDto.NominaDetalle.FechaInicioRelLaboral;
                    comprobante.NominaDetalle.Antiguedad = nominaDto.NominaDetalle.Antiguedad;
                    comprobante.NominaDetalle.TipoContrato = nominaDto.NominaDetalle.TipoContrato;
                    comprobante.NominaDetalle.NumEmpleado = nominaDto.NominaDetalle.NumEmpleado;
                    comprobante.NominaDetalle.Departamento = nominaDto.NominaDetalle.Departamento;
                    comprobante.NominaDetalle.Puesto = nominaDto.NominaDetalle.Puesto;
                    comprobante.NominaDetalle.SalarioBaseCotApor = nominaDto.NominaDetalle.SalarioBaseCotApor;
                    comprobante.NominaDetalle.SalarioDiarioIntegrado = nominaDto.NominaDetalle.SalarioDiarioIntegrado;
                    comprobante.NominaDetalle.PeriodicidadPago = nominaDto.NominaDetalle.PeriodicidadPago;
                    comprobante.NominaDetalle.CuentaBancaria = nominaDto.NominaDetalle.CuentaBancaria;
                    comprobante.NominaDetalle.ClaveEntFed = nominaDto.NominaDetalle.ClaveEntFed;
                    _context.Entry(comprobante.NominaDetalle).State = EntityState.Modified;
                }

                // Eliminar y recrear Percepciones
                _context.NominaPercepciones.RemoveRange(comprobante.NominaPercepciones);
                foreach (var percepcion in nominaDto.Percepciones)
                {
                    var nominaPercepcion = new NominaPercepcione
                    {
                        IdComprobante = idComprobante,
                        TipoPercepcion = percepcion.TipoPercepcion,
                        Clave = percepcion.Clave,
                        Concepto = percepcion.Concepto,
                        ImporteGravado = percepcion.ImporteGravado,
                        ImporteExento = percepcion.ImporteExento
                    };
                    _context.NominaPercepciones.Add(nominaPercepcion);
                }

                // Eliminar y recrear Deducciones
                _context.NominaDeducciones.RemoveRange(comprobante.NominaDeducciones);
                foreach (var deduccion in nominaDto.Deducciones)
                {
                    var nominaDeduccion = new NominaDeduccione
                    {
                        IdComprobante = idComprobante,
                        TipoDeduccion = deduccion.TipoDeduccion,
                        Clave = deduccion.Clave,
                        Concepto = deduccion.Concepto,
                        Importe = deduccion.Importe
                    };
                    _context.NominaDeducciones.Add(nominaDeduccion);
                }

                // Eliminar y recrear Otros Pagos
                _context.NominaOtrosPagos.RemoveRange(comprobante.NominaOtrosPagos);
                foreach (var otroPago in nominaDto.OtrosPagos)
                {
                    var nominaOtroPago = new NominaOtrosPago
                    {
                        IdComprobante = idComprobante,
                        TipoOtroPago = otroPago.TipoOtroPago,
                        Clave = otroPago.Clave,
                        Concepto = otroPago.Concepto,
                        Importe = otroPago.Importe,
                        SubsidioCausado = otroPago.SubsidioCausado
                    };
                    _context.NominaOtrosPagos.Add(nominaOtroPago);
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                // Obtener el comprobante actualizado
                var comprobanteActualizado = await _context.CfdiComprobantes
                    .Include(c => c.CfdiEmisor)
                    .Include(c => c.CfdiReceptor)
                    .Include(c => c.NominaDetalle)
                    .Include(c => c.NominaPercepciones)
                    .Include(c => c.NominaDeducciones)
                    .Include(c => c.NominaOtrosPagos)
                    .FirstAsync(c => c.IdComprobante == idComprobante);

                var dto = MapToDTO(comprobanteActualizado);
                response.SetComplete(dto);
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
        catch (Exception ex)
        {
            Loggers.Error(GetType(), _infoToken.UserId, nameof(Update), $"{ex}");
            response.SetError("Error al actualizar el CFDI de Nómina");
        }
        return response;
    }

    public async Task<ApiResponse<bool>> Delete(int idComprobante)
    {
        var response = new ApiResponse<bool>();
        try
        {
            var comprobante = await _context.CfdiComprobantes
                .Include(c => c.CfdiEmisor)
                .Include(c => c.CfdiReceptor)
                .Include(c => c.NominaDetalle)
                .Include(c => c.NominaPercepciones)
                .Include(c => c.NominaDeducciones)
                .Include(c => c.NominaOtrosPagos)
                .FirstOrDefaultAsync(c => c.IdComprobante == idComprobante && c.TipoDeComprobante == "N");

            if (comprobante == null)
            {
                response.SetError("No se encontró el CFDI de Nómina", 404);
                return response;
            }

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // Eliminar en orden inverso por las relaciones
                if (comprobante.NominaPercepciones.Any())
                    _context.NominaPercepciones.RemoveRange(comprobante.NominaPercepciones);

                if (comprobante.NominaDeducciones.Any())
                    _context.NominaDeducciones.RemoveRange(comprobante.NominaDeducciones);

                if (comprobante.NominaOtrosPagos.Any())
                    _context.NominaOtrosPagos.RemoveRange(comprobante.NominaOtrosPagos);

                if (comprobante.NominaDetalle != null)
                    _context.NominaDetalles.Remove(comprobante.NominaDetalle);

                if (comprobante.CfdiReceptor != null)
                    _context.CfdiReceptors.Remove(comprobante.CfdiReceptor);

                if (comprobante.CfdiEmisor != null)
                    _context.CfdiEmisors.Remove(comprobante.CfdiEmisor);

                _context.CfdiComprobantes.Remove(comprobante);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                response.SetComplete(true);
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
        catch (Exception ex)
        {
            Loggers.Error(GetType(), _infoToken.UserId, nameof(Delete), $"{ex}");
            response.SetError("Error al eliminar el CFDI de Nómina");
        }
        return response;
    }

    // Métodos auxiliares
    private string ValidateNomina(CFDINominaDTO nominaDto)
    {
        if (string.IsNullOrWhiteSpace(nominaDto.Uuid))
            return "El UUID es requerido";

        if (string.IsNullOrWhiteSpace(nominaDto.Emisor?.Rfc))
            return "El RFC del emisor es requerido";

        if (string.IsNullOrWhiteSpace(nominaDto.Receptor?.Rfc))
            return "El RFC del receptor es requerido";

        if (nominaDto.Total <= 0)
            return "El total debe ser mayor a cero";

        if (nominaDto.SubTotal <= 0)
            return "El subtotal debe ser mayor a cero";

        if (string.IsNullOrWhiteSpace(nominaDto.NominaDetalle?.TipoNomina))
            return "El tipo de nómina es requerido";

        if (nominaDto.NominaDetalle.NumDiasPagados <= 0)
            return "El número de días pagados debe ser mayor a cero";

        if (string.IsNullOrWhiteSpace(nominaDto.NominaDetalle.CurpEmpleado))
            return "El CURP del empleado es requerido";

        // Validar formato de RFC (básico)
        if (!System.Text.RegularExpressions.Regex.IsMatch(nominaDto.Emisor.Rfc, @"^[A-ZÑ&]{3,4}\d{6}[A-Z0-9]{3}$"))
            return "El RFC del emisor no tiene un formato válido";

        if (!System.Text.RegularExpressions.Regex.IsMatch(nominaDto.Receptor.Rfc, @"^[A-ZÑ&]{3,4}\d{6}[A-Z0-9]{3}$"))
            return "El RFC del receptor no tiene un formato válido";

        // Validar formato de CURP (básico)
        if (!System.Text.RegularExpressions.Regex.IsMatch(nominaDto.NominaDetalle.CurpEmpleado, @"^[A-Z]{4}\d{6}[HM][A-Z]{5}[0-9A-Z]\d$"))
            return "El CURP del empleado no tiene un formato válido";

        return string.Empty;
    }

    private CFDINominaDTO MapToDTO(CfdiComprobante comprobante)
    {
        return new CFDINominaDTO
        {
            IdComprobante = comprobante.IdComprobante,
            Uuid = comprobante.Uuid,
            Version = comprobante.Version,
            Serie = comprobante.Serie,
            Folio = comprobante.Folio,
            Fecha = comprobante.Fecha,
            FechaTimbrado = comprobante.FechaTimbrado,
            TipoDeComprobante = comprobante.TipoDeComprobante,
            LugarExpedicion = comprobante.LugarExpedicion,
            Moneda = comprobante.Moneda,
            TipoCambio = comprobante.TipoCambio,
            Total = comprobante.Total,
            SubTotal = comprobante.SubTotal,
            Descuento = comprobante.Descuento,
            MetodoPago = comprobante.MetodoPago,
            FormaPago = comprobante.FormaPago,
            Estatus = comprobante.Estatus,
            SelloDigital = comprobante.SelloDigital,
            Certificado = comprobante.Certificado,
            Emisor = comprobante.CfdiEmisor != null ? new CfdiEmisorDTO
            {
                Rfc = comprobante.CfdiEmisor.Rfc,
                Nombre = comprobante.CfdiEmisor.Nombre,
                RegimenFiscal = comprobante.CfdiEmisor.RegimenFiscal
            } : new CfdiEmisorDTO(),
            Receptor = comprobante.CfdiReceptor != null ? new CfdiReceptorDTO
            {
                Rfc = comprobante.CfdiReceptor.Rfc,
                Nombre = comprobante.CfdiReceptor.Nombre,
                UsoCfdi = comprobante.CfdiReceptor.UsoCfdi,
                DomicilioFiscalReceptor = comprobante.CfdiReceptor.DomicilioFiscalReceptor,
                RegimenFiscalReceptor = comprobante.CfdiReceptor.RegimenFiscalReceptor
            } : new CfdiReceptorDTO(),
            NominaDetalle = comprobante.NominaDetalle != null ? new NominaDetalleDTO
            {
                TipoNomina = comprobante.NominaDetalle.TipoNomina,
                FechaPago = comprobante.NominaDetalle.FechaPago,
                FechaInicialPago = comprobante.NominaDetalle.FechaInicialPago,
                FechaFinalPago = comprobante.NominaDetalle.FechaFinalPago,
                NumDiasPagados = comprobante.NominaDetalle.NumDiasPagados,
                TotalPercepciones = comprobante.NominaDetalle.TotalPercepciones,
                TotalDeducciones = comprobante.NominaDetalle.TotalDeducciones,
                TotalOtrosPagos = comprobante.NominaDetalle.TotalOtrosPagos,
                CurpEmpleado = comprobante.NominaDetalle.CurpEmpleado,
                NumSeguridadSocial = comprobante.NominaDetalle.NumSeguridadSocial,
                FechaInicioRelLaboral = comprobante.NominaDetalle.FechaInicioRelLaboral,
                Antiguedad = comprobante.NominaDetalle.Antiguedad,
                TipoContrato = comprobante.NominaDetalle.TipoContrato,
                NumEmpleado = comprobante.NominaDetalle.NumEmpleado,
                Departamento = comprobante.NominaDetalle.Departamento,
                Puesto = comprobante.NominaDetalle.Puesto,
                SalarioBaseCotApor = comprobante.NominaDetalle.SalarioBaseCotApor,
                SalarioDiarioIntegrado = comprobante.NominaDetalle.SalarioDiarioIntegrado,
                PeriodicidadPago = comprobante.NominaDetalle.PeriodicidadPago,
                CuentaBancaria = comprobante.NominaDetalle.CuentaBancaria,
                ClaveEntFed = comprobante.NominaDetalle.ClaveEntFed
            } : new NominaDetalleDTO(),
            Percepciones = comprobante.NominaPercepciones.Select(p => new NominaPercepcionDTO
            {
                IdPercepcion = p.IdPercepcion,
                TipoPercepcion = p.TipoPercepcion,
                Clave = p.Clave,
                Concepto = p.Concepto,
                ImporteGravado = p.ImporteGravado,
                ImporteExento = p.ImporteExento
            }).ToList(),
            Deducciones = comprobante.NominaDeducciones.Select(d => new NominaDeduccionDTO
            {
                IdDeduccion = d.IdDeduccion,
                TipoDeduccion = d.TipoDeduccion,
                Clave = d.Clave,
                Concepto = d.Concepto,
                Importe = d.Importe
            }).ToList(),
            OtrosPagos = comprobante.NominaOtrosPagos.Select(o => new NominaOtroPagoDTO
            {
                IdOtroPago = o.IdOtroPago,
                TipoOtroPago = o.TipoOtroPago,
                Clave = o.Clave,
                Concepto = o.Concepto,
                Importe = o.Importe,
                SubsidioCausado = o.SubsidioCausado
            }).ToList()
        };
    }
}
