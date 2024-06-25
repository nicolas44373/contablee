using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static contable.sistemafac;
using contable;
using System.Globalization;

namespace contable
{
    public partial class VenderForm : Form
    {
        private List<Producto> productosAVender;

        public List<Producto> ProductosVendidos { get; private set; }

        public VenderForm(List<Producto> productos)
        {
            InitializeComponent();
            this.productosAVender = productos;
            ProductosVendidos = new List<Producto>();
            InitializeDataGridView();
        }

        private void InitializeDataGridView()
        {
            dataGridViewVender.AutoGenerateColumns = false;

            // Columna Producto
            DataGridViewTextBoxColumn productoColumn = new DataGridViewTextBoxColumn();
            productoColumn.DataPropertyName = "Nombre";
            productoColumn.HeaderText = "Producto";
            productoColumn.Name = "Nombre";
            dataGridViewVender.Columns.Add(productoColumn);

            // Columna Stock Disponible
            DataGridViewTextBoxColumn stockColumn = new DataGridViewTextBoxColumn();
            stockColumn.DataPropertyName = "Stock";
            stockColumn.HeaderText = "Stock Disponible";
            stockColumn.Name = "Stock";
            dataGridViewVender.Columns.Add(stockColumn);

            // Columna Precio por Unidad
            DataGridViewTextBoxColumn precioColumn = new DataGridViewTextBoxColumn();
            precioColumn.DataPropertyName = "Precio";
            precioColumn.HeaderText = "Precio por Unidad";
            precioColumn.Name = "Precio";
            precioColumn.DefaultCellStyle.Format = "C2"; // Formato de moneda
            dataGridViewVender.Columns.Add(precioColumn);

            // Columna Cantidad a Vender
            DataGridViewTextBoxColumn cantidadColumn = new DataGridViewTextBoxColumn();
            cantidadColumn.DataPropertyName = "CantidadVender";
            cantidadColumn.HeaderText = "Cantidad a Vender";
            cantidadColumn.Name = "CantidadVender";
            dataGridViewVender.Columns.Add(cantidadColumn);

            dataGridViewVender.DataSource = productosAVender;
        }

        private void btnCalcularTotal_Click(object sender, EventArgs e)
        {
            decimal total = 0;

            foreach (DataGridViewRow row in dataGridViewVender.Rows)
            {
                if (!row.IsNewRow && row.Cells["CantidadVender"].Value != null)
                {
                    int cantidadVendida = Convert.ToInt32(row.Cells["CantidadVender"].Value);
                    decimal precio = Convert.ToDecimal(row.Cells["Precio"].Value);
                    total += cantidadVendida * precio;
                }
            }

            // Aplicar descuentos o recargos según el método de pago
            string metodoPago = comboBoxMetodoPago.SelectedItem.ToString();
            switch (metodoPago)
            {
                case "Efectivo":
                    total *= 0.9m; // Descuento del 10%
                    break;
                case "Transferencia":
                    total *= 0.95m; // Descuento del 5%
                    break;
                case "Tarjeta de Crédito":
                    total *= 1.21m; // Recargo del 21%
                    break;
            }

            textBoxTotal.Text = total.ToString("C2");
        }

        private void btnConfirmarVenta_Click(object sender, EventArgs e)
        {
            decimal totalFactura;
            if (!decimal.TryParse(textBoxTotal.Text, NumberStyles.Currency, CultureInfo.CurrentCulture, out totalFactura))
            {
                MessageBox.Show("El total calculado no es válido. Verifique los datos antes de confirmar la venta.");
                return;
            }

            foreach (DataGridViewRow row in dataGridViewVender.Rows)
            {
                if (!row.IsNewRow && row.Cells["CantidadVender"].Value != null)
                {
                    string nombre = Convert.ToString(row.Cells["Nombre"].Value);
                    decimal precio = Convert.ToDecimal(row.Cells["Precio"].Value);
                    int cantidadVendida = Convert.ToInt32(row.Cells["CantidadVender"].Value);

                    Producto producto = productosAVender.FirstOrDefault(p => p.Nombre == nombre && p.Precio == precio);
                    if (producto != null && cantidadVendida > 0 && cantidadVendida <= producto.Stock)
                    {
                        producto.Stock -= cantidadVendida;
                        ProductosVendidos.Add(new Producto(nombre, producto.Stock, cantidadVendida, precio));
                    }
                    else
                    {
                        MessageBox.Show($"No se puede vender {cantidadVendida} unidades de '{nombre}'. Verifique el stock y la cantidad.");
                        return;
                    }
                }
            }

            if (ProductosVendidos.Any())
            {
                decimal descuentoRecargo = CalcularDescuentoRecargo();
                FacturaForm facturaForm = new FacturaForm(ProductosVendidos, totalFactura, descuentoRecargo);
                facturaForm.ShowDialog();
                MessageBox.Show("Ventas realizadas con éxito.");
                this.DialogResult = DialogResult.OK; // Indicar que la venta fue confirmada con éxito
                this.Close();
            }
            else
            {
                MessageBox.Show("No se ha seleccionado ningún producto para la venta.");
            }
        }

        private decimal CalcularDescuentoRecargo()
        {
            decimal total = 0;

            foreach (DataGridViewRow row in dataGridViewVender.Rows)
            {
                if (!row.IsNewRow && row.Cells["CantidadVender"].Value != null)
                {
                    int cantidadVendida = Convert.ToInt32(row.Cells["CantidadVender"].Value);
                    decimal precio = Convert.ToDecimal(row.Cells["Precio"].Value);
                    total += cantidadVendida * precio;
                }
            }

            // Aplicar descuentos o recargos según el método de pago
            string metodoPago = comboBoxMetodoPago.SelectedItem.ToString();
            decimal descuentoRecargo = 0;

            switch (metodoPago)
            {
                case "Efectivo":
                    descuentoRecargo = total * -0.1m; // Descuento del 10%
                    break;
                case "Transferencia":
                    descuentoRecargo = total * -0.05m; // Descuento del 5%
                    break;
                case "Tarjeta de Crédito":
                    descuentoRecargo = total * 0.21m; // Recargo del 21%
                    break;
            }

            return descuentoRecargo;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("operacion cancelada");
            sistemafac sistemafac = new sistemafac();
            this.Hide();
            sistemafac.ShowDialog();
        }
    }
}
