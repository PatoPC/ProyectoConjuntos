using AutoMapper;
using DTOs.CatalogoGeneral;
using EntidadesCatalogos.Entidades;

namespace APICondominios.Perfil
{
    public class ProfileCatalogo : Profile
    {
        public ProfileCatalogo()
        {
            CreateMap<Catalogo, CatalogoDTOCrear>();
            CreateMap<CatalogoDTOCrear, Catalogo>();

            CreateMap<Catalogo, CatalogoDTOCompleto>().
                ForMember(x => x.CodigoCatalogoPadre, y => y.MapFrom(fuente => fuente.IdCatalogopadreNavigation.CodigoCatalogo)).
                ForMember(x => x.NombreCatalogoPadre, y => y.MapFrom(fuente => fuente.IdCatalogopadreNavigation.NombreCatalogo));
            CreateMap<CatalogoDTOCompleto, Catalogo>();

            CreateMap<Catalogo, CatalogoDTOActualizar>();
            CreateMap<CatalogoDTOActualizar, Catalogo>();

           
            CreateMap<Catalogo, CatalogoDTOResultadoBusqueda>().
                ForMember(x => x.CodigoCatalogoPadre, y => y.MapFrom(fuente => fuente.IdCatalogopadreNavigation.CodigoCatalogo)).
                ForMember(x => x.NombreCatalogoPadre, y => y.MapFrom(fuente => fuente.IdCatalogopadreNavigation.NombreCatalogo));

            CreateMap<CatalogoDTOResultadoBusqueda, Catalogo>();

            CreateMap<Catalogo, CatalogoDTODropDown>();

            CreateMap<CatalogoDTODropDown, Catalogo>();

         
        }
    }
}
