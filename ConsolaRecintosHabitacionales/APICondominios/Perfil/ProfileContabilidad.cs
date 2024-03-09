using AutoMapper;
using ConjuntosEntidades.Entidades;
using DTOs.CatalogoGeneral;
using DTOs.Contabilidad;
using EntidadesCatalogos.Entidades;

namespace APICondominios.Perfil
{
    public class ProfileContabilidad : Profile
    {
        public ProfileContabilidad()
        {
            CreateMap<EncabezadoContabilidad, EncabezContDTOCrear>();
            CreateMap<EncabezContDTOCrear, EncabezadoContabilidad>();

            CreateMap<EncabezContDTOCompleto, EncabezadoContabilidad>();
            CreateMap<EncabezadoContabilidad, EncabezContDTOCompleto>();

            CreateMap<DetalleContabilidadCrear, DetalleContabilidad>();
            CreateMap<DetalleContabilidad, DetalleContabilidadCrear>();
        }
       
        
    }
}
