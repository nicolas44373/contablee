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
    public partial class registro : Form
    {
        public registro()
        {
            InitializeComponent();
        }

        private void txtuser_Enter(object sender, EventArgs e)
        {
            if (txtuser.Text == "USUARIO")
            {
                txtuser.Text = "";
                txtuser.ForeColor = Color.Black;
            }
        }

        private void txtuser_Leave(object sender, EventArgs e)
        {
            if (txtuser.Text == "")
            {
                txtuser.Text = "USUARIO";
                txtuser.ForeColor = Color.DimGray;
            }
        }

        private void txtcontra_Enter(object sender, EventArgs e)
        {
            if (txtcontra.Text == "CONTRASEÑA")
            {
                txtcontra.Text = "";
                txtcontra.ForeColor = Color.LightGray;
                txtcontra.UseSystemPasswordChar = true;
            }
        }

        private void txtcontra_Leave(object sender, EventArgs e)
        {
            if (txtcontra.Text == "")
            {
                txtcontra.Text = "CONTRASEÑA";
                txtcontra.ForeColor = Color.DimGray;

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Obtener el usuario y la contraseña ingresados por el usuario
                string usuario = txtuser.Text;
                string contraseña = txtcontra.Text;

                // Verificar si el usuario y la contraseña coinciden con alguna cuenta
                if (VerificarCredenciales(usuario, contraseña))
                {
                    MessageBox.Show("Inicio de sesión exitoso.");
                    // Realizar acciones adicionales después de iniciar sesión...
                    sistemafac sistemafac = new sistemafac();
                    this.Hide();
                    sistemafac.Show();
                }
                else
                {
                    MessageBox.Show("Usuario o contraseña incorrectos.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al iniciar sesión: {ex.Message}");
            }
            
            
        }

        private bool VerificarCredenciales(string usuario, string contraseña)
        {
            // Verificar si el usuario y la contraseña coinciden con alguna cuenta en el archivo de texto
            if (File.Exists("usuarios.txt"))
            {
                string[] lineas = File.ReadAllLines("usuarios.txt");
                bool usuarioEncontrado = false;
                foreach (string linea in lineas)
                {
                    if (linea.StartsWith("USUARIO:"))
                    {
                        string nombreUsuario = linea.Substring("USUARIO:".Length).Trim();
                        if (nombreUsuario.Equals(usuario, StringComparison.OrdinalIgnoreCase))
                        {
                            usuarioEncontrado = true;
                        }
                    }
                    else if (linea.StartsWith("CONTRASEÑA:") && usuarioEncontrado)
                    {
                        string contraseñaGuardada = linea.Substring("CONTRASEÑA:".Length).Trim();
                        if (contraseñaGuardada.Equals(contraseña))
                        {
                            return true;
                        }
                        else
                        {
                            return false; // Contraseña incorrecta
                        }
                    }
                }
            }
            return false; // Usuario no encontrado

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
           this.WindowState= FormWindowState.Minimized;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            crearusuario crearusuario = new crearusuario();
            crearusuario.Show();
        }
    }
}
