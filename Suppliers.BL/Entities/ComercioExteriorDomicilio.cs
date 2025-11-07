using System;
using System.Collections.Generic;

namespace Suppliers.BL.Entities;

public partial class ComercioExteriorDomicilio
{
    public int IdDomicilio { get; set; }

    public int IdComprobante { get; set; }

    public string TipoDomicilio { get; set; }

    public string Calle { get; set; }

    public string CodigoPostal { get; set; }

    public string Colonia { get; set; }

    public string Estado { get; set; }

    public string Localidad { get; set; }

    public string Municipio { get; set; }

    public string NumeroExterior { get; set; }

    public string Pais { get; set; }

    public virtual CfdiComprobante IdComprobanteNavigation { get; set; }
}
