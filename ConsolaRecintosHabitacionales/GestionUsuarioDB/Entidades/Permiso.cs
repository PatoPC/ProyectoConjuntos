using System;
using System.Collections.Generic;

namespace GestionUsuarioDB.Entidades;

public partial class Permiso
{
    public Guid IdPermisos { get; set; }

    public Guid? IdMenu { get; set; }

    public string NombrePermiso { get; set; } = null!;

    public bool Concedido { get; set; }

    public string? CssPermiso { get; set; }

    public virtual Menu? IdMenuNavigation { get; set; }
}
