﻿using AutoMapper;
using ConjuntosEntidades.Entidades;
using DTOs.Adeudo;
using DTOs.Comprobantes;
using DTOs.Persona;
using DTOs.Torre;

namespace APICondominios.Perfil
{
    public class ProfileAdeudo : Profile
    {
        public ProfileAdeudo()
        {
            CreateMap<Adeudo, AdeudoDTOCrear>();                
            CreateMap<AdeudoDTOCrear, Adeudo>();

            CreateMap<AdeudoDTOEditar, Adeudo>();
            CreateMap<Adeudo, AdeudoDTOEditar>();

            CreateMap<Adeudo, AdeudoDTOPagar>();
            CreateMap<AdeudoDTOPagar, Adeudo>();

            CreateMap<ComprobantePago, ComprobantePagoDTOCompleto>();
            CreateMap<ComprobantePagoDTOCompleto, ComprobantePago>();
            

            CreateMap<Adeudo, AdeudoDTOCompleto>()
                .ForMember(x => x.Nombre, y => y.MapFrom(fuente => fuente.IdPersonaNavigation.NombresPersona))
                .ForMember(x => x.Apellido, y => y.MapFrom(fuente => fuente.IdPersonaNavigation.ApellidosPersona))
                .ForMember(x => x.NombreConjunto, y => y.MapFrom(fuente => fuente.IdDepartamentoNavigation.IdTorresNavigation.IdConjuntoNavigation.NombreConjunto))
                .ForMember(x => x.Torre, y => y.MapFrom(fuente => fuente.IdDepartamentoNavigation.IdTorresNavigation.NombreTorres))
                .ForMember(x => x.Departamento, y => y.MapFrom(fuente => fuente.IdDepartamentoNavigation.CodigoDepartamento));

            CreateMap<DetalleComprobantePago, DetalleComprobantePagoDTOCompleto>();
            CreateMap<DetalleComprobantePagoDTOCompleto, DetalleComprobantePago>();

            CreateMap<SecuencialComprobantePagoDTO, SecuencialComprobantePago>();
            CreateMap<SecuencialComprobantePago, SecuencialComprobantePagoDTO>();
        }
       
    }
}
