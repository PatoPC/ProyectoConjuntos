using DTOs.AreasDepartamento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Conjunto
{
    public class ModeloArchivoConjunto
    {
        public string? Nombre_Conjunto { get; set; } = null;
        public string? RUC { get; set; } = null;
        public string? Correo_Conjunto { get; set; } = null;
        public string? Telefono { get; set; } = null;
        public string? Dirección { get; set; } = null;
        public string? Torre { get; set; } = null;
        public string? Departamento { get; set; } = null;
        public decimal? Metros_Cuadrados { get; set; } = null;
        public decimal? Valor_Alicuota { get; set; } = null;
        public decimal? Saldo_Inicial { get; set; } = null;
        public string? Tipo_Identificacion_Condomino { get; set; } = null;
        public string? Numero_Identificacion_Condomino { get; set; } = null;
        public string? Nombre_Condomino { get; set; } = null;
        public string? Apellido_Condomino { get; set; } = null;
        public string? Telefono_Condomino { get; set; } = null;
        public string? Celular_Condomino { get; set; } = null;
        public string? Correo_Condomino { get; set; } = null;
        public string? Observación_Condomino { get; set; } = null;
        public string? Tipo_Identificacion_Propietario { get; set; } = null;
        public string? Numero_Identificacion_Propietario { get; set; } = null;
        public string? Nombre_Propietario { get; set; } = null;
        public string? Apellido_Propietario { get; set; } = null;
        public string? Telefono_Propietario { get; set; } = null;
        public string? Celular_Propietario { get; set; } = null;
        public string? Correo_Propietario { get; set; } = null;
        public string? Observacion_Propietario { get; set; } = null;
        public string? listaAreasDepartamentos { get; set; } = null;

        //public List<AreasDepartamentoDTO>? AreasDepartamentos { get; set; }
    }
}
