using System;
using System.Collections.Generic;

namespace ConjuntosEntidades.Entidades
{
    public partial class Adeudo
    {
        public Guid IdAdeudos { get; set; }
        public DateTime FechaAdeudos { get; set; }
        public string CodigoDeptoAdeudos { get; set; } = null!;
        public decimal? MontoAdeudos { get; set; }
        public bool? StatusAdeudos { get; set; }
        public string? NombreAdeudos { get; set; }
        public int? CodigoAdeudos { get; set; }
    }
}
