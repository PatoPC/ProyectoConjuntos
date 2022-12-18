using AutoMapper;
using ConjuntosEntidades.Entidades;
using DTOs.Conjunto;
using DTOs.Menu;
using DTOs.Modulo;
using DTOs.Permiso;
using DTOs.Roles;
using DTOs.Torre;
using DTOs.Usuarios;
using GestionUsuarioDB.Entidades;

namespace APICondominios.Perfil
{
    public class ProfileModuloRol : Profile
    {
        public ProfileModuloRol()
        {
            //Usuario
            CreateMap<Usuario, UsuarioDTOCompleto>();
            CreateMap<UsuarioDTOCompleto, Usuario>();

            CreateMap<Usuario, UsuarioDTOEditar>();
            CreateMap<UsuarioDTOEditar, Usuario>();

            CreateMap<Usuario, UsuarioDTOCrear>();
            CreateMap<UsuarioDTOCrear, Usuario>();

            //Rol
            CreateMap<Rol, RolDTOCompleto>().
                ForMember(x => x.listaModulos, y => y.MapFrom(y => y.Modulos));
            CreateMap<RolDTOCompleto, Rol>();

            CreateMap<Rol, RolDTOEditar>();
            CreateMap<RolDTOEditar, Rol>().
                ForMember(x => x.Modulos, y => y.MapFrom(y => y.listaModulos));

            CreateMap<Rol, RolDTOCrear>().
                ForMember(x => x.listaModulos, y => y.MapFrom(y => y.Modulos));
            CreateMap<RolDTOCrear, Rol>().
                ForMember(x => x.Modulos, y => y.MapFrom(y => y.listaModulos));

            CreateMap<Rol, RolDTOBusqueda>();
            CreateMap<RolDTOBusqueda, Rol>();

            //Modulo
            CreateMap<Modulo, ModuloDTOCompleto>();                
            CreateMap<ModuloDTOCompleto, Modulo>();

            CreateMap<Modulo, ModuloDTOCrear>();
            CreateMap<ModuloDTOCrear, Modulo>();

            CreateMap<Modulo, ModuloDTOEditar>();
            CreateMap<ModuloDTOEditar, Modulo>();

            CreateMap<Modulo, ModuloBusquedaDTO>().
                 ForMember(x => x.nombreModulo, y => y.MapFrom(y => y.Nombre));
            CreateMap<ModuloBusquedaDTO, Modulo>();

          

            //Menu
            CreateMap<Menu, MenuDTOCompleto>();
            CreateMap<MenuDTOCompleto, Menu>();

            CreateMap<Menu, MenuDTOEditar>();
            CreateMap<MenuDTOEditar, Menu>();

            CreateMap<Menu, MenuDTOCrear>();
            CreateMap<MenuDTOCrear, Menu>();

            CreateMap<MenuDTOCompleto, MenuDTOEditar>();
            CreateMap<MenuDTOEditar, MenuDTOCompleto>();
            //Permiso
            CreateMap<Permiso, PermisoDTOCompleto>();
            CreateMap<PermisoDTOCompleto, Permiso>();

            CreateMap<Permiso, PermisoDTOCrear>();
            CreateMap<PermisoDTOCrear, Permiso>();

            CreateMap<Permiso, PermisoDTOEditar>();
            CreateMap<PermisoDTOEditar, Permiso>();

           
        }

    }
}
