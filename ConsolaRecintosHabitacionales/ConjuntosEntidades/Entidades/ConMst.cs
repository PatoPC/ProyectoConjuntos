using System;
using System.Collections.Generic;

namespace ConjuntosEntidades.Entidades
{
    /// <summary>
    /// Maestro contable (plan de cuentas)
    /// </summary>
    public partial class ConMst
    {
        public ConMst()
        {
            Departamentos = new HashSet<Departamento>();
            InverseIdConMstPadreNavigation = new HashSet<ConMst>();
        }

        public Guid IdConMst { get; set; }
        public Guid IdConjunto { get; set; }
        public Guid? IdConMstPadre { get; set; }
        public string CuentaCon { get; set; } = null!;
        public string NombreCuenta { get; set; } = null!;
        public bool Grupo { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string? UsuarioModificacion { get; set; }

        public virtual ConMst? IdConMstPadreNavigation { get; set; }
        public virtual Conjunto IdConjuntoNavigation { get; set; } = null!;
        public virtual ICollection<Departamento> Departamentos { get; set; }
        public virtual ICollection<ConMst> InverseIdConMstPadreNavigation { get; set; }
    }
}
