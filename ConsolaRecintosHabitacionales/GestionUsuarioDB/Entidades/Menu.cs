using System;
using System.Collections.Generic;

namespace GestionUsuarioDB.Entidades;

public partial class Menu
{
    public Guid IdMenu { get; set; }

    public Guid? IdModulo { get; set; }

    public string NombreMenu { get; set; } = null!;

    public string RutaMenu { get; set; } = null!;

    public string? DatoIcono { get; set; }

    public virtual Modulo? IdModuloNavigation { get; set; }

    public virtual ICollection<Permiso> Permisos { get; } = new List<Permiso>();
}
