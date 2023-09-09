using AutoMapper;
using ConjuntosEntidades.Entidades;
using DTOs.AreaComunal;
using DTOs.Persona;
using DTOs.Torre;

namespace APICondominios.Perfil
{
    public class ProfileAreaComunal : Profile
    {
        public ProfileAreaComunal()
        {
            CreateMap<AreaComunal, AreaComunalDTOCrear>();                
            CreateMap<AreaComunalDTOCrear, AreaComunal>();

            CreateMap<AreaComunal, AreaComunalDTOEditar>();
            CreateMap<AreaComunalDTOEditar, AreaComunal>()
                 .ForMember(x => x.IdAreaComunal, y => y.MapFrom(fuent => fuent.IdAreaComunalEditar))
                 .ForMember(x => x.IdConjunto, y => y.MapFrom(fuent => fuent.IdConjuntoAreaComunal))
                 .ForMember(x => x.NombreArea, y => y.MapFrom(fuent => fuent.NombreAreaEditar))
                 .ForMember(x => x.HoraInicio, y => y.MapFrom(fuent => fuent.HoraInicioEditar))
                 .ForMember(x => x.HoraFin, y => y.MapFrom(fuent => fuent.HoraFinEditar))
                 .ForMember(x => x.Estado, y => y.MapFrom(fuent => fuent.EstadoEditar))
                 .ForMember(x => x.Observacion, y => y.MapFrom(fuent => fuent.ObservacionEditar))
                 .ForMember(x => x.UsuarioModificacion, y => y.MapFrom(fuent => fuent.UsuarioModificacion))
                 .ForMember(x => x.FechaModificacion, y => y.MapFrom(fuent => fuent.FechaModificacion));

            CreateMap<AreaComunal, AreaComunalDTOCompleto>();
            CreateMap<AreaComunalDTOCompleto, AreaComunal>();

        }
       
    }
}
