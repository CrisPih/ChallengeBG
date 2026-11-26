using System;
using System.Collections.Generic;

namespace ChallengeBG.Domain.Entities;

public partial class Usuario
{
    public int UsuarioId { get; set; }

    public string NombreUsuario { get; set; } = null!;

    public string ClaveHash { get; set; } = null!;

    public string NombreCompleto { get; set; } = null!;

    public DateTime FechaCreacion { get; set; }
}
