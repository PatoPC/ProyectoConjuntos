using AutoMapper;
using ConjuntosEntidades.Entidades;
using DTOs.Adeudo;
using DTOs.Parametro;
using DTOs.Proveedor;

namespace APICondominios.Perfil
{
    public class ProfileParametro : Profile    {
        public ProfileParametro() {

            CreateMap<Parametro, ParametroCompletoDTO>();
            CreateMap<ParametroCompletoDTO, Parametro>();

            CreateMap<Parametro, ParametroCrearDTO>();
            CreateMap<ParametroCrearDTO, Parametro>();

            CreateMap<Parametro, ParametroEditarDTO>();
            CreateMap<ParametroEditarDTO, Parametro>();


        }
        
            
    }
}
