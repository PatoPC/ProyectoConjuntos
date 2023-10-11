using System;
using System.Collections.Generic;

namespace ConjuntosEntidades.Entidades
{
    public partial class ConMst
    {
        public ConMst()
        {
            DepartamentoConIdConMstNavigations = new HashSet<Departamento>();
            DepartamentoIdConMstNavigations = new HashSet<Departamento>();
        }

        public Guid IdConMst { get; set; }
        public Guid IdConjunto { get; set; }
        public string CuentaCon { get; set; } = null!;
        public string NombreCuenta { get; set; } = null!;
        public bool Grupo { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string? UsuarioModificacion { get; set; }

        public virtual Conjunto IdConjuntoNavigation { get; set; } = null!;
        public virtual ICollection<Departamento> DepartamentoConIdConMstNavigations { get; set; }
        public virtual ICollection<Departamento> DepartamentoIdConMstNavigations { get; set; }
    }
}
