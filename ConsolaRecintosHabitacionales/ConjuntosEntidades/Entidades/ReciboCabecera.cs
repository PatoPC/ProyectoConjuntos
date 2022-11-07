using System;
using System.Collections.Generic;

namespace ConjuntosEntidades.Entidades
{
    public partial class ReciboCabecera
    {
        public ReciboCabecera()
        {
            ReciboDetalles = new HashSet<ReciboDetalle>();
        }

        public Guid IdReciboCab { get; set; }
        public int NroReciboReciboCab { get; set; }
        public DateTime FechaReciboCab { get; set; }
        public int CodClienteReciboCab { get; set; }
        public string ConceptoReciboCab { get; set; } = null!;
        public bool? StatusReciboCab { get; set; }
        public DateTime? FechaCreacionReciboCab { get; set; }
        public DateTime? FechaModificacionReciboCab { get; set; }
        public string? UsuarioCreacionReciboCab { get; set; }
        public string? UsuarioModificacionReciboCab { get; set; }

        public virtual ICollection<ReciboDetalle> ReciboDetalles { get; set; }
    }
}
