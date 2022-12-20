using ConjuntosEntidades.Entidades;
using DTOs.Persona;
using DTOs.Torre;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositorioConjuntos.Interface
{
    public interface IManagePersona
    {
        public Task<Persona> obtenerPorIDPersona(Guid idPersona);
        public Task<List<Persona>> obtenerPersonaPorNombre(string nombreTorre);    
        public Task<List<Persona>> obtenerPersonaPoNumeroIdentificacion(string numeroIdentificacion);    
        public Task<Persona> obtenerPersonaPoNumeroIdentificacionExacta(string numeroIdentificacion);    
        public Task<List<Persona>> busquedaAvanzada(ObjetoBusquedaPersona objBusqueda);    
        public Task<TipoPersona> busquedaPersonaDepartamento(ObjTipoPersonaDepartamento objBusqueda);    
    }
}
