using GestionUsuarioDB.Entidades;
using Microsoft.EntityFrameworkCore;
using RepositorioGestionUsuarios.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositorioGestionUsuarios.Implementacion
{
    public class ManageConsultasPermisos : IManageConsultasPermisos
    {
        public readonly ContextoDB_Permisos _context;

        public ManageConsultasPermisos(ContextoDB_Permisos context)
        {
            _context = context;
        }

        public async Task<List<Rol>> GetAllRolsByConjuntos()
        {
            List<Rol> listRols = new List<Rol>();
            try
            {
                listRols = await _context.Rols.Include(x => x.Modulos).ToListAsync();

            }
            catch (Exception ex)
            {

            }
            return listRols;
        }

        public async Task<Rol> GetRolByID(Guid IdRol)
        {
            try
            {
                Rol objRol = await _context.Rols.Where(c => c.IdRol == IdRol).Include(x => x.Modulos)
                    .ThenInclude(x => x.Menus).ThenInclude(x => x.Permisos).FirstOrDefaultAsync();

                return objRol;
            }
            catch (Exception ex)
            {
            }

            return null;
        }

        public async Task<List<Rol>> GetRolPorNombre(string nombreRol)
        {
            try
            {
                List<Rol> listaRol = await _context.Rols.Where(c => c.NombreRol.ToUpper().Trim().Contains(nombreRol.ToUpper().Trim())).Include(x => x.Modulos).ToListAsync();

                return listaRol;
            }
            catch (Exception ex)
            {
            }

            return null;
        }
    }
}
