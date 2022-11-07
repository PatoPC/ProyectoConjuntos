using System;
using System.Collections.Generic;

namespace ConuntosEntidades.Entidades
{
    public partial class Banco
    {
        public Guid IdBancos { get; set; }
        public string NombreBancos { get; set; } = null!;
        public string CtacteBancos { get; set; } = null!;
        public DateTime FechaApBancos { get; set; }
        public int ChequeInicioBancos { get; set; }
        public bool? StatusBancos { get; set; }
    }
}
