using System;
using System.Collections.Generic;

namespace ConjuntosEntidades.Entidades
{
    public partial class AreasDepartamento
    {
        public Guid IdAreasDepartamentos { get; set; }
        public Guid IdDepartamento { get; set; }
        public Guid IdTipoArea { get; set; }
        public decimal? MetrosCuadrados { get; set; }

        public virtual Departamento IdDepartamentoNavigation { get; set; } = null!;
    }
}
