namespace presentacion
{
    partial class frmAltaProductos
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
            this.dgvAltaProducto = new System.Windows.Forms.DataGridView();
            this.btnCambiarCodigo = new System.Windows.Forms.Button();
            this.btnCancelarAlta = new System.Windows.Forms.Button();
            this.lblIndicacion = new System.Windows.Forms.Label();
            this.txtCodigoAlta = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAltaProducto)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvAltaProducto
            // 
            this.dgvAltaProducto.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAltaProducto.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvAltaProducto.Location = new System.Drawing.Point(68, 12);
            this.dgvAltaProducto.MultiSelect = false;
            this.dgvAltaProducto.Name = "dgvAltaProducto";
            this.dgvAltaProducto.ReadOnly = true;
            this.dgvAltaProducto.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAltaProducto.Size = new System.Drawing.Size(620, 241);
            this.dgvAltaProducto.TabIndex = 0;
            // 
            // btnCambiarCodigo
            // 
            this.btnCambiarCodigo.Location = new System.Drawing.Point(300, 354);
            this.btnCambiarCodigo.Name = "btnCambiarCodigo";
            this.btnCambiarCodigo.Size = new System.Drawing.Size(147, 23);
            this.btnCambiarCodigo.TabIndex = 1;
            this.btnCambiarCodigo.Text = "Cambiar Codigo";
            this.btnCambiarCodigo.UseVisualStyleBackColor = true;
            this.btnCambiarCodigo.Click += new System.EventHandler(this.btnCambiarCodigo_Click);
            // 
            // btnCancelarAlta
            // 
            this.btnCancelarAlta.Location = new System.Drawing.Point(332, 411);
            this.btnCancelarAlta.Name = "btnCancelarAlta";
            this.btnCancelarAlta.Size = new System.Drawing.Size(90, 27);
            this.btnCancelarAlta.TabIndex = 2;
            this.btnCancelarAlta.Text = "Cancelar";
            this.btnCancelarAlta.UseVisualStyleBackColor = true;
            this.btnCancelarAlta.Click += new System.EventHandler(this.btnCancelarAlta_Click);
            // 
            // lblIndicacion
            // 
            this.lblIndicacion.AutoSize = true;
            this.lblIndicacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIndicacion.Location = new System.Drawing.Point(148, 267);
            this.lblIndicacion.Name = "lblIndicacion";
            this.lblIndicacion.Size = new System.Drawing.Size(461, 18);
            this.lblIndicacion.TabIndex = 3;
            this.lblIndicacion.Text = "INGRESA EL CODIGO DEL ARTICULO PARA DAR DE ALTA";
            // 
            // txtCodigoAlta
            // 
            this.txtCodigoAlta.Location = new System.Drawing.Point(322, 309);
            this.txtCodigoAlta.Name = "txtCodigoAlta";
            this.txtCodigoAlta.Size = new System.Drawing.Size(100, 20);
            this.txtCodigoAlta.TabIndex = 4;
            // 
            // frmAltaProductos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.txtCodigoAlta);
            this.Controls.Add(this.lblIndicacion);
            this.Controls.Add(this.btnCancelarAlta);
            this.Controls.Add(this.btnCambiarCodigo);
            this.Controls.Add(this.dgvAltaProducto);
            this.Name = "frmAltaProductos";
            this.Text = "frmAltaProductos";
            this.Load += new System.EventHandler(this.frmAltaProductos_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAltaProducto)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvAltaProducto;
        private System.Windows.Forms.Button btnCambiarCodigo;
        private System.Windows.Forms.Button btnCancelarAlta;
        private System.Windows.Forms.Label lblIndicacion;
        private System.Windows.Forms.TextBox txtCodigoAlta;
    }
}