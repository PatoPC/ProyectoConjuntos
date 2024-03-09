using System;
using System.Collections.Generic;

namespace ConjuntosEntidades.Entidades
{
    public partial class SecuencialCabeceraCont
    {
        public Guid IdSecuencialCabeceraCont { get; set; }
        public Guid? IdEncCont { get; set; }
        public int Secuencial { get; set; }

        public virtual EncabezadoContabilidad? IdEncContNavigation { get; set; }
    }
}
