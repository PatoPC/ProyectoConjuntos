using System;
using System.Collections.Generic;

namespace ConjuntosEntidades.Entidades
{
    public partial class TipoPersona
    {
        public Guid IdTipoPersona { get; set; }
        public Guid? IdPersona { get; set; }
        public Guid? IdDepartamento { get; set; }

        public virtual Departamento? IdDepartamentoNavigation { get; set; }
        public virtual Persona? IdPersonaNavigation { get; set; }
    }
}
