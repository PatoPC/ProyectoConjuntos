using AutoMapper;
using ConjuntosEntidades.Entidades;
using DTOs.Conjunto;
using DTOs.Torre;

namespace APICondominios.Perfil
{
    public class ProfileTorre : Profile
    {
        public ProfileTorre()
        {
            CreateMap<TorreDTOCrear, Torre>();
            CreateMap<Torre, TorreDTOCrear>();

            CreateMap<Torre, TorreDTOCompleto>().
                 ForMember(x => x.NombreConjunto, y => y.MapFrom(fuente => fuente.IdConjuntoNavigation.NombreConjunto));
            CreateMap<TorreDTOCompleto, Torre>();          
                    

            CreateMap<Conjunto, BusquedaTorres>();

            CreateMap<TorreDTOEditar, Torre>().
                ForMember(x => x.NombreTorres, y => y.MapFrom(fuente => fuente.NombreTorresEditar));
            CreateMap<Torre, TorreDTOEditar>();

        }
       
    }
}
