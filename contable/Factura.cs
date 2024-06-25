using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace contable
{
    public class Factura
    {
        public string NumeroFactura { get; set; }
        public DateTime Fecha { get; set; }
        public List<Producto> Productos { get; set; }
        public decimal Total { get; set; }
        public int CantidadVendida { get; set; }

        public Factura(string numeroFactura, DateTime fecha, List<Producto> productos, decimal total)
        {
            NumeroFactura = numeroFactura;
            Fecha = fecha;
            Productos = productos;
            Total = total;
            CantidadVendida = 0;
        }

    }
}
