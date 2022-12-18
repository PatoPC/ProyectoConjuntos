using System;
using System.Collections.Generic;

namespace GestionUsuarioDB.Entidades;

public partial class Modulo
{
    public Guid IdModulo { get; set; }

    public Guid? IdRol { get; set; }

    public string Nombre { get; set; } = null!;

    public string? IconoModulo { get; set; }

    public virtual Rol? IdRolNavigation { get; set; }

    public virtual ICollection<Menu> Menus { get; } = new List<Menu>();
}
