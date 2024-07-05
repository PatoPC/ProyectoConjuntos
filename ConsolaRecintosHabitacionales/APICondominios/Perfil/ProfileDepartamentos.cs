using AutoMapper;
using ConjuntosEntidades.Entidades;
using DTOs.AreasDepartamento;
using DTOs.Departamento;

namespace APICondominios.Perfil
{
    public class ProfileDepartamentos : Profile
    {
        public ProfileDepartamentos()
        {
            CreateMap<Departamento, DepartamentoDTOCompleto>().
                 ForMember(x => x.CuentaCon, y => y.MapFrom(fuente => fuente.IdConMstNavigation != null ? fuente.IdConMstNavigation.CuentaCon : default)).
                 ForMember(x => x.NombreCuenta, y => y.MapFrom(fuente => fuente.IdConMstNavigation != null ? fuente.IdConMstNavigation.NombreCuenta : default));

            CreateMap<DepartamentoDTOCompleto, Departamento>();

            CreateMap<Departamento, DepartamentoDTOCrear>();
            CreateMap<DepartamentoDTOCrear, Departamento>().
                ForMember(x => x.IdTorres, y => y.MapFrom(fuente => fuente.IdTorresCrearDepartamento));

            CreateMap<Departamento, DepartamentoDTOCrearArchivo>();
            CreateMap<DepartamentoDTOCrearArchivo, Departamento>().
                ForMember(x => x.IdTorres, y => y.MapFrom(fuente => fuente.IdTorresCrearDepartamento));


            CreateMap<DepartamentoDTOEditar, Departamento>().
            ForMember(x => x.IdDepartamento, y => y.MapFrom(fuente => fuente.IdDeptoEditar)).
            ForMember(x => x.CodigoDepartamento, y => y.MapFrom(fuente => fuente.CoigoDeptoEditar)).
            ForMember(x => x.MetrosDepartamento, y => y.MapFrom(fuente => fuente.MetrosDeptoEditar)).
            ForMember(x => x.AliqDepartamento, y => y.MapFrom(fuente => fuente.AliqDeptoEditar)).
            ForMember(x => x.SaldoInicialAnual, y => y.MapFrom(fuente => fuente.SaldoInicialAnualEditar));

            CreateMap<AreasDepartamentoDTO, AreasDepartamento>();
            CreateMap<AreasDepartamento, AreasDepartamentoDTO>();
        }
       
    }
}
