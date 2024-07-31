using AutoMapper;
using ConjuntosEntidades.Entidades;
using DTOs.Conjunto;
using DTOs.Torre;

namespace APICondominios.Perfil
{
    public class ProfileConjuntos : Profile
    {
        public ProfileConjuntos()
        {
            CreateMap<Conjunto, ConjuntoDTOCompleto>();
            CreateMap<ConjuntoDTOCompleto, Conjunto>();

            CreateMap<Conjunto, ConjuntoDTOCrear>();
            CreateMap<ConjuntoDTOCrear, Conjunto>().
                ForMember(x => x.TelefonoConjunto, y => y.MapFrom(fuente => fuente.TelefonoConjunto));

            CreateMap<Conjunto, ConjuntoDTOCrearArchivo>();
            CreateMap<ConjuntoDTOCrearArchivo, Conjunto>().
                ForMember(x => x.TelefonoConjunto, y => y.MapFrom(fuente => fuente.TelefonoConjunto));

            CreateMap<Torre, TorreDTOEditar>();

            CreateMap<Conjunto, ResultadoBusquedaConjuntos>();
            CreateMap<ConjuntoDTOEditar, Conjunto>();
           
        }
       
    }
}
