using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmaciaC.COMMON
{
    public class Producto
    {
        public string NombreProducto { get; set; }
        public double PrecioCompra { get; set; }
        public string Presentacion { get; set; }
        public string Descripcion { get; set; }
        public double PrecioVenta { get; set; }
        public string Categoria { get; set; }
    }
}
