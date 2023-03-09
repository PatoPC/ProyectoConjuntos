using AutoMapper;
using ConjuntosEntidades.Entidades;
using DTOs.CatalogoGeneral;
using DTOs.MaestroContable;
using EntidadesCatalogos.Entidades;

namespace APICondominios.Perfil
{
    public class ProfileMaestroContable : Profile
    {
        public ProfileMaestroContable()
        {
            CreateMap<MaestroContableDTOCrear, ConMst>();

            CreateMap<ConMst, MaestroContableDTOCompleto>();

        }
    }
}
