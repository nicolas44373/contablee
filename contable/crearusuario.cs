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

namespace contable
{
    public partial class crearusuario : Form
    {
        public crearusuario()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Obtener el usuario y la contraseña ingresados por el usuario
                string nuevoUsuario = txtusuario.Text;
                string nuevaContraseña = txtcontra.Text;

                // Validar que se hayan ingresado datos
                if (string.IsNullOrEmpty(nuevoUsuario) || string.IsNullOrEmpty(nuevaContraseña))
                {
                    MessageBox.Show("Por favor, ingrese un usuario y una contraseña válidos.");
                    return;
                }

                // Verificar si el usuario ya existe
                if (UsuarioExiste(nuevoUsuario))
                {
                    MessageBox.Show("El usuario ya existe.");
                    return;
                }

                // Guardar el nuevo usuario y contraseña en el archivo de texto
                GuardarUsuario(nuevoUsuario, nuevaContraseña);

                MessageBox.Show("Usuario registrado correctamente.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al registrar usuario: {ex.Message}");
            }
        }

        private bool UsuarioExiste(string usuario)
        {
            // Verificar si el usuario ya existe en el archivo de texto
            if (File.Exists("usuarios.txt"))
            {
                string[] lineas = File.ReadAllLines("usuarios.txt");
                foreach (string linea in lineas)
                {
                    if (linea.StartsWith("USUARIO:"))
                    {
                        string nombreUsuario = linea.Substring("USUARIO:".Length).Trim();
                        if (nombreUsuario.Equals(usuario, StringComparison.OrdinalIgnoreCase))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        private void GuardarUsuario(string usuario, string contraseña)
        {
            // Escribir el nuevo usuario y contraseña en el archivo de texto
            using (StreamWriter writer = new StreamWriter("usuarios.txt", true))
            {
                writer.WriteLine("USUARIO: " + usuario);
                writer.WriteLine("CONTRASEÑA: " + contraseña);
            }

            registro registro = new registro();
            this.Hide();
            registro.Show();
        }
    }
}
