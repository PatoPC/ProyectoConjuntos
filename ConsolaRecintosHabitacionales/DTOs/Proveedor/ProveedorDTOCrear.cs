﻿using DTOs.FacturaProveedor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Proveedor
{
    public class ProveedorDTOCrear
    {
        public Guid? IdConjunto { get; set; }
        public Guid? IdCiudadProveedor { get; set; }
        public string? NombreProveedor { get; set; } = null!;
        public Guid? IdTipoContacto { get; set; } = null!;
        public string? RucProveedor { get; set; } = null!;
        public string? ContactoProveedor { get; set; }
        public string? DirecProveedor { get; set; } = null!;
        public string? TelefonosProveedor { get; set; }
        public string? EMailProveedor { get; set; }
        public decimal? SaldoAntProveedor { get; set; }
        public decimal? SaldoPendProveedor { get; set; }
        public bool StatusProveedor { get; set; }       
        public string? UsuarioCreacion { get; set; } = null!;
    }
}
