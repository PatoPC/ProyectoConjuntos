using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilitarios
{
    public class ConstantesConsumoAPI
    {

        #region Conjutos
        public const string crearConjuto = ConstantesAplicacion.pathAPI + "/api/API_Conjuntos/";
        public const string buscarConjuntosAvanzado = ConstantesAplicacion.pathAPI + "/api/API_Conjuntos/ObtenerConjutosAvanzado";
        public const string buscarConjuntosPorID = ConstantesAplicacion.pathAPI + "/api/API_Conjuntos/";
        public const string EditarConjuntosPorID = ConstantesAplicacion.pathAPI + "/api/API_Conjuntos/Editar?id=";
        public const string EditarConjuntosEliminar = ConstantesAplicacion.pathAPI + "/api/API_Conjuntos/Eliminar?id=";
        #endregion

        #region Torre
        public const string GestionarTorres = ConstantesAplicacion.pathAPI + "/api/API_Torre/";
        public const string buscarTorresAvanzado = ConstantesAplicacion.pathAPI + "/api/API_Torre/ObtenerTorresAvanzado";
        public const string TorresPorIDEditar = ConstantesAplicacion.pathAPI + "/api/API_Torre/Editar?id=";
        public const string TorresPorIDEliminar = ConstantesAplicacion.pathAPI + "/api/API_Torre/Eliminar?id=";
        #endregion

        #region Departamento
        public const string gestionarDepartamentoAPI = ConstantesAplicacion.pathAPI + "/api/API_Departamento/";
        public const string gestionarDepartamentoAPIEditar = ConstantesAplicacion.pathAPI + "/api/API_Departamento/Editar?id=";
        public const string gestionarDepartamentoAPIEliminar = ConstantesAplicacion.pathAPI + "/api/API_Departamento/Eliminar?id=";
        public const string buscarDepartamentoAvanzado = ConstantesAplicacion.pathAPI + "/api/ API_Departamento/ObtenerTorresAvanzado";
      
        #endregion


    }
}
