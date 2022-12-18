using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Menu
{
    public class MenuDTOCrear
    {
        public Guid? IdModulo { get; set; }

        public string Nombremenu { get; set; } = null!;

        public string Rutamenu { get; set; } = null!;

        public string? Datoicono { get; set; }
    }
}
