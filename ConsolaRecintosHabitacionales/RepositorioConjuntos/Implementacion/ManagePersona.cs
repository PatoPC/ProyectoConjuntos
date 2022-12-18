using ConjuntosEntidades.Entidades;
using DTOs.Torre;
using RepositorioConjuntos.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;
using DTOs.Persona;

namespace RepositorioConjuntos.Implementacion
{
    public class ManagePersona : IManagePersona
    {
        public readonly ContextoDB_Condominios _context;

        public ManagePersona(ContextoDB_Condominios context)
        {
            this._context = context ?? throw new ArgumentException(nameof(context));
        }

        public Task<List<Persona>> obtenerPersonaPoNumeroIdentificacion(string numeroIdentificacion)
        {
            var listaPersonas = _context.Personas.Where(x => x.IdentificacionPersona.Trim().Contains(numeroIdentificacion.Trim())).ToListAsync();

            return listaPersonas;
        }

        public async Task<Persona> obtenerPersonaPoNumeroIdentificacionExacta(string numeroIdentificacion)
        {
            var objRepositorio = await _context.Personas.Where(x => x.IdentificacionPersona == numeroIdentificacion).FirstOrDefaultAsync();

            return objRepositorio;
        }


        public Task<List<Persona>> obtenerPersonaPorNombre(string nombrePersona)
        {
            var listaPersonas = _context.Personas.Where(x => x.NombresPersona.Trim().ToUpper().Contains(nombrePersona.Trim().ToUpper())).ToListAsync();

            return listaPersonas;
        }

        public async Task<Persona> obtenerPorIDPersona(Guid idPersona)
        {
            var objPersona = await _context.Personas.Where(x => x.IdPersona == idPersona).FirstOrDefaultAsync();

            return objPersona;
        }

        public async Task<List<Persona>> busquedaAvanzada(ObjetoBusquedaPersona objBusqueda)
        {
            List<Persona> listaPersona = new List<Persona>();

            if (!string.IsNullOrEmpty(objBusqueda.NombresPersona))
            {
                listaPersona = await _context.Personas.Where(x => x.NombresPersona.ToUpper().Trim().Contains(objBusqueda.NombresPersona.Trim().ToUpper())).ToListAsync();
            }
            else if (!string.IsNullOrEmpty(objBusqueda.ApellidosPersona))
            {
                listaPersona = await _context.Personas.Where(x => x.NombresPersona.ToUpper().Trim().Contains(objBusqueda.ApellidosPersona.Trim().ToUpper())).ToListAsync();
            }
            else if(objBusqueda.IdPersona != Guid.Empty)
            {
                listaPersona = await _context.Personas.Where(x => x.IdPersona == objBusqueda.IdPersona).ToListAsync();

                return listaPersona;
            }
            else if (!string.IsNullOrEmpty(objBusqueda.IdentificacionPersona))
            {
                listaPersona = await _context.Personas.Where(x => x.IdentificacionPersona==objBusqueda.IdentificacionPersona).ToListAsync();
            }
            else if (!string.IsNullOrEmpty(objBusqueda.IdentificacionPersona))
            {
                listaPersona = await _context.Personas.Where(x => x.EmailPersona.ToUpper().Trim().Contains(objBusqueda.EmailPersona.Trim().ToUpper())).ToListAsync();
            }
            else
            {
                listaPersona = await _context.Personas.ToListAsync();
            }


            if (!string.IsNullOrEmpty(objBusqueda.NombresPersona))            
                listaPersona = listaPersona.Where(x => x.NombresPersona.ToUpper().Trim().Contains(objBusqueda.NombresPersona.Trim().ToUpper())).ToList();

            if (!string.IsNullOrEmpty(objBusqueda.ApellidosPersona))
                listaPersona = listaPersona.Where(x => x.NombresPersona.ToUpper().Trim().Contains(objBusqueda.ApellidosPersona.Trim().ToUpper())).ToList();

            if (objBusqueda.IdPersona != Guid.Empty)            
                listaPersona = listaPersona.Where(x => x.IdPersona == objBusqueda.IdPersona).ToList();
            
            if (!string.IsNullOrEmpty(objBusqueda.IdentificacionPersona))            
                listaPersona = listaPersona.Where(x => x.IdentificacionPersona == objBusqueda.IdentificacionPersona).ToList();            

            if (!string.IsNullOrEmpty(objBusqueda.EmailPersona))            
                listaPersona = listaPersona.Where(x => x.EmailPersona.ToUpper().Trim().Contains(objBusqueda.EmailPersona.Trim().ToUpper())).ToList();
            

            return listaPersona;
        }
    }
}
