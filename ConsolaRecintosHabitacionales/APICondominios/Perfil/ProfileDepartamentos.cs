using AutoMapper;
using ConjuntosEntidades.Entidades;
using DTOs.Departamento;

namespace APICondominios.Perfil
{
    public class ProfileDepartamentos : Profile
    {
        public ProfileDepartamentos()
        {
            CreateMap<Departamento, DepartamentoDTOCompleto>();
            CreateMap<DepartamentoDTOCompleto, Departamento>();

            CreateMap<Departamento, DepartamentoDTOCrear>();
            CreateMap<DepartamentoDTOCrear, Departamento>().
                ForMember(x => x.IdTorres, y => y.MapFrom(fuente => fuente.IdTorresCrearDepartamento));


            CreateMap<DepartamentoDTOEditar, Departamento>().
            ForMember(x => x.CoigoDepto, y => y.MapFrom(fuente => fuente.CoigoDeptoEditar)).
            ForMember(x => x.MetrosDepto, y => y.MapFrom(fuente => fuente.MetrosDeptoEditar)).
            ForMember(x => x.AliqDepto, y => y.MapFrom(fuente => fuente.AliqDeptoEditar)).
            ForMember(x => x.SaldoInicialAnual, y => y.MapFrom(fuente => fuente.SaldoInicialAnualEditar));
        }
       
    }
}
