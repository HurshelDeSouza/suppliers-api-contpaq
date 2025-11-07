using System;
using System.Collections.Generic;

namespace Suppliers.BL.Entities;

public partial class ComercioExteriorDetalle
{
    public int IdComprobante { get; set; }

    public string Incoterm { get; set; }

    public string ClaveDePedimento { get; set; }

    public decimal TipoCambioUsd { get; set; }

    public decimal TotalUsd { get; set; }

    public virtual CfdiComprobante IdComprobanteNavigation { get; set; }
}
