using System;
using System.Collections.Generic;

namespace ConjuntosEntidades.Entidades
{
    /// <summary>
    /// ID_TIPO_PERSONA_DEPARTAMENTO: aqui se guarda el catalogo para identificar si la persona es Propietario o Arendatario (Condominio), los codigos de los catalogos son: 
    ///    mdueño
    ///    opcInqui
    /// </summary>
    public partial class TipoPersona
    {
        public Guid IdTipoPersona { get; set; }
        public Guid IdPersona { get; set; }
        public Guid IdDepartamento { get; set; }
        public Guid IdTipoPersonaDepartamento { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string? UsuarioModificacion { get; set; }

        public virtual Departamento IdDepartamentoNavigation { get; set; } = null!;
        public virtual Persona IdPersonaNavigation { get; set; } = null!;
    }
}
