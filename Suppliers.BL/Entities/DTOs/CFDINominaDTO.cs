namespace Suppliers.BL.Entities.DTOs;

public class CFDINominaDTO
{
    // Datos del Comprobante
    public int? IdComprobante { get; set; }
    public string Uuid { get; set; } = "";
    public string Version { get; set; } = "";
    public string Serie { get; set; } = "";
    public string Folio { get; set; } = "";
    public DateTime Fecha { get; set; }
    public DateTime? FechaTimbrado { get; set; }
    public string TipoDeComprobante { get; set; } = "";
    public string LugarExpedicion { get; set; } = "";
    public string Moneda { get; set; } = "";
    public decimal? TipoCambio { get; set; }
    public decimal Total { get; set; }
    public decimal SubTotal { get; set; }
    public decimal? Descuento { get; set; }
    public string MetodoPago { get; set; } = "";
    public string FormaPago { get; set; } = "";
    public string Estatus { get; set; } = "";
    public string SelloDigital { get; set; } = "";
    public string Certificado { get; set; } = "";

    // Datos del Emisor
    public CfdiEmisorDTO Emisor { get; set; } = new();

    // Datos del Receptor
    public CfdiReceptorDTO Receptor { get; set; } = new();

    // Datos de Nómina
    public NominaDetalleDTO NominaDetalle { get; set; } = new();
    public List<NominaPercepcionDTO> Percepciones { get; set; } = new();
    public List<NominaDeduccionDTO> Deducciones { get; set; } = new();
    public List<NominaOtroPagoDTO> OtrosPagos { get; set; } = new();
}

public class CfdiEmisorDTO
{
    public string Rfc { get; set; } = "";
    public string Nombre { get; set; } = "";
    public string RegimenFiscal { get; set; } = "";
}

public class CfdiReceptorDTO
{
    public string Rfc { get; set; } = "";
    public string Nombre { get; set; } = "";
    public string UsoCfdi { get; set; } = "";
    public string DomicilioFiscalReceptor { get; set; } = "";
    public string RegimenFiscalReceptor { get; set; } = "";
}

public class NominaDetalleDTO
{
    public string TipoNomina { get; set; } = "";
    public DateOnly FechaPago { get; set; }
    public DateOnly FechaInicialPago { get; set; }
    public DateOnly FechaFinalPago { get; set; }
    public decimal NumDiasPagados { get; set; }
    public decimal? TotalPercepciones { get; set; }
    public decimal? TotalDeducciones { get; set; }
    public decimal? TotalOtrosPagos { get; set; }
    public string CurpEmpleado { get; set; } = "";
    public string NumSeguridadSocial { get; set; } = "";
    public DateOnly? FechaInicioRelLaboral { get; set; }
    public string Antiguedad { get; set; } = "";
    public string TipoContrato { get; set; } = "";
    public string NumEmpleado { get; set; } = "";
    public string Departamento { get; set; } = "";
    public string Puesto { get; set; } = "";
    public decimal? SalarioBaseCotApor { get; set; }
    public decimal? SalarioDiarioIntegrado { get; set; }
    public string PeriodicidadPago { get; set; } = "";
    public string CuentaBancaria { get; set; } = "";
    public string ClaveEntFed { get; set; } = "";
}

public class NominaPercepcionDTO
{
    public int? IdPercepcion { get; set; }
    public string TipoPercepcion { get; set; } = "";
    public string Clave { get; set; } = "";
    public string Concepto { get; set; } = "";
    public decimal? ImporteGravado { get; set; }
    public decimal? ImporteExento { get; set; }
}

public class NominaDeduccionDTO
{
    public int? IdDeduccion { get; set; }
    public string TipoDeduccion { get; set; } = "";
    public string Clave { get; set; } = "";
    public string Concepto { get; set; } = "";
    public decimal Importe { get; set; }
}

public class NominaOtroPagoDTO
{
    public int? IdOtroPago { get; set; }
    public string TipoOtroPago { get; set; } = "";
    public string Clave { get; set; } = "";
    public string Concepto { get; set; } = "";
    public decimal Importe { get; set; }
    public decimal? SubsidioCausado { get; set; }
}
