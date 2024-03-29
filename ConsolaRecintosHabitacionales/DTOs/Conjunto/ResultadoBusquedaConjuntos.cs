﻿using DTOs.Torre;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Conjunto
{
    public class ResultadoBusquedaConjuntos
    {
        public Guid IdConjunto { get; set; }
        public Guid IdUsuario { get; set; }

        public string? NombreConjunto { get; set; } 
        public string? RucConjunto { get; set; }        
        public string? TelefonoConjunto { get; set; } 
        public string? MailConjunto { get; set; }

        public List<TorreDTOCompleto>? Torres { get; set; } = null;

    }
}
