using DTOs.Conjunto;
using DTOs.Modulo;
using DTOs.Select;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DTOs.Usuarios
{
    public class UsuarioSesionDTO
    {
        public Guid IdUsuario { get; set; }
        public Guid IdPersona { get; set; }
        public Guid? IdTipousuario { get; set; }
        public Guid IdConjuntoDefault { get; set; }
        public Guid? IdTipoidentificacion { get; set; }
        public DateTime? Fechaultimoingreso { get; set; }
        public bool Estado { get; set; }
        public string Nombre { get; set; }
        public string NombreUsuario { get; set; }
        public string NombreRol { get; set; }
        public string NumeroIdentificacion { get; set; }
        public bool AccesoTodosRol { get; set; }
        public bool ContrasenaInicial { get; set; }
        public string Apellido { get; set; }
        public string PaginaDefault { get; set; }
        public Guid idPaginaDefault { get; set; }
        public string CorreoElectronico { get; set; }
        public CustomSelectConjuntos ListaConjuntos { get; set; }
        public List<ModuloDTOCompleto> Modulos { get; set; }
        
        public List<ResultadoBusquedaConjuntos> ListaConjuntosAcceso { get; set; }

        public virtual SelectList ConjutosAccesoSelect
        {
            get
            {
                if (ListaConjuntosAcceso != null)
                    return new SelectList(ListaConjuntosAcceso, "IdConjunto", "NombreConjunto", IdConjuntoDefault);
                else
                    return default;

            }
        }

    }
}
