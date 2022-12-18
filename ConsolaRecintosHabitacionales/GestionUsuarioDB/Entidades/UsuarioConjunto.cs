using System;
using System.Collections.Generic;

namespace GestionUsuarioDB.Entidades;

public partial class UsuarioConjunto
{
    public Guid IdUsuairoConjunto { get; set; }

    public Guid IdUsuario { get; set; }

    public Guid IdConjunto { get; set; }

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
