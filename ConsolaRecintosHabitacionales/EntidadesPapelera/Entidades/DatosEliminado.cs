using System;
using System.Collections.Generic;

namespace EntidadesPapelera.Entidades;

public partial class DatosEliminado
{
    public Guid IdDatosEliminados { get; set; }

    public Guid IdObjetoEliminado { get; set; }

    public string DatosEliminados { get; set; } = null!;

    public string TipoDatoEliminado { get; set; } = null!;

    public Guid IdPersonaEliminar { get; set; }

    public string UsuarioElimina { get; set; } = null!;

    public Guid IdUsuarioElimina { get; set; }

    public string NumeroIdentificacion { get; set; } = null!;

    public string? RutaArchivo { get; set; }

    public DateTime FechaEliminacion { get; set; }
}
