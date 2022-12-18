using System;
using System.Collections.Generic;

namespace GestionLogs.Entidades;

public partial class LogsExcepcione
{
    public Guid IdLogsExcepciones { get; set; }

    public Guid? IdUsuario { get; set; }

    public string Metodo { get; set; } = null!;

    public string Entidad { get; set; } = null!;

    public string Error { get; set; } = null!;

    public DateTime FechaError { get; set; }

    public string? Descripcion { get; set; }
}
