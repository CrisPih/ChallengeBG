using System;
using System.Collections.Generic;

namespace ChallengeBG.Domain.Entities;

public partial class Categoria
{
    public int CategoriaId { get; set; }

    public string Nombre { get; set; } = null!;

    public DateTime FechaCreacion { get; set; }

    public virtual ICollection<Producto> Producto { get; set; } = new List<Producto>();
}
