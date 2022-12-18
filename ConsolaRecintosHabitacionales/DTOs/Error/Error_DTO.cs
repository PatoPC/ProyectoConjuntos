using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Error
{
    public class Error_DTO
    {
        public Error_DTO()
        {
        }

        public Error_DTO(string nombreControlador, string accion, string mensaje, string jsonObjeto)
        {
            this.nombreControlador = nombreControlador;
            this.accion = accion;
            this.mensaje = mensaje;
            this.jsonObjeto = jsonObjeto;
        }

        public string nombreControlador { set; get; }
        public string accion { set; get; }
        public string mensaje { set; get; }
        public string jsonObjeto { set; get; }

    }
}
