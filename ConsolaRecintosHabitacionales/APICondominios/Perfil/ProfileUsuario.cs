using AutoMapper;
using DTOs.Conjunto;
using DTOs.Usuarios;
using GestionUsuarioDB.Entidades;

namespace APICondominios.Perfil
{
    public class ProfileUsuario : Profile
    {
        public ProfileUsuario()
        {
            CreateMap<UsuarioDTOCompleto, Usuario>();
            CreateMap<Usuario, UsuarioDTOCompleto>();
            CreateMap<UsuarioCambioContrasena, Usuario>();


            CreateMap<UsuarioSesionDTO, Usuario>();
            CreateMap<Usuario, UsuarioSesionDTO>().
                ForMember(x => x.NombreRol, y => y.MapFrom(fuent => fuent.IdRolNavigation.NombreRol)).
                ForMember(x => x.Modulos, y => y.MapFrom(fuent => fuent.IdRolNavigation.Modulos)).
                ForMember(x => x.ContrasenaInicial, y => y.MapFrom(fuent => fuent.ContrasenaInicial)).
                ForMember(x => x.ListaConjuntosAcceso, y => y.MapFrom(fuent => fuent.UsuarioConjuntos));

            //ForMember(x => x.AccesoTodosRol, y => y.MapFrom(fuent => fuent.IdRolNavigation.AccesoTodos)).
            //ForMember(x => x.idPaginaDefault, y => y.MapFrom(fuent => fuent.IdRolNavigation.IdPaginaInicioRol)).

            //ForMember(x => x.Modulos, y => y.MapFrom(fuent => fuent.IdRolNavigation.Modulos));


            //CreateMap<UsuarioConjunto, GestionUsuarioDB>();

            //CreateMap<UsuarioConjunto, ConjuntoResultadoBusquedaDTO>().
            CreateMap<UsuarioConjunto, ResultadoBusquedaConjuntos>();
           

            //CreateMap<Origen, Destino>();
            CreateMap<UsuarioDTOCrear, Usuario>().
            ForMember(x => x.UsuarioConjuntos, y => y.MapFrom(fuent => fuent.UsuarioConjuntos));

            CreateMap<Usuario, UsuarioDTOCrear>().
            ForMember(x => x.UsuarioConjuntos, y => y.MapFrom(fuent => fuent.UsuarioConjuntos));

            CreateMap<Usuario, UsuarioDTOCompleto>();

            CreateMap<UsuarioConjunto, UsuarioConjuntoDTO>();                
            CreateMap<UsuarioConjuntoDTO, UsuarioConjunto>();                

            //CreateMap<EmpresaUsuarioDTO, UsuarioEmpresa>()
            //    .ForMember(x => x.IdConjunto, y => y.MapFrom(fuent => fuent.IdConjunto))
            //    .ForMember(x => x.IdUsuario, y => y.MapFrom(fuent => fuent.IdUsuario));


            CreateMap<UsuarioResultadoBusquedaDTO, Usuario>();
            CreateMap<Usuario, UsuarioResultadoBusquedaDTO>()
                .ForMember(x => x.Perfil, y => y.MapFrom(fuente => fuente.IdRolNavigation.NombreRol))
                .ForMember(x => x.IdConjunto, y => y.MapFrom(fuente => fuente.IdConjuntoDefault));                
                

            CreateMap<Usuario, UsuarioDTOEditar>();
            CreateMap<UsuarioDTOEditar, Usuario>();
                //.ForMember(x => x.UsuarioEmpresas, y => y.MapFrom(fuent => fuent.EmpresaUsuarioDTO));

        }
    }
}
