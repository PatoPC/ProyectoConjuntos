using EntidadesCatalogos.Entidades;
using Microsoft.EntityFrameworkCore;
using RepositorioCatalogos.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilitarios;

namespace RepositorioCatalogos.Implementacion
{
    public class ManageConsultasCatalogos : IManageConsultasCatalogos
    {
        public readonly ContextoDB_Catalogos _context;
        public ManageConsultasCatalogos(ContextoDB_Catalogos context)
        {
            this._context = context ?? throw new ArgumentException(nameof(context));
        }


        #region Search
        #region Get Catalogo by Id
        public async Task<Catalogo> GetCatalogoById(Guid idCatalogo)
        {
            Catalogo catalogo = new Catalogo();

            try
            {
                catalogo = await _context.Catalogos
                        .Where(c => c.IdCatalogo == idCatalogo)
                        .Include(x => x.IdCatalogopadreNavigation)
                        .Include(x => x.InverseIdCatalogopadreNavigation).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

            }


            return catalogo;
        }

        //public async Task<Catalogo> GetCatalogoByIdInstancia(Guid idCatalogo)
        //{
        //    Catalogo catalogo = new Catalogo();
        //    using (var dbContext = new ContextoDB_Catalogos())
        //    {
        //        catalogo = await dbContext.Catalogos.Where(c => c.IdCatalogo == idCatalogo).Include(x => x.IdCatalogopadreNavigation).FirstOrDefaultAsync();
        //    }

        //    return catalogo;
        //}
        #endregion

        #region Get All 
        public async Task<List<Catalogo>> GetAllCatalogoByConjunto(Guid idConjunto)
        {
            List<Catalogo> listCatalogo = new();
            try
            {
                listCatalogo = await _context.Catalogos.Where(x => x.IdConjunto == idConjunto).ToListAsync();
            }
            catch (Exception ex)
            {

            }
            return listCatalogo;
        }
        #endregion

        #region Get by Name Conjunto
        public async Task<List<Catalogo>> GetCatalogoByNameIdConjunto(string name, Guid idConjunto)
        {
            if (!string.IsNullOrEmpty(name))
            {
                List<Catalogo> listCatalogo = await _context.Catalogos.Where(c => c.NombreCatalogo.Trim().ToUpper().Contains(name.Trim().ToUpper()) && c.IdConjunto == idConjunto).ToListAsync();

                return listCatalogo;
            }

            return default;
        }
        #endregion

        #region Get by Name
        public async Task<List<Catalogo>> GetCatalogoByName(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                List<Catalogo> listCatalogo = await _context.Catalogos.Where(c => c.NombreCatalogo.Trim().ToUpper().Contains(name.Trim().ToUpper())).ToListAsync();

                return listCatalogo;
            }

            return default;
        }

        public async Task<Catalogo> GetCatalogoByNameExact(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                Catalogo objRepositorio = await _context.Catalogos.Where(c => c.NombreCatalogo.Trim().ToUpper() == name.Trim().ToUpper()).FirstOrDefaultAsync();

                //Catalogo objRepositorio = await _context.Catalogos.Where(c => FuncionesUtiles.quitarTildes(c.NombreCatalogo).Trim().ToUpper()==FuncionesUtiles.quitarTildes(name).Trim().ToUpper()).FirstOrDefaultAsync();

                return objRepositorio;
            }

