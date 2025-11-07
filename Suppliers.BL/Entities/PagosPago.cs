using System;
using System.Collections.Generic;

namespace Suppliers.BL.Entities;

public partial class PagosPago
{
    public int IdPago { get; set; }

    public int IdComprobante { get; set; }

    public DateTime FechaPago { get; set; }

    public string FormaDePagoP { get; set; }

    public string MonedaP { get; set; }

    public decimal? TipoCambioP { get; set; }

    public decimal Monto { get; set; }

    public string NumOperacion { get; set; }

    public virtual CfdiComprobante IdComprobanteNavigation { get; set; }

    public virtual ICollection<PagosDoctoRelacionado> PagosDoctoRelacionados { get; set; } = new List<PagosDoctoRelacionado>();
}
