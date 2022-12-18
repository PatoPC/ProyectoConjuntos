using DTOs.Conjunto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Select
{
    public class CustomSelectConjuntos
    {
        public string DataGroupField { set; get; }
        public string DataTextField { set; get; }
        public string DataValueField { set; get; }
        public List<ResultadoBusquedaConjuntos> Items { set; get; }
        public string SelectedValues { set; get; }
    }
}
