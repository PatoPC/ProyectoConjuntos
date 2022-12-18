using System;
using System.Collections.Generic;

namespace GestionUsuarioDB.Entidades;

public partial class Rol
{
    public Guid IdRol { get; set; }

    public string NombreRol { get; set; } = null!;

    public bool Estado { get; set; }

    public Guid? IdPaginaInicioRol { get; set; }

    public DateTime FechaCreacion { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public string UsuarioCreacion { get; set; } = null!;

    public string UsuarioModificacion { get; set; } = null!;

    public bool AccesoTodos { get; set; }

    public bool RolRestringido { get; set; }

    public virtual ICollection<Modulo> Modulos { get; } = new List<Modulo>();

    public virtual ICollection<Usuario> Usuarios { get; } = new List<Usuario>();
}
