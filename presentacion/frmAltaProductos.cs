using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using dominio;
using negocio;

namespace presentacion
{
    public partial class frmAltaProductos : Form
    {
        public frmAltaProductos()
        {
            InitializeComponent();
        }


        private void frmAltaProductos_Load(object sender, EventArgs e)
        {
            ProductoNegocio negocio = new ProductoNegocio();
            try
            {
                dgvAltaProducto.DataSource = negocio.listaAlta();
                ocultarColumnas();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void ocultarColumnas()
        {
            dgvAltaProducto.Columns["Id"].Visible = false;
            dgvAltaProducto.Columns["Descripcion"].Visible = false;
            dgvAltaProducto.Columns["ImagenUrl"].Visible = false;
            dgvAltaProducto.Columns["Precio"].Visible = false;
        }

        

        private void btnCancelarAlta_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnCambiarCodigo_Click(object sender, EventArgs e)
        {
            Producto seleccionado = new Producto();
            seleccionado = (Producto)dgvAltaProducto.CurrentRow.DataBoundItem;
            string codigoAlta = txtCodigoAlta.Text;
            ProductoNegocio negocio = new ProductoNegocio();

            if (!(string.IsNullOrEmpty(txtCodigoAlta.Text.ToUpper())) && codigoAlta.Length <=6)
            {
                negocio.altaProducto(seleccionado, codigoAlta);
                MessageBox.Show("Producto Dado de Alta");
                Close();
                
            }
            else
            {
                MessageBox.Show("Recuerda que el Código es de 6 caracteres maximo");
                return;
            }
            

  
        }
    }
}
