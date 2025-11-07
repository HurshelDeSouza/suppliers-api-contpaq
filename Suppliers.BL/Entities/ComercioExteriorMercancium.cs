using System;
using System.Collections.Generic;

namespace Suppliers.BL.Entities;

public partial class ComercioExteriorMercancium
{
    public int IdMercancia { get; set; }

    public int IdComprobante { get; set; }

    public string NoIdentificacion { get; set; }

    public string FraccionArancelaria { get; set; }

    public string UnidadAduana { get; set; }

    public decimal CantidadAduana { get; set; }

    public decimal ValorDolares { get; set; }

    public decimal? ValorUnitarioAduana { get; set; }

    public string Descripcion { get; set; }

    public string UnidadMedida { get; set; }

    public virtual CfdiComprobante IdComprobanteNavigation { get; set; }
}
