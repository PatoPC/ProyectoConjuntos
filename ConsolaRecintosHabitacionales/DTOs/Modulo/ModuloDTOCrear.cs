﻿using DTOs.Menu;
using DTOs.Roles;
using DTOs.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Modulo
{
    public class ModuloDTOCrear
    {
        public Guid? IdRol { get; set; }

        public string Nombre { get; set; } = null!;

        public string? IconoModulo { get; set; }

        public List<MenuDTOCompleto> Menus { get; set; } = new List<MenuDTOCompleto>();
    }
}
