using System;
using System.Collections.Generic;

namespace ChallengeBG.Domain.Entities;

public partial class ProductoProveedor
{
    public int ProductoProveedorId { get; set; }

    public int ProductoId { get; set; }

    public int ProveedorId { get; set; }

    public string NumeroLote { get; set; } = null!;

    public decimal Precio { get; set; }

    public int Stock { get; set; }

    public virtual Producto Producto { get; set; } = null!;

    public virtual Proveedor Proveedor { get; set; } = null!;
}
