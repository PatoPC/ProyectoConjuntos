using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilitarios
{
    public class MensajesRespuesta
    {
        public bool IsSuccess { get; set; }
        public Guid orderId { get; set; }
        public string message { get; set; }
        public string icon { get; set; }
        public bool gifAnimado { get; set; }
        public string state { get; set; }
        public string email { get; set; }
        public string aspirante { get; set; }
        public int status { get; set; }
        public string title { get; set; }
        public Guid? IdPersona { get; set; }
        public string urlRetorno { get; set; }

        private const string mensajeObjetosNulos = "No se puede crear este elemento porque no contiene información.";
        private const string mensajeGuardarError = "Ocurrió un error inesperado al intentar guardar.";
        public const string mensajeBusquedaSinResultados = "No se encontraron resultados para su búsqueda.";
        private const string mensajeErrorConexion = "El servidor tardó mucho en responder, por favor revise su conexión a internet.";
        private const string mensajeEliminadoOk = "Sus datos han sido eliminados correctamente, esta acción no se puede deshacer.";
        private const string mensajeGuardarOk = "Sus datos han sido guardados correctamente";
        private const string mensajeErrorInesperado = "Ocurrió un error inesperado al intentar al procesar la información.";
        public MensajesRespuesta(string mensaje, bool bandera, string state, string icon, string urlRetorno)
        {
            this.message = mensaje;
            IsSuccess = bandera;
            this.state = state;
            this.icon = icon;
            this.urlRetorno = urlRetorno;
        }

        public MensajesRespuesta(string mensaje, bool bandera, string state, string icon)
        {
            this.message = mensaje;
            IsSuccess = bandera;
            this.state = state;
            this.icon = icon;
        }
        public MensajesRespuesta(string mensaje, bool bandera, string state, string icon, bool gifAnimado)
        {
            this.message = mensaje;
            IsSuccess = bandera;
            this.state = state;
            this.icon = icon;
            this.gifAnimado = gifAnimado;
        }

        public MensajesRespuesta(string mensaje, bool bandera, string state, string icon, Guid? IdPersona, string aspirante)
        {
            this.message = mensaje;
            IsSuccess = bandera;
            this.state = state;
            this.icon = icon;
            this.IdPersona = IdPersona;
            this.aspirante = aspirante;
        }


        public MensajesRespuesta(string mensaje, bool bandera, string state, string icon, int status, string title)
        {
            this.message = mensaje;
            IsSuccess = bandera;
            this.state = state;
            this.icon = icon;
            this.status = status;
            this.title = title;
        }

        public MensajesRespuesta(string mensaje, string? correo = "")
        {
            message = mensaje;
            email = correo;
        }


        public MensajesRespuesta()
        {

        }
        public static MensajesRespuesta errorMensajePersonalizado(string mensaje)
        {
            return new MensajesRespuesta(mensaje, false, "¡error!", "error");
        }

        public static MensajesRespuesta noSePermiteObjNulos()
        {
            return new MensajesRespuesta(mensajeObjetosNulos, false, "¡error!", "error");
        }

        public static MensajesRespuesta guardarError()
        {
            return new MensajesRespuesta(mensajeGuardarError, false, "¡error!", "error");
        }
        public static MensajesRespuesta sinResultados()
        {
            return new MensajesRespuesta(mensajeBusquedaSinResultados, true, "Exitoso!", "info", 404, "Not Found");
        }

        public static MensajesRespuesta errorConexion()
        {
            return new MensajesRespuesta(mensajeErrorConexion, false, "¡Error!", "error");
        }

        public static MensajesRespuesta guardarOK(string controlador, string accion)
        {
            return new MensajesRespuesta(mensajeGuardarOk, true, "Exitoso!", "success", ConstantesAplicacion.pathConsola + "/" + controlador + "/" + accion);
        }

        public static MensajesRespuesta guardarOK()
        {
            return new MensajesRespuesta(mensajeGuardarOk, true, "Exitoso!", "success");
        }

        public static MensajesRespuesta eliminadoOK()
        {
            return new MensajesRespuesta(mensajeEliminadoOk, true, "Exitoso!", "info");
        }

        public static MensajesRespuesta eliminadoOK(string controlador, string accion)
        {
            return new MensajesRespuesta(mensajeEliminadoOk, true, "Exitoso!", "info", ConstantesAplicacion.pathConsola + "/" + controlador + "/" + accion);
        }

        public static MensajesRespuesta errorInesperado()
        {
            return new MensajesRespuesta(mensajeErrorInesperado, false, "¡error!", "error");
        }
    }
}
