using System;
using System.Collections.Generic;

namespace Suppliers.BL.Entities;

public partial class CfdiEmisor
{
    public int IdComprobante { get; set; }

    public string Rfc { get; set; }

    public string Nombre { get; set; }

    public string RegimenFiscal { get; set; }

    public string RegistroPatronal { get; set; }

    public virtual CfdiComprobante IdComprobanteNavigation { get; set; }
}
