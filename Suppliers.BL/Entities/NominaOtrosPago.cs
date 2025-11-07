using System;
using System.Collections.Generic;

namespace Suppliers.BL.Entities;

public partial class NominaOtrosPago
{
    public int IdOtroPago { get; set; }

    public int IdComprobante { get; set; }

    public string TipoOtroPago { get; set; }

    public string Clave { get; set; }

    public string Concepto { get; set; }

    public decimal Importe { get; set; }

    public decimal? SubsidioCausado { get; set; }

    public virtual CfdiComprobante IdComprobanteNavigation { get; set; }
}
