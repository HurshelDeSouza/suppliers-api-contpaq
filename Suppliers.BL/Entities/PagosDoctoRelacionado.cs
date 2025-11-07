using System;
using System.Collections.Generic;

namespace Suppliers.BL.Entities;

public partial class PagosDoctoRelacionado
{
    public int IdDoctoRel { get; set; }

    public int IdPago { get; set; }

    public string IdDocumento { get; set; }

    public string Serie { get; set; }

    public string Folio { get; set; }

    public string MonedaDr { get; set; }

    public decimal? EquivalenciaDr { get; set; }

    public int? NumParcialidad { get; set; }

    public decimal ImpSaldoAnt { get; set; }

    public decimal ImpPagado { get; set; }

    public decimal ImpSaldoInsoluto { get; set; }

    public string ObjetoImpDr { get; set; }

    public string ObjetoImp { get; set; }

    public virtual PagosPago IdPagoNavigation { get; set; }
}
