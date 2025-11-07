using System;
using System.Collections.Generic;

namespace Suppliers.BL.Entities;

public partial class CfdiReceptor
{
    public int IdComprobante { get; set; }

    public string Rfc { get; set; }

    public string Nombre { get; set; }

    public string DomicilioFiscalReceptor { get; set; }

    public string RegimenFiscalReceptor { get; set; }

    public string UsoCfdi { get; set; }

    public string NumRegIdTrib { get; set; }

    public string ResidenciaFiscal { get; set; }

    public virtual CfdiComprobante IdComprobanteNavigation { get; set; }
}
