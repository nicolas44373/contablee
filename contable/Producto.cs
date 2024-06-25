using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace contable
{
    public class Producto
    {
        private static int nextId = 1;
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Stock { get; set; }
        public decimal Precio { get; set; }
        public int CantidadVendida { get; set; } // Propiedad para la cantidad vendida

        public Producto(string nombre, int stock,int cantidadVendida, decimal precio)
        {
            Id = nextId++;
            Nombre = nombre;
            Stock = stock;
            Precio = precio;
            CantidadVendida = cantidadVendida; 
        }
    }
}
