using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using negocio;
using dominio;
using presentacion;

namespace winformApp
{
    public partial class frmProductos : Form
    {
        private List<Producto> listaProductos;
   

        public frmProductos()
        {
            InitializeComponent();
            
        }

        public void frmProductos_Load(object sender, EventArgs e)
        {
            cargar();   
            try
            {

                cbxCampo.Items.Add("Nombre");
                cbxCampo.Items.Add("Marca");
                cbxCampo.Items.Add("Precio");
            
                cargar();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }


        public void cargar()
        {
           ProductoNegocio producto = new ProductoNegocio();

            try
            {
                listaProductos = producto.listar();
                dgvListaProductos.DataSource = listaProductos;
                ocultarColumnas();

                cargarImagen(listaProductos[0].ImagenUrl);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void ocultarColumnas()
        {
            dgvListaProductos.Columns["Id"].Visible = false;
            dgvListaProductos.Columns["ImagenUrl"].Visible = false;
        }

        public void cargarImagen(string cadena)
        {
            try
            {
                                     
                    pxbProductos.Load(cadena);

               

            }
            catch (Exception ex)
            {

                pxbProductos.Load("https://media.istockphoto.com/id/1147544807/vector/thumbnail-image-vector-graphic.jpg?s=612x612&w=0&k=20&c=rnCKVbdxqkjlcs3xH87-9gocETqpspHFXu5dIGB4wuM=");
            }



        }

        private void dgvListaProductos_SelectionChanged(object sender, EventArgs e)
        {
            try
            {

                if (dgvListaProductos.CurrentRow != null)
                {
                    Producto seleccionado = (Producto)dgvListaProductos.CurrentRow.DataBoundItem;
                    cargarImagen(seleccionado.ImagenUrl);

                }


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            frmAgregarProducto agregar = new frmAgregarProducto();
            agregar.ShowDialog();
            cargar();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            Producto prodcutoModificar = new Producto();
            prodcutoModificar = (Producto)dgvListaProductos.CurrentRow.DataBoundItem;

            try
            {
                frmAgregarProducto modificar = new frmAgregarProducto(prodcutoModificar);
                modificar.ShowDialog();
                cargar();

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
            
        }

        private void txtFiltroRapido_TextChanged(object sender, EventArgs e)
        {
            List<Producto> listaFiltrada = new List<Producto>();
            string filtro = txtFiltroRapido.Text;

            if(filtro.Length <= 3)
            {
                listaFiltrada = listaProductos.FindAll(x => x.Nombre.ToUpper().Contains(filtro.ToUpper()) || x.Descripcion.ToUpper().Contains(filtro.ToUpper()));
            }
            else
            {
                listaFiltrada = listaProductos;
            }

            dgvListaProductos.DataSource = null;
            dgvListaProductos.DataSource = listaFiltrada;

            ocultarColumnas();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            
            eliminar();

        }

        public void eliminar(bool logic= false)
        {
            Producto seleccionado;
            ProductoNegocio negocio = new ProductoNegocio();
            
            try
            {
                DialogResult respuesta = MessageBox.Show("¿Deseas Quitar el Articulo?", "Eliminar Registro", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
              
              if(respuesta == DialogResult.Yes)
                {
                    seleccionado = (Producto)dgvListaProductos.CurrentRow.DataBoundItem;

               if (logic)
                {
                    
                    negocio.eliminarLogico(seleccionado.Id);
                    MessageBox.Show("Producto Oculto");
                }
                else
                {
                    negocio.eliminar(seleccionado.Id);
                    MessageBox.Show("Elimimo el Registro");

                }
             }

                cargar();
                
                
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void btnOcultar_Click(object sender, EventArgs e)
        {
            MessageBox.Show("El registro quedara en la Base de Datos");
            eliminar(true);
        }

        private void cbxCampo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string campo = cbxCampo.SelectedItem.ToString();

            if (campo == "Nombre" || campo == "Marca")
            {
                cboCriterio.Items.Clear();
                cboCriterio.Items.Add("Comienza con:");
                cboCriterio.Items.Add("Termina con:");
                cboCriterio.Items.Add("Contiene:");
            }
            else
            {
                cboCriterio.Items.Clear();
                cboCriterio.Items.Add("Mayor a:");
                cboCriterio.Items.Add("Menor a:");
                cboCriterio.Items.Add("Igual a:");
            }
        }

        private bool verificarFiltro()
        {
            if(cbxCampo.SelectedIndex < 0)
            {
                MessageBox.Show("Debes Seleccionar un Campo de busqueda");
                return true;
            }
            if(cboCriterio.SelectedIndex < 0)
            {
                MessageBox.Show("Selecciona un Criterio de Busqueda");
                return true;
            }

            if (string.IsNullOrEmpty(txtFiltro.Text))
            {
                MessageBox.Show("Debes Completar el Filtro de Busqueda");
                return true;
            }

            if (cbxCampo.SelectedItem.ToString() == "Precio")
            {
               
                if (!(soloNumeros(txtFiltro.Text)))
                {
                    MessageBox.Show("Debes Completar con Números el Filtro si estas Buscando por Precio");
                    return true;
                }
            }

            return false;
        }

        public bool soloNumeros(string texto )
        {
            foreach (char caracterer in texto)
            {
                if(!(char.IsNumber(caracterer)))
                {
                    MessageBox.Show("Debes Cargar con Números solamente el Filtro DB, si has elegido Precio en Campo");
                    return false;
                }
                return true;
            }

            return true;
        }

        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            ProductoNegocio negocio = new ProductoNegocio();
            try
            {
                if (verificarFiltro())
                {
                    return;
                }
                string campo = cbxCampo.SelectedItem.ToString();
                string criterio = cboCriterio.SelectedItem.ToString();
                string filtro = txtFiltro.Text;

                dgvListaProductos.DataSource = negocio.filtrar(campo,criterio,filtro);

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void btnAltaProductos_Click(object sender, EventArgs e)
        {
            frmAltaProductos alta = new frmAltaProductos();
            alta.ShowDialog();
            cargar();
        }
    }

}
