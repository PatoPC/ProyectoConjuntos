﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTOs.Comprobantes;

namespace DTOs.Adeudo
{
    public class AdeudoDTOCompleto
    {
        public Guid IdAdeudos { get; set; }
        public Guid IdDepartamento { get; set; }
        public Guid IdPersona { get; set; }
        public DateTime FechaAdeudos { get; set; }
        public decimal MontoAdeudos { get; set; }
        public decimal SaldoPendiente { get; set; }
        public bool EstadoAdeudos { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public string NombreConjunto { get; set; } = null!;
        public string Departamento { get; set; } = null!;
        public string Observacion { get; set; } = null!;
        public string Torre { get; set; } = null!;
        public Guid? IdBancoComprobante { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public DateTime? FechaModificacion { get; set; }
        public string? UsuarioModificacion { get; set; }
        public List<ComprobantePagoDTOCompleto> PagoAdeudos { get; set; }

    }
}
