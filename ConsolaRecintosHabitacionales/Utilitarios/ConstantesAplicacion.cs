using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilitarios
{
    public class ConstantesAplicacion
    {

        ////DESARROLLO
        //public const string pathConsola = "";
        //public const string pathAPI = "";
        //public const string urlConsola = "http://localhost:5067";

        ////PRODUCCION
        public const string pathConsola = "/Consola_Conjuntos";
        public const string pathAPI = "/API_Conjuntos";
        //public const string urlConsola = "http://181.39.23.39";

        public const string urlConsola = "http://181.39.23.39/Consola_Conjuntos/";

        //http://181.39.23.39/API_Conjuntos/api/API_Conjuntos/ObtenerConjutosAvanzado


        public static Guid guidNulo = new Guid(); /// "00000000-0000-0000-0000-000000000000"
        public const string nombreSesion = "datosSesion";


        #region Códigos Persona
        public const string padreTipoIdentificacion = "TIPIDENT";
        #endregion

        #region Tipo Identificación
        public const string IdentificacionCedula = "Cédula";
        public const string IdentificacionPasaporte = "Pasaporte";
        #endregion

        #region CódigoCatalogos
        public const string padreModulo = "MDLS";
        public const string padrePermisos = "TPPMS";
        public const string padrePaginasRoles = "HOMEINI";
        public const string padreTipoPersona = "tipPerso";
        #endregion

    }
}
