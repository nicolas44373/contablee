using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static contable.sistemafac;

namespace contable
{
    public partial class FacturaForm : Form
    {
        private List<Producto> productosAVender;
        private decimal totalFactura;
        private decimal descuentoRecargo;
        private PrintDocument printDocumentFactura;
        private PrintDialog printDialogFactura;
        private string numeroFactura;
        private const string archivoFactura = "factura.txt";

        public FacturaForm(List<Producto> productos, decimal total, decimal descuentoRecargo)
        {
            InitializeComponent();
            this.productosAVender = productos;
            this.totalFactura = total;
            this.descuentoRecargo = descuentoRecargo;
            this.printDocumentFactura = new PrintDocument();
            this.printDialogFactura = new PrintDialog();

            // Asignar el evento PrintPage al documento de impresión
            this.printDocumentFactura.PrintPage += new PrintPageEventHandler(printDocumentFactura_PrintPage);

            // Leer el último número de factura
            numeroFactura = LeerNumeroFactura();
            ConfigurarFactura();
        }

        private void ConfigurarFactura()
        {
            txtNumeroFactura.Text = numeroFactura;
            dtpFecha.Value = DateTime.Now;

            dataGridViewDetalles.AutoGenerateColumns = false;
            dataGridViewDetalles.DataSource = productosAVender;

            ConfigurarColumnasDataGridView();

            txtTotal.Text = totalFactura.ToString("C2");
            txtDescuentoRecargo.Text = descuentoRecargo.ToString("C2");
        }

        private string LeerNumeroFactura()
        {
            try
            {
                if (!File.Exists(archivoFactura))
                {
                    File.WriteAllText(archivoFactura, "0"); // Crear el archivo con el valor inicial si no existe
                }

                string lastNumber = File.ReadAllText(archivoFactura);
                int numero;

                if (!int.TryParse(lastNumber, out numero))
                {
                    MessageBox.Show("El contenido del archivo de número de factura no es válido. Se reiniciará a 0.");
                    numero = 0;
                }

                numero++; // Incrementar el número de factura
                File.WriteAllText(archivoFactura, numero.ToString()); // Guardar el nuevo número

                return $"FAC-{numero:D3}"; // Formatear el número de factura con ceros a la izquierda
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error al leer el número de factura: {ex.Message}");
                return "FAC-000";
            }
        }

        private void ConfigurarColumnasDataGridView()
        {
            dataGridViewDetalles.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Nombre",
                HeaderText = "Producto",
                Name = "Nombre",
                ReadOnly = true
            });

            dataGridViewDetalles.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "CantidadVendida",
                HeaderText = "Cantidad",
                Name = "CantidadVendida",
                ReadOnly = true
            });

            dataGridViewDetalles.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Precio",
                HeaderText = "Precio Unitario",
                Name = "Precio",
                ReadOnly = true,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "C2" }
            });
        }

        private void printDocumentFactura_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics graphics = e.Graphics;
            Font font = new Font("Arial", 12);

            float fontHeight = font.GetHeight();
            int startX = 10;
            int startY = 10;
            int offset = 40;

            graphics.DrawString("Factura de Venta", new Font("Arial", 24, FontStyle.Bold), Brushes.Black, startX, startY);

            string facturaInfo = $"Número de Factura: {txtNumeroFactura.Text}\nFecha: {dtpFecha.Value.ToShortDateString()}";
            graphics.DrawString(facturaInfo, font, Brushes.Black, startX, startY + offset);

            offset = offset + 40;
            foreach (Producto producto in productosAVender)
            {
                string productoInfo = $"{producto.Nombre} - Cantidad: {producto.CantidadVendida} - Precio Unitario: {producto.Precio.ToString("C2")}";
                graphics.DrawString(productoInfo, font, Brushes.Black, startX, startY + offset);
                offset = offset + (int)fontHeight + 5;
            }

            offset = offset + 20;
            string descuentoRecargoInfo = $"Descuento/Recargo: {descuentoRecargo.ToString("C2")}";
            graphics.DrawString(descuentoRecargoInfo, font, Brushes.Black, startX, startY + offset);

            offset = offset + 20;
            string totalInfo = $"Total: {totalFactura.ToString("C2")}";
            graphics.DrawString(totalInfo, new Font("Arial", 16, FontStyle.Bold), Brushes.Black, startX, startY + offset);
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            printDialogFactura.Document = printDocumentFactura;

            if (printDialogFactura.ShowDialog() == DialogResult.OK)
            {
                printDocumentFactura.Print();
                
            }
            this.Close();
        }

        private void txtTotal_TextChanged(object sender, EventArgs e)
        {
            // Implementa aquí la lógica que deseas ejecutar cuando el texto en txtTotal cambie
        }

        private void button1_Click(object sender, EventArgs e)
        {
            sistemafac sistemafac = new sistemafac();
            this.Hide();
           sistemafac.Show();
        }
    }
}   
