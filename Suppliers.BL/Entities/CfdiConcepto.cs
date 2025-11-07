using System;
using System.Collections.Generic;

namespace Suppliers.BL.Entities;

public partial class CfdiConcepto
{
    public int IdConcepto { get; set; }

    public int IdComprobante { get; set; }

    public string ClaveProdServ { get; set; }

    public decimal Cantidad { get; set; }

    public string ClaveUnidad { get; set; }

    public string Unidad { get; set; }

    public string Descripcion { get; set; }

    public decimal ValorUnitario { get; set; }

    public decimal Importe { get; set; }

    public decimal? Descuento { get; set; }

    public string NoIdentificacion { get; set; }

    public string ObjetoImp { get; set; }

    public virtual ICollection<CfdiRetencionConcepto> CfdiRetencionConceptos { get; set; } = new List<CfdiRetencionConcepto>();

    public virtual ICollection<CfdiTrasladoConcepto> CfdiTrasladoConceptos { get; set; } = new List<CfdiTrasladoConcepto>();

    public virtual CfdiComprobante IdComprobanteNavigation { get; set; }
}
