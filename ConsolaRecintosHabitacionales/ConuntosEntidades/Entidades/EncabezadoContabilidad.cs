﻿using System;
using System.Collections.Generic;

namespace ConuntosEntidades.Entidades
{
    public partial class EncabezadoContabilidad
    {
        public EncabezadoContabilidad()
        {
            DetalleContabilidads = new HashSet<DetalleContabilidad>();
        }

        public Guid IdEncCont { get; set; }
        public int TipoDocNEncCont { get; set; }
        public int NCompEncCont { get; set; }
        public DateTime FechaEncCont { get; set; }
        public int? ChequeEncCont { get; set; }
        public string ConceptoEncCont { get; set; } = null!;
        public int? NroRetEncCont { get; set; }
        public bool? AnuladoEncCont { get; set; }
        public string? CtacontEncCont { get; set; }
        public DateTime? FecAnulaEncCont { get; set; }
        public DateTime? FecVenciEncCont { get; set; }
        public string UsuarioCreacionEncCont { get; set; } = null!;

        public virtual ICollection<DetalleContabilidad> DetalleContabilidads { get; set; }
    }
}
