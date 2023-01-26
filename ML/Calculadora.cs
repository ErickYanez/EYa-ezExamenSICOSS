using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Calculadora
    {
        public int IdCalculadora { get; set; }
        public string Numero { get; set; }
        public int Resultado { get; set; }
        public string FechaHora { get; set; }
        public List<Object> Calculadoras { get; set; }
        public ML.Usuario Usuario { get; set; }
        public int? Existe { get; set; }
    }
}
