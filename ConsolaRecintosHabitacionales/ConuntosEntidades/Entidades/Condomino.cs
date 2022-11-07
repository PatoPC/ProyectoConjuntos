using System;
using System.Collections.Generic;

namespace ConuntosEntidades.Entidades
{
    public partial class Condomino
    {
        public Guid IdCondominio { get; set; }
        public string NombreCondomino { get; set; } = null!;
        public string RucCondomino { get; set; } = null!;
        public string DireccionCondomino { get; set; } = null!;
        public string? TelefonoCondomino { get; set; }
        public string? EmailCondomino { get; set; }
        public string? CodigoDeptoCondomino { get; set; }
        public decimal? SaldoAntCondomino { get; set; }
        public string? ObservacionCondomino { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string? UsuarioCreacion { get; set; }
        public string? UsuarioModificacion { get; set; }
    }
}
