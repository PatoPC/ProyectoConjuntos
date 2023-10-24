using AutoMapper;
using ConjuntosEntidades.Entidades;
using DTOs.Adeudo;
using DTOs.ConfiguracionCuenta;
using DTOs.Persona;
using DTOs.Torre;
using EntidadesCatalogos.Entidades;

namespace APICondominios.Perfil
{
    public class ProfileConfiguracionCuenta : Profile
    {
        public ProfileConfiguracionCuenta()
        {
            CreateMap<Configuracioncuentum, ConfiguraCuentasDTOCrear>();                
            CreateMap<ConfiguraCuentasDTOCrear, Configuracioncuentum>();

            CreateMap<Configuracioncuentum, ConfiguraCuentasDTOEditar>();                
            CreateMap<ConfiguraCuentasDTOEditar, Configuracioncuentum>();

            CreateMap<Configuracioncuentum, ConfiguraCuentasDTOCompleto>();
            CreateMap<ConfiguraCuentasDTOCompleto, Configuracioncuentum>();

          
        }
       
    }
}
