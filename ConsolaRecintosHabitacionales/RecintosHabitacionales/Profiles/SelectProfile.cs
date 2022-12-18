using AutoMapper;
using DTOs.CatalogoGeneral;
using DTOs.Roles;
using DTOs.Select;
using DTOs.Usuarios;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RecintosHabitacionales.Profiles
{
    public class SelectProfile : Profile
    {
        public SelectProfile()
        {
            CreateMap<CustomSelectConjuntos, SelectList>();
            CreateMap<SelectList, CustomSelectConjuntos>().
                ForMember(x => x.DataTextField, y => y.MapFrom(fuente => fuente.DataTextField)).
                ForMember(x => x.Items, y => y.MapFrom(fuente => fuente.Items)).
                ForMember(x => x.SelectedValues, y => y.MapFrom(fuente => fuente.SelectedValue.ToString()));

            CreateMap<RolDTOEditar, RolDTOCompleto>();
            CreateMap<RolDTOCompleto, RolDTOEditar>();

            CreateMap<CatalogoDTODropDown, CatalogoDTOResultadoBusqueda>();
            CreateMap<CatalogoDTOResultadoBusqueda, CatalogoDTODropDown>();

            CreateMap<UsuarioSesionDTO, UsuarioCambioContrasena>().
                ForMember(x => x.Nombre, y => y.MapFrom(fuent => fuent.Nombre)).
                ForMember(x => x.Apellido, y => y.MapFrom(fuent => fuent.Apellido)).
                ForMember(x => x.CorreoElectronico, y => y.MapFrom(fuent => fuent.CorreoElectronico)).
                ForMember(x => x.IdUsuario, y => y.MapFrom(fuent => fuent.IdUsuario));

        }
    }
}
