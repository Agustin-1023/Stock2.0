using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock2._0
{
    public class Repuestos
    {
        public int id { get; set; }

        public int id_repuesto { get; set; }

        public string tipo { get; set; }

        public string descripcion { get; set; }

        public string estado { get; set; }

        public string codigo { get; set; }

        public int cantidad { get; set; }

        public int id_modulo { get; set; }

        public int id_equipo { get; set; }

    }
}
