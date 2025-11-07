using System;
using System.Collections.Generic;

namespace Suppliers.BL.Entities;

public partial class NominaPercepcione
{
    public int IdPercepcion { get; set; }

    public int IdComprobante { get; set; }

    public string TipoPercepcion { get; set; }

    public string Clave { get; set; }

    public string Concepto { get; set; }

    public decimal? ImporteGravado { get; set; }

    public decimal? ImporteExento { get; set; }

    public virtual CfdiComprobante IdComprobanteNavigation { get; set; }
}
