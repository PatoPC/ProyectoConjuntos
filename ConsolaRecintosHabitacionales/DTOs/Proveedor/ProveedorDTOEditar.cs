﻿using DTOs.FacturaProveedor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Proveedor
{
    public class ProveedorDTOEditar
    {
        public Guid? IdConjunto { get; set; }
        public Guid? IdCiudadProveedor { get; set; }
        public Guid? IdTipoContacto { get; set; } = null!;
        public string? NombreProveedor { get; set; } = null!;
        public string? RucProveedor { get; set; } = null!;
        public string? ContactoProveedor { get; set; }
        public string? DirecProveedor { get; set; } = null!;
        public string? TelefonosProveedor { get; set; }
        public string? EMailProveedor { get; set; }
        public decimal? SaldoAntProveedor { get; set; }
        public decimal? SaldoPendProveedor { get; set; }
        public bool StatusProveedor { get; set; }       
        public string? UsuarioModificacion { get; set; }
        public List<FacturaCompraDTOCompleto>? FacturaCompras { get; set; }
    }
}
