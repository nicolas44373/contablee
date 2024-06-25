using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace contable
{
    public class Producto
    {
        private static int nextId = 1; // Campo estático para generar IDs únicos

        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Stock { get; set; }
        public int CantidadVendida { get; set; }
        public decimal Precio { get; set; }

        public Producto(string nombre, int stock, int cantidadVendida, decimal precio)
        {
            this.Id = nextId++; // Asignar y luego incrementar el ID
            this.Nombre = nombre;
            this.Stock = stock;
            this.CantidadVendida = cantidadVendida;
            this.Precio = precio;
        }
    }
}

