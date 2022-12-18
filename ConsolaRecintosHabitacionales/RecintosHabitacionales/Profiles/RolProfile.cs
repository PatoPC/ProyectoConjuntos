using AutoMapper;
using DTOs.Menu;
using DTOs.Modulo;
using DTOs.Permiso;
using DTOs.Roles;

namespace RecintosHabitacionales.Profiles
{
    public class RolProfile : Profile
    {
        public RolProfile()
        {
            CreateMap<RolDTOCompleto, RolDTOEditar>();
            CreateMap<RolDTOEditar, RolDTOCompleto>();

            CreateMap<ModuloDTOCompleto, ModuloDTOEditar>();
            CreateMap<ModuloDTOEditar, ModuloDTOCompleto>();

            CreateMap<MenuDTOCompleto, MenuDTOEditar>();
            CreateMap<MenuDTOEditar, MenuDTOCompleto>();

            CreateMap<PermisoDTOCompleto, PermisoDTOEditar>();
            CreateMap<PermisoDTOEditar, PermisoDTOCompleto>();


            /***Crear */

            CreateMap<RolDTOCompleto, RolDTOCrear>();
            CreateMap<RolDTOCrear, RolDTOCompleto>();

            CreateMap<ModuloDTOCompleto, ModuloDTOCrear>();
            CreateMap<ModuloDTOCrear, ModuloDTOCompleto>();

            CreateMap<MenuDTOCompleto, MenuDTOCrear>();
            CreateMap<MenuDTOCrear, MenuDTOCompleto>();

            CreateMap<PermisoDTOCompleto, PermisoDTOCrear>();
            CreateMap<PermisoDTOCrear, PermisoDTOCompleto>();
        }
    }
}
