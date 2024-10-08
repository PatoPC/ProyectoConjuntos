﻿using System;
using System.Collections.Generic;

namespace ConjuntosEntidades.Entidades
{
    /// <summary>
    /// ESTADO = Para controlar si se debe generar el Adeudo del mes, porque puede estar en mantenimiento.
    /// </summary>
    public partial class Departamento
    {
        public Departamento()
        {
            Adeudos = new HashSet<Adeudo>();
            AreasDepartamentos = new HashSet<AreasDepartamento>();
            TipoPersonas = new HashSet<TipoPersona>();
        }

        public Guid IdDepartamento { get; set; }
        public Guid IdTorres { get; set; }
        public Guid? IdConMst { get; set; }
        public bool Estado { get; set; }
        public decimal AliqDepartamento { get; set; }
        public string CodigoDepartamento { get; set; } = null!;
        public decimal MetrosDepartamento { get; set; }
        public decimal? SaldoInicialAnual { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public DateTime? FechaModificacion { get; set; }
        public string? UsuarioModificacion { get; set; }

        public virtual ConMst? IdConMstNavigation { get; set; }
        public virtual Torre IdTorresNavigation { get; set; } = null!;
        public virtual ICollection<Adeudo> Adeudos { get; set; }
        public virtual ICollection<AreasDepartamento> AreasDepartamentos { get; set; }
        public virtual ICollection<TipoPersona> TipoPersonas { get; set; }
    }
}
