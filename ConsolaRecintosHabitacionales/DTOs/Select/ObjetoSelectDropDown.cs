using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Select
{
    public class ObjetoSelectDropDown
    {
        public ObjetoSelectDropDown()
        {
        }

        public ObjetoSelectDropDown(string texto, string id)
        {
            this.texto = texto;
            this.id = id;
        }

        public string texto { get; set; }
        public string id { get; set; }
    }
}
