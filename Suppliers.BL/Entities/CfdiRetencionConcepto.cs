using System;
using System.Collections.Generic;

namespace Suppliers.BL.Entities;

public partial class CfdiRetencionConcepto
{
    public int IdRetencion { get; set; }

    public int IdConcepto { get; set; }

    public decimal Base { get; set; }

    public string Impuesto { get; set; }

    public string TipoFactor { get; set; }

    public decimal TasaOcuota { get; set; }

    public decimal Importe { get; set; }

    public virtual CfdiConcepto IdConceptoNavigation { get; set; }
}
