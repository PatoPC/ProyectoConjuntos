using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConjuntosEntidades.Entidades;
using DTOs.Proveedor;
using Microsoft.EntityFrameworkCore;
using RepositorioConjuntos.Interface;

namespace RepositorioProveedores.Implementacion
{
    public class ManageProveedor : IManageProveedor
    {
        public readonly ContextoDB_Condominios _context;

        public ManageProveedor(ContextoDB_Condominios context)
        {
            this._context = context ?? throw new ArgumentException(nameof(context));
        }

        public async Task<List<Proveedore>> busquedaAvanzada(BusquedaProveedor objBusqueda)
        {
            List<Proveedore> listaProveedor = await _context.Proveedores.ToListAsync();

            if (objBusqueda.IdProveedor != null)
                listaProveedor = listaProveedor.Where(x => x.IdProveedor == objBusqueda.IdProveedor).ToList();

            if (!string.IsNullOrEmpty(objBusqueda.NombreProveedor))
                listaProveedor = listaProveedor.Where(x => x.NombreProveedor.Trim().ToUpper().Contains(objBusqueda.NombreProveedor.Trim().ToUpper())).ToList();

            if (!string.IsNullOrEmpty(objBusqueda.RucProveedor))
                listaProveedor = listaProveedor.Where(x => x.RucProveedor.Trim().Contains(objBusqueda.RucProveedor.Trim())).ToList();


            return listaProveedor;
        }

        public async Task<List<Proveedore>> busquedaTodosProveedor(Guid idConjuunto)
        {
            List<Proveedore> listaProveedor = await _context.Proveedores.Where(x => x.IdProveedor==idConjuunto).ToListAsync();

            return listaProveedor;
        }

        public async Task<Proveedore> obtenerPorIDProveedor(Guid idProveedor)
        {
            Proveedore objProveedor = await _context.Proveedores.Where(x => x.IdProveedor == idProveedor).FirstAsync();

            return objProveedor;
        }

        public async Task<List<Proveedore>> obtenerPorNombre(string nombreProducto)
        {
            List<Proveedore> listaProveedor = await _context.Proveedores.Where(x => x.NombreProveedor.Trim().ToUpper() == nombreProducto.Trim().ToUpper()).ToListAsync();

            return listaProveedor;
        }

        public async Task<List<Proveedore>> obtenerPorRUC(string ruc)
        {
            List<Proveedore> listaProveedor = await _context.Proveedores.Where(x => x.RucProveedor.Trim() == ruc.Trim()).ToListAsync();

            return listaProveedor;
        }
    }
}
