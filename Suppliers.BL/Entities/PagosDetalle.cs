using System;
using System.Collections.Generic;

namespace Suppliers.BL.Entities;

public partial class PagosDetalle
{
    public int IdComprobante { get; set; }

    public string FormaDePago { get; set; }

    public decimal MontoTotalPagos { get; set; }

    public decimal? TotalTrasladosBaseIva16 { get; set; }

    public decimal? TotalTrasladosImpuestoIva16 { get; set; }

    public virtual CfdiComprobante IdComprobanteNavigation { get; set; }
}
