namespace contable
{
    partial class Productoform
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.producto = new System.Windows.Forms.Label();
            this.stock = new System.Windows.Forms.Label();
            this.preciouni = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.txtStockDisponible = new System.Windows.Forms.TextBox();
            this.txtPrecioUnitario = new System.Windows.Forms.TextBox();
            this.aceptar = new System.Windows.Forms.Button();
            this.cancelar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // producto
            // 
            this.producto.AutoSize = true;
            this.producto.Font = new System.Drawing.Font("Sans Serif Collection", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.producto.Location = new System.Drawing.Point(12, 64);
            this.producto.Name = "producto";
            this.producto.Size = new System.Drawing.Size(188, 32);
            this.producto.TabIndex = 5;
            this.producto.Text = "ingrese producto";
            // 
            // stock
            // 
            this.stock.AutoSize = true;
            this.stock.Font = new System.Drawing.Font("Sans Serif Collection", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stock.Location = new System.Drawing.Point(18, 106);
            this.stock.Name = "stock";
            this.stock.Size = new System.Drawing.Size(182, 32);
            this.stock.TabIndex = 6;
            this.stock.Text = "stock disponible";
            // 
            // preciouni
            // 
            this.preciouni.AutoSize = true;
            this.preciouni.Font = new System.Drawing.Font("Sans Serif Collection", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.preciouni.Location = new System.Drawing.Point(8, 148);
            this.preciouni.Name = "preciouni";
            this.preciouni.Size = new System.Drawing.Size(192, 32);
            this.preciouni.TabIndex = 7;
            this.preciouni.Text = "precio por unidad";
            // 
            // txtNombre
            // 
            this.txtNombre.Location = new System.Drawing.Point(206, 71);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(317, 20);
            this.txtNombre.TabIndex = 8;
            // 
            // txtStockDisponible
            // 
            this.txtStockDisponible.Location = new System.Drawing.Point(206, 113);
            this.txtStockDisponible.Name = "txtStockDisponible";
            this.txtStockDisponible.Size = new System.Drawing.Size(123, 20);
            this.txtStockDisponible.TabIndex = 9;
            // 
            // txtPrecioUnitario
            // 
            this.txtPrecioUnitario.Location = new System.Drawing.Point(206, 155);
            this.txtPrecioUnitario.Name = "txtPrecioUnitario";
            this.txtPrecioUnitario.Size = new System.Drawing.Size(123, 20);
            this.txtPrecioUnitario.TabIndex = 10;
            // 
            // aceptar
            // 
            this.aceptar.BackColor = System.Drawing.Color.Lime;
            this.aceptar.Location = new System.Drawing.Point(178, 203);
            this.aceptar.Name = "aceptar";
            this.aceptar.Size = new System.Drawing.Size(118, 34);
            this.aceptar.TabIndex = 11;
            this.aceptar.Text = "aceptar";
            this.aceptar.UseVisualStyleBackColor = false;
            this.aceptar.Click += new System.EventHandler(this.aceptar_Click);
            // 
            // cancelar
            // 
            this.cancelar.BackColor = System.Drawing.Color.Red;
            this.cancelar.Location = new System.Drawing.Point(342, 203);
            this.cancelar.Name = "cancelar";
            this.cancelar.Size = new System.Drawing.Size(118, 34);
            this.cancelar.TabIndex = 12;
            this.cancelar.Text = "cancelar";
            this.cancelar.UseVisualStyleBackColor = false;
            // 
            // Productoform
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(549, 264);
            this.Controls.Add(this.cancelar);
            this.Controls.Add(this.aceptar);
            this.Controls.Add(this.txtPrecioUnitario);
            this.Controls.Add(this.txtStockDisponible);
            this.Controls.Add(this.txtNombre);
            this.Controls.Add(this.preciouni);
            this.Controls.Add(this.stock);
            this.Controls.Add(this.producto);
            this.Name = "Productoform";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Productoform";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label producto;
        private System.Windows.Forms.Label stock;
        private System.Windows.Forms.Label preciouni;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.TextBox txtStockDisponible;
        private System.Windows.Forms.TextBox txtPrecioUnitario;
        private System.Windows.Forms.Button aceptar;
        private System.Windows.Forms.Button cancelar;
    }
}