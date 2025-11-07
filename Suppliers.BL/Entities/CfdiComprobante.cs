using System;
using System.Collections.Generic;

namespace Suppliers.BL.Entities;

public partial class CfdiComprobante
{
    public int IdComprobante { get; set; }

    public string Uuid { get; set; }

    public string Version { get; set; }

    public string Serie { get; set; }

    public string Folio { get; set; }

    public DateTime Fecha { get; set; }

    public DateTime? FechaTimbrado { get; set; }

    public string TipoDeComprobante { get; set; }

    public string LugarExpedicion { get; set; }

    public string Moneda { get; set; }

    public decimal? TipoCambio { get; set; }

    public decimal Total { get; set; }

    public decimal SubTotal { get; set; }

    public decimal? Descuento { get; set; }

    public string MetodoPago { get; set; }

    public string FormaPago { get; set; }

    public string Estatus { get; set; }

    public string SelloDigital { get; set; }

    public string Certificado { get; set; }

    public virtual ICollection<CfdiConcepto> CfdiConceptos { get; set; } = new List<CfdiConcepto>();

    public virtual CfdiEmisor CfdiEmisor { get; set; }

    public virtual CfdiReceptor CfdiReceptor { get; set; }

    public virtual ComercioExteriorDetalle ComercioExteriorDetalle { get; set; }

    public virtual ICollection<ComercioExteriorDomicilio> ComercioExteriorDomicilios { get; set; } = new List<ComercioExteriorDomicilio>();

    public virtual ICollection<ComercioExteriorMercancium> ComercioExteriorMercancia { get; set; } = new List<ComercioExteriorMercancium>();

    public virtual ICollection<NominaDeduccione> NominaDeducciones { get; set; } = new List<NominaDeduccione>();

    public virtual NominaDetalle NominaDetalle { get; set; }

    public virtual ICollection<NominaOtrosPago> NominaOtrosPagos { get; set; } = new List<NominaOtrosPago>();

    public virtual ICollection<NominaPercepcione> NominaPercepciones { get; set; } = new List<NominaPercepcione>();

    public virtual PagosDetalle PagosDetalle { get; set; }

    public virtual ICollection<PagosPago> PagosPagos { get; set; } = new List<PagosPago>();
}
