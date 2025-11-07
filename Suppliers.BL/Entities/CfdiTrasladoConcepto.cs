using System;
using System.Collections.Generic;

namespace Suppliers.BL.Entities;

public partial class CfdiTrasladoConcepto
{
    public int IdTraslado { get; set; }

    public int IdConcepto { get; set; }

    public decimal Base { get; set; }

    public string Impuesto { get; set; }

    public string TipoFactor { get; set; }

    public decimal TasaOcuota { get; set; }

    public decimal? Importe { get; set; }

    public virtual CfdiConcepto IdConceptoNavigation { get; set; }
}
