using EntidadesCatalogos.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositorioCatalogos.Interface
{
    public interface IManageConsultasCatalogos
    {

        #region Search
        public Task<Catalogo> GetCatalogoById(Guid idCatalogo);
        //public Task<Catalogo> GetCatalogoByIdInstancia(Guid idCatalogo);
        public Task<List<Catalogo>> GetAllCatalogoByConjunto(Guid idConjunto);
        public Task<List<Catalogo>> GetAllCatalogos();
        public Task<List<Catalogo>> GetCatalogoByNameIdConjunto(string name, Guid idConjunto);
        public Task<List<Catalogo>> GetCatalogoByName(string name);
        public Task<Catalogo> GetCatalogoByCodeIDEmpresa(string code, Guid? idConjunto);
        public Task<List<Catalogo>> GetCatalogsChildsByIDFather(Guid idCodigoPadreCatalgo);
        public Task<List<Catalogo>> GetChildByParentCodeIDEmpresa(string code, Guid idConjunto);
        public Task<List<Catalogo>> GetCatalogsChildsIDConjuntoIDCatalogoPadre(Guid idCodigoPadreCatalgo, Guid idConjunto);
        public Task<List<Catalogo>> GetChildByParentCodeByID_IDEmpresa(Guid idPadre, Guid idConjunto);
        public Task<List<Catalogo>> GetChildByParentCodeByID_Nombre(string nombrePadre);
        public Task<List<Catalogo>> GetCatalogsUpLevel_Conjunto(Guid idCatalogoHijo, Guid idConjunto);
        public Task<List<Catalogo>> GetCatalogsSameLevelByID_Conjunto(Guid idCatalogoHermano, Guid idConjunto);
        public Task<List<Catalogo>> GetAllCatalogosPorNivel(int nivel);
       

        #endregion
    }
}
