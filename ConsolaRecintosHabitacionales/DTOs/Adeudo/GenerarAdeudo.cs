using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Adeudo
{
	public class GenerarAdeudo

	{
		public Guid? IdDepartamento { get; set; }

		public Guid? IdConjunto { get; set; } = null!;
		public Guid? IdTorre { get; set; } = null!;
		public int anio { get; set; }
		public int mes { get; set; }


		public int tipoGeneracion { get; set; }
		public string? nombrePersona { get; set; }
		public string? numeroDepartamento { get; set; }
		public string? numeroIdentificacion { get; set; }
		public DateTime fechaADeudoActual { get; set; }

		public bool? EstadoPago
		{
			get
			{
				return tipoGeneracion == 1 ? (bool?)true :
				  tipoGeneracion == 2 ? (bool?)false :
				  (bool?)null;
			}
		}

	}
}