            return default;
        }

      

        #endregion

        #region Get by Code
        public async Task<Catalogo> GetCatalogoByCodeIDEmpresa(string code, Guid? idConjunto)
        {
            try
            {
                if (idConjunto != ConstantesAplicacion.guidNulo && idConjunto != null)
                {
                    Catalogo objCatalogo = await _context.Catalogos.Where(c => c.CodigoCatalogo.Trim().ToUpper() == code.Trim().ToUpper() && c.IdConjunto == idConjunto).Include(x => x.InverseIdCatalogopadreNavigation).FirstOrDefaultAsync();
                    return objCatalogo;
                }
                else
                {
                    Catalogo objCatalogo = await _context.Catalogos.Where(c => c.CodigoCatalogo.Trim().ToUpper() == code.Trim().ToUpper()).Include(x => x.InverseIdCatalogopadreNavigation).FirstOrDefaultAsync();
                    return objCatalogo;
                }
            }
            catch (Exception ex) 
            {

            }
            return null;
        }
        #endregion

        #region Get Child by Parent Code 
        public async Task<List<Catalogo>> GetChildByParentCodeIDEmpresa(string code, Guid idConjunto)
        {
            if (idConjunto != ConstantesAplicacion.guidNulo)
            {
                var listaCatalogos = await _context.Catalogos.Where(x => x.CodigoCatalogo.Trim().ToUpper() == code.Trim().ToUpper() && x.IdConjunto == idConjunto)
                        .Include(x => x.InverseIdCatalogopadreNavigation)
                        .SelectMany(y => y.InverseIdCatalogopadreNavigation).OrderBy(x => x.NombreCatalogo).ToListAsync();
                return listaCatalogos;
            }
            else
            {
                var listaCatalogos = await _context.Catalogos.Where(x => x.CodigoCatalogo.Trim().ToUpper() == code.Trim().ToUpper())
                        .Include(x => x.InverseIdCatalogopadreNavigation)
                        .SelectMany(y => y.InverseIdCatalogopadreNavigation).OrderBy(x => x.NombreCatalogo).ToListAsync();
                return listaCatalogos;
            }

        }

        public async Task<List<Catalogo>> GetCatalogsChildsByIDFather(Guid idCodigoPadreCatalgo)
        {
            List<Catalogo> lista = new List<Catalogo>();

            lista = await _context.Catalogos.Where(x => x.IdCatalogo == idCodigoPadreCatalgo)
                    .Include(x => x.InverseIdCatalogopadreNavigation)
                    .SelectMany(y => y.InverseIdCatalogopadreNavigation).OrderBy(x => x.NombreCatalogo).ToListAsync();


            return lista;
        }


        #endregion

        #region Get Child by Parent Code by ID
        public async Task<List<Catalogo>> GetChildByParentCodeByID_IDEmpresa(Guid idPadre, Guid idConjunto)
        {
            List<Catalogo> listaCatalogos = new List<Catalogo>();

            if (idConjunto != ConstantesAplicacion.guidNulo)
            {
                listaCatalogos = await _context.Catalogos.Where(x => x.IdCatalogopadre == idPadre && x.IdConjunto == idConjunto).ToListAsync(); 
            }
            else
            {
                listaCatalogos = await _context.Catalogos.Where(x => x.IdCatalogopadre == idPadre).ToListAsync();
            }

            return listaCatalogos;
        }
        #endregion

        #region Get Child by Parent Code by Name
        public async Task<List<Catalogo>> GetChildByParentCodeByID_Nombre(string nombrePadre)
        {
            var catalogoPadre = await _context.Catalogos.Where(x => x.NombreCatalogo.ToLower().Trim() == nombrePadre.ToLower().Trim()).Include(x => x.InverseIdCatalogopadreNavigation).FirstOrDefaultAsync();

            //Se coloca esta condición debido a que en ROL, los módulos que se muestran estan en una sola empresa, por eso cuando se crea una empresa en Orienfluvial y Oriendrin no encuentra los modulos.
            if(catalogoPadre==null)
                catalogoPadre = await _context.Catalogos.Where(x => x.NombreCatalogo.ToLower().Trim() == nombrePadre.ToLower().Trim()).Include(x => x.InverseIdCatalogopadreNavigation).FirstOrDefaultAsync();

            if (catalogoPadre !=null)
            {
                var listaCatalogos = catalogoPadre.InverseIdCatalogopadreNavigation.ToList();

                return listaCatalogos;
            }

            return new List<Catalogo>();
        }
        #endregion

        #region Get Parent by Child
        public async Task<List<Catalogo>> GetCatalogsUpLevel_Conjunto(Guid idCatalogoHijo, Guid idConjunto)
        {
            List<Catalogo> lista = new List<Catalogo>();

            if (idConjunto!=ConstantesAplicacion.guidNulo)
            {
                lista = await _context.Catalogos.Where(x => x.IdCatalogo == idCatalogoHijo && x.IdConjunto == idConjunto).
                        Include(x => x.IdCatalogopadreNavigation)
                        .SelectMany(y => y.IdCatalogopadreNavigation.IdCatalogopadreNavigation.InverseIdCatalogopadreNavigation).ToListAsync(); 
            }
            else
            {
                lista = await _context.Catalogos.Where(x => x.IdCatalogo == idCatalogoHijo).
                        Include(x => x.IdCatalogopadreNavigation)
                        .SelectMany(y => y.IdCatalogopadreNavigation.IdCatalogopadreNavigation.InverseIdCatalogopadreNavigation).ToListAsync();
            }

            return lista;
        }
        #endregion

        #region Get Catalogs Same Label
        public async Task<List<Catalogo>> GetCatalogsSameLevelByID_Conjunto(Guid idCatalogoHermano, Guid idConjunto)
        {
            List<Catalogo> lista = new List<Catalogo>();
            try
            {
                if (idConjunto != ConstantesAplicacion.guidNulo)
                {
                    lista = await _context.Catalogos.Where(x => x.IdCatalogo == idCatalogoHermano && x.IdConjunto == idConjunto).Include(x => x.IdCatalogopadreNavigation)
                                   .SelectMany(y => y.IdCatalogopadreNavigation.InverseIdCatalogopadreNavigation).ToListAsync();
                }
                else
                {
                    lista = await _context.Catalogos.Where(x => x.IdCatalogo == idCatalogoHermano).Include(x => x.IdCatalogopadreNavigation)
                                   .SelectMany(y => y.IdCatalogopadreNavigation.InverseIdCatalogopadreNavigation).ToListAsync();
                }


                return lista;
            }
            catch (Exception)
            {
            }

            return null;
        }

        public async Task<List<Catalogo>> GetCatalogsChildsIDConjuntoIDCatalogoPadre(Guid idCodigoPadreCatalgo, Guid idConjunto)
        {
            List<Catalogo> listaCatalogos = new List<Catalogo>();

            if (idConjunto!=ConstantesAplicacion.guidNulo)
            {
                listaCatalogos = await _context.Catalogos.Where(x => x.IdCatalogopadre == idCodigoPadreCatalgo && x.IdConjunto == idConjunto)
                         .Include(x => x.InverseIdCatalogopadreNavigation).ToListAsync(); 
            }
            else
            {
                listaCatalogos = await _context.Catalogos.Where(x => x.IdCatalogopadre == idCodigoPadreCatalgo)
                        .Include(x => x.InverseIdCatalogopadreNavigation).ToListAsync();
            }

           return listaCatalogos;
        }

        public async Task<List<Catalogo>> GetAllCatalogos()
        {
            List<Catalogo> listCatalogo = new();
            try
            {
                listCatalogo = await _context.Catalogos.ToListAsync();
            }
            catch (Exception ex)
            {

            }
            return listCatalogo;
        }     
        #endregion

        public async Task<List<Catalogo>> GetAllCatalogosPorNivel(int nivel)
        {
            List<Catalogo> listaCatalogos = new List<Catalogo>();

            listaCatalogos = await _context.Catalogos.Where(x => x.NivelCatalogo == nivel).ToListAsync();

            return listaCatalogos;
        }

        #endregion
    }
}
