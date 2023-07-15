using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Models
{
    public class ProductoCompra
    {
        public int? Idproductos {set; get; }
        public  string Titulo { set; get; }
        public string Descripcion { set; get; }
        public decimal PrecioUnitario { set; get; }
        public int Cantidad { set; get; }
        public decimal Subtotal => PrecioUnitario * Cantidad;

    }
}
