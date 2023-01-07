using AutoMapper;
using ConjuntosEntidades.Entidades;
using DTOs.Persona;
using DTOs.Proveedor;

namespace APICondominios.Perfil
{
    public class ProfileProveedor : Profile
    {
        public ProfileProveedor()
        {
            CreateMap<Proveedore, ProveedorDTOCrear>();
            CreateMap<ProveedorDTOCrear, Proveedore>();

            CreateMap<Proveedore, ProveedorDTOEditar>();
            CreateMap<ProveedorDTOEditar, Proveedore>();

            CreateMap<Proveedore, ProveedorDTOCompleto>();
            CreateMap<ProveedorDTOCompleto, Proveedore>();


        }
    }
}
