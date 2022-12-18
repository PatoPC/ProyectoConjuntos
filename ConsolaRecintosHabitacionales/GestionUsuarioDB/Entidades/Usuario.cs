using System;
using System.Collections.Generic;

namespace GestionUsuarioDB.Entidades;

public partial class Usuario
{
    public Guid IdUsuario { get; set; }

    public Guid IdRol { get; set; }

    public Guid IdPersona { get; set; }

    public Guid IdConjuntoDefault { get; set; }

    public bool? Estado { get; set; }

    public string? CorreoElectronico { get; set; }

    public bool ContrasenaInicial { get; set; }

    public string Contrasena { get; set; } = null!;

    public string? IndicioContrasena { get; set; }

    public DateTime? FechaUltimoIngreso { get; set; }

    public DateTime FechaCreacion { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public string UsuarioCreacion { get; set; } = null!;

    public string UsuarioModificacion { get; set; } = null!;

    public virtual Rol IdRolNavigation { get; set; } = null!;

    public virtual ICollection<UsuarioConjunto> UsuarioConjuntos { get; } = new List<UsuarioConjunto>();
}
