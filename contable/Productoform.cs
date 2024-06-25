using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace contable
{
    public partial class Productoform : Form
    {
        public string Producto { get; set; }
        public int Stock { get; set; }
        public decimal Precio { get; set; }
        public Productoform()
        {
            InitializeComponent();
        }

        private void aceptar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text) || !int.TryParse(txtStockDisponible.Text, out int stock) || !decimal.TryParse(txtPrecioUnitario.Text, out decimal precio))
            {
                MessageBox.Show("Por favor, ingrese valores válidos.");
                return;
            }
            Producto = txtNombre.Text;
            Stock = int.Parse(txtStockDisponible.Text);
            Precio = decimal.Parse(txtPrecioUnitario.Text);
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
