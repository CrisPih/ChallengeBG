using System;
using System.Collections.Generic;

namespace ChallengeBG.Domain.Entities;

public partial class Proveedor
{
    public int ProveedorId { get; set; }

    public string Nombre { get; set; } = null!;

    public DateTime FechaCreacion { get; set; }

    public virtual ICollection<ProductoProveedor> ProductoProveedor { get; set; } = new List<ProductoProveedor>();
}
