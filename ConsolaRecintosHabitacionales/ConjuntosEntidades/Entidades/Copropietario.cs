using System;
using System.Collections.Generic;

namespace ConjuntosEntidades.Entidades
{
    public partial class Copropietario
    {
        public Guid IdCopropietario { get; set; }
        public Guid? IdArrendatario { get; set; }
        public string NombreCondomino { get; set; } = null!;
        public string RucCondomino { get; set; } = null!;
        public string? TelefonoCondomino { get; set; }
        public string? EmailCondomino { get; set; }
        public string? CodigoDeptoCondomino { get; set; }
        public string? ObservacionCondomino { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string? UsuarioModificacion { get; set; }
    }
}
