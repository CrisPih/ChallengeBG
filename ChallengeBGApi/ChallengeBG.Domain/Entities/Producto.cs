using System;
using System.Collections.Generic;

namespace ChallengeBG.Domain.Entities;

public partial class Producto
{
    public int ProductoId { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public DateTime FechaCreacion { get; set; }

    public virtual ICollection<ProductoProveedor> ProductoProveedor { get; set; } = new List<ProductoProveedor>();
}
