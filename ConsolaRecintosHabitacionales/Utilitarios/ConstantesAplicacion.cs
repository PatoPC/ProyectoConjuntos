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
        public const string pathConsola = "";
        public const string pathAPI = "";
        public const string urlConsola = "http://localhost:5067";

        ////PRODUCCION
        //public const string pathConsola = "/Consola_Conjuntos";
        //public const string pathAPI = "/API_Conjuntos";
        //public const string urlConsola = "http://181.39.23.39";

        //public const string urlConsola = "http://181.39.23.39/Consola_Conjuntos/";

        //http://181.39.23.39/API_Conjuntos/api/API_Conjuntos/ObtenerConjutosAvanzado


        public static Guid guidNulo = new Guid(); /// "00000000-0000-0000-0000-000000000000"
        public const string nombreSesion = "datosSesion";


        #region Códigos Persona
        public const string padreTipoIdentificacion = "TPIDENT";
        public const string padrePais = "ECUADR";
		#endregion
		
		#region Códigos Proveedor
		
        #endregion

        #region Tipo Identificación
        public const string IdentificacionCedula = "Cédula";
        public const string CodigoCatalogoCedula = "CEDULA";
        public const string IdentificacionPasaporte = "Pasaporte";
        public const string CedulaNoValida = "Cédula no válida";

        #endregion

        #region Código Catalogos
        public const string padreModulo = "MDLS";
        public const string padrePermisos = "TPPMS";
        public const string padrePaginasRoles = "HOMEINI";
        public const string padreTipoPersona = "TIPPERSO";
        public const string tipoPersonaCondomino = "OPCINQUI";
        public const string tipoPersonaPropietario = "MDUEÑO";
        public const string tipoFormaPago = "FRPGO";
        public const string tipoBancoFormaPago = "TPRECIB";

        //Tipos Cabecera Tipo Transacción
        public const string tipoTransaccion = "PTTCOTP";


        //Ingreso Adeudos
        public const string ingresoAdeudos = "TPINGAD";
        #endregion

        #region Ciudades
        public const string padreCiudades = "ECUADR";
        #endregion


        #region MÓDULOS CONTABLES
        public const string padreModulosContables = "MDLSCONT"; //Padre

        public const string adeudoModulosContables = "MDLSADUD";
        //public const string padreModulosContables = "MDLSCONT";
        #endregion

        public const string mensajeSesionCaducada = "?sesionCaducada=Tu sesión ha caducado";

        public const string rutaArchivosLectura = @"ArchivosConjuntos\";
        public const string nombreRolCondominos = "Condomino";
        public const string nombrePadreParam = "PARAM";


        #region Tipo Areas
        public const string padreTipoAreas = "TIPAREAS";
        #endregion
    }
}
