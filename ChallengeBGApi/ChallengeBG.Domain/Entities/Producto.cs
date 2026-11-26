using System;
using System.Collections.Generic;

namespace ChallengeBG.Domain.Entities;

public partial class Producto
{
    public int ProductoId { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public DateTime FechaCreacion { get; set; }

    public int CategoriaId { get; set; }

    public virtual Categoria Categoria { get; set; } = null!;

    public virtual ICollection<ProductoProveedor> ProductoProveedor { get; set; } = new List<ProductoProveedor>();
}
