using System;
using System.Collections.Generic;

namespace Suppliers.BL.Entities;

public partial class NominaDeduccione
{
    public int IdDeduccion { get; set; }

    public int IdComprobante { get; set; }

    public string TipoDeduccion { get; set; }

    public string Clave { get; set; }

    public string Concepto { get; set; }

    public decimal Importe { get; set; }

    public virtual CfdiComprobante IdComprobanteNavigation { get; set; }
}
