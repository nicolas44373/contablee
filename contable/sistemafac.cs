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
using contable;

namespace contable
{
    public partial class sistemafac : Form
    {
        private List<Producto> productos;
        private int nextId;

        public sistemafac()
        {
            InitializeComponent();
            this.Load += sistemafac_Load;
            LoadDataFromFile();

            // Configurar las columnas del DataGridView manualmente
            ConfigurarColumnasDataGridView();
        }

        private void LoadDataFromFile()
        {
            productos = new List<Producto>();

            if (File.Exists("datos.txt"))
            {
                dataGridView1.DataSource = null; // Limpiar el DataSource actual

                using (StreamReader reader = new StreamReader("datos.txt"))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        var data = line.Split(';');
                        // Asegúrate de que los datos se puedan parsear correctamente
                        if (data.Length >= 4)
                        {
                            int id = int.Parse(data[0]);
                            string nombre = data[1];
                            int stock = int.Parse(data[2]);
                            decimal precio = decimal.Parse(data[3]);

                            // Crear una nueva instancia de Producto con los datos obtenidos
                            Producto producto = new Producto(nombre, stock, 0, precio); // CantidadVendida por defecto a 0
                            producto.Id = id; // Asignar el Id después de crear la instancia
                            productos.Add(producto);
                        }
                        else
                        {
                            // Manejar el caso donde los datos no tienen el formato esperado
                            // Puedes mostrar un mensaje de error, omitir la línea, etc.
                            Console.WriteLine($"Error: formato incorrecto en la línea \"{line}\".");
                        }
                    }
                }

                // Asignar la lista de productos al DataSource del DataGridView
                dataGridView1.DataSource = productos;
            }
        }

        private void ConfigurarColumnasDataGridView()
        {
            // Configurar las columnas manualmente si no lo has hecho ya en el diseñador
            dataGridView1.AutoGenerateColumns = false;

            DataGridViewTextBoxColumn colId = new DataGridViewTextBoxColumn();
            colId.DataPropertyName = "Id";
            colId.HeaderText = "ID";
            dataGridView1.Columns.Add(colId);

            DataGridViewTextBoxColumn colNombre = new DataGridViewTextBoxColumn();
            colNombre.DataPropertyName = "Nombre";
            colNombre.HeaderText = "Producto";
            dataGridView1.Columns.Add(colNombre);

            DataGridViewTextBoxColumn colStock = new DataGridViewTextBoxColumn();
            colStock.DataPropertyName = "Stock";
            colStock.HeaderText = "Stock Disponible";
            dataGridView1.Columns.Add(colStock);

            DataGridViewTextBoxColumn colPrecio = new DataGridViewTextBoxColumn();
            colPrecio.DataPropertyName = "Precio";
            colPrecio.HeaderText = "Precio por Unidad";
            dataGridView1.Columns.Add(colPrecio);
        }

        private void btnagregar_Click(object sender, EventArgs e)
        {
            Productoform productoForm = new Productoform();
            if (productoForm.ShowDialog() == DialogResult.OK)
            {
                // Crear una instancia de Producto con el constructor correcto
                Producto producto = new Producto(
                    productoForm.Producto, // Nombre
                    productoForm.Stock,
                    0, // CantidadVendida por defecto a 0
                    productoForm.Precio
                );
                productos.Add(producto);
                GuardarDatos();
            }
        }

        private void GuardarDatos()
        {
            using (StreamWriter writer = new StreamWriter("datos.txt"))
            {
                foreach (Producto producto in productos)
                {
                    writer.WriteLine($"{producto.Id};{producto.Nombre};{producto.Stock};{producto.Precio}");
                }
            }

            // Refrescar el DataGridView
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = productos;
        }

        private void btneliminar_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Obtener el producto seleccionado
                Producto productoSeleccionado = dataGridView1.SelectedRows[0].DataBoundItem as Producto;

                if (productoSeleccionado != null)
                {
                    // Remover el producto de la lista
                    productos.Remove(productoSeleccionado);
                    GuardarDatos(); // Guardar los datos actualizados en el archivo
                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccione una fila para eliminar.");
            }
        }

        private void btnedit_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dataGridView1.SelectedRows[0];
                Productoform productoForm = new Productoform
                {
                    Producto = row.Cells["Nombre"].Value.ToString(),
                    Stock = Convert.ToInt32(row.Cells["Stock"].Value),
                    Precio = Convert.ToDecimal(row.Cells["Precio"].Value)
                };

                if (productoForm.ShowDialog() == DialogResult.OK)
                {
                    row.Cells["Nombre"].Value = productoForm.Producto;
                    row.Cells["Stock"].Value = productoForm.Stock;
                    row.Cells["Precio"].Value = productoForm.Precio;
                    GuardarDatos();
                }
            }
        }

        private void sistemafac_Load(object sender, EventArgs e)
        {
            LoadDataFromFile();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            registro registro = new registro();
            this.Hide();
            registro.Show();
        }

        private void btnVender_Click(object sender, EventArgs e)
        {
            List<Producto> productosSeleccionados = new List<Producto>();

            // Iterar sobre las filas seleccionadas en el DataGridView
            foreach (DataGridViewRow selectedRow in dataGridView1.SelectedRows)
            {
                int productId = Convert.ToInt32(selectedRow.Cells["Id"].Value);

                // Buscar y agregar el producto seleccionado a la lista de productos a vender
                Producto productoSeleccionado = productos.Find(p => p.Id == productId);
                if (productoSeleccionado != null)
                {
                    productosSeleccionados.Add(productoSeleccionado);
                }
            }

            if (productosSeleccionados.Count > 0)
            {
                // Asegúrate de que el constructor de VenderForm espera una lista de Producto correcta
                VenderForm venderForm = new VenderForm(productosSeleccionados);
                this.Hide();
                venderForm.ShowDialog();
                GuardarDatos(); // Guardar datos después de realizar las ventas
            }
            else
            {
                MessageBox.Show("Por favor, seleccione al menos un producto para vender.");
            }
        }

        private void btnActualizarStock_Click(object sender, EventArgs e)
        {
            // Implementa la funcionalidad para actualizar el stock aquí si es necesario
        }
    }
}
