﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Persona
{
    public class PersonaDTOCompleto
    {
        public Guid IdPersona { get; set; }
        public Guid IdTipoIdentificacion { get; set; }
        public Guid IdUsuario { get; set; }
        public string? NombresPersona { get; set; } = null!;
        public string? ApellidosPersona { get; set; }
        public string IdentificacionPersona { get; set; } = null!;
        public string? TelefonoPersona { get; set; }
        public string? CelularPersona { get; set; }
        public string? EmailPersona { get; set; }
        public string? ObservacionPersona { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string? UsuarioCreacion { get; set; } = null!;
        public string? UsuarioModificacion { get; set; }
        public List<TipoPersonaDTOCompleto>? TipoPersonas { get; set; }
    }
}
