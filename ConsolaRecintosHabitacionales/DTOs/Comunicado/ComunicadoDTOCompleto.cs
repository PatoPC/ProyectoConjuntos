using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Comunicado
{
    public class ComunicadoDTOCompleto
    {
        public Guid IdComunicado { get; set; }
        public Guid IdConjunto { get; set; }
        public string NombreConjunto { get; set; } = null!;
        public string Titulo { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public bool Estado { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string? UsuarioModificacion { get; set; }

        public virtual double DiasParaCaducar
        {
            get
            {
                DateTime fechaActual = DateTime.Now;

                fechaActual = fechaActual.AddHours(-fechaActual.Hour);
                fechaActual = fechaActual.AddMinutes(-fechaActual.Minute);
                fechaActual = fechaActual.AddSeconds(-fechaActual.Second);
                int cantidadDias = Convert.ToInt32((FechaFin - fechaActual).TotalDays);
                return cantidadDias;

            }
        }

        public virtual double DiasAntiguedad
        {
            get
            {
                DateTime fechaActual = DateTime.Now;

                fechaActual = fechaActual.AddHours(-fechaActual.Hour);
                fechaActual = fechaActual.AddMinutes(-fechaActual.Minute);
                fechaActual = fechaActual.AddSeconds(-fechaActual.Second);
                int cantidadDiasAntiguedad = Convert.ToInt32((fechaActual - FechaInicio).TotalDays);
                return cantidadDiasAntiguedad;

            }
        }
    }
}
