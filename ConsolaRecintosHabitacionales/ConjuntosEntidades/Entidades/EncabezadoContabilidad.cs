using System;
using System.Collections.Generic;

namespace ConjuntosEntidades.Entidades
{
    /// <summary>
    /// 1 Ingreso/deposito
    ///    2 egreso/pagos
    ///    3 asientos de diarios
    ///    9 comprobante generado automaticamente
    ///    
    ///    N_COMP_ENC_CONT -&gt; mes del año
    ///    CONCEPTO_ENC_CONT -&gt; generación automatica
    ///    
    /// </summary>
    public partial class EncabezadoContabilidad
    {
        public EncabezadoContabilidad()
        {
            DetalleContabilidads = new HashSet<DetalleContabilidad>();
            SecuencialCabeceraConts = new HashSet<SecuencialCabeceraCont>();
        }

        public Guid IdEncCont { get; set; }
        public Guid IdConjunto { get; set; }
        public Guid TipoDocNEncCont { get; set; }
        public int Mes { get; set; }
        public DateTime FechaEncCont { get; set; }
        public int? ChequeEncCont { get; set; }
        public string ConceptoEncCont { get; set; } = null!;
        public bool? AnuladoEncCont { get; set; }
        public DateTime? FecAnulaEncCont { get; set; }
        public DateTime? FecVenciEncCont { get; set; }
        public string UsuarioCreacion { get; set; } = null!;

        public virtual Conjunto IdConjuntoNavigation { get; set; } = null!;
        public virtual ICollection<DetalleContabilidad> DetalleContabilidads { get; set; }
        public virtual ICollection<SecuencialCabeceraCont> SecuencialCabeceraConts { get; set; }
    }
}
