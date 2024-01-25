using AutoMapper;
using ConjuntosEntidades.Entidades;
using DTOs.AreaComunal;
using DTOs.Persona;
using DTOs.ReservaArea;
using DTOs.Torre;

namespace APICondominios.Perfil
{
    public class ProfileReservaArea : Profile
    {
        public ProfileReservaArea()
        {
            CreateMap<ReservaArea, ReservaAreaDTOCrear>();
            CreateMap<ReservaAreaDTOCrear, ReservaArea>();

            CreateMap<ReservaArea, ReservaAreaDTOEditar>();
            CreateMap<ReservaAreaDTOEditar, ReservaArea>();

            CreateMap<ReservaArea, ReservaAreaDTOCompleto>();
            CreateMap<ReservaAreaDTOCompleto, ReservaArea>();

        }
       
    }
}
