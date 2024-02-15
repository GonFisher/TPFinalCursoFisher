using negocio;
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
using System.IO;
using System.Configuration;

namespace negocio
{
    public partial class frmAgregarProducto : Form
    {
        private Producto producto = null;
        private OpenFileDialog archivo = null;


        public frmAgregarProducto()
        {
            InitializeComponent();

           
        } 
        
        public frmAgregarProducto(Producto producto)
        {
            InitializeComponent();
            Text = "Modificar Producto";
            this.producto = producto; 

           
        }



        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();

        }


        public void cargarImagen(string imagen)
        {
            try
            {
                pbxProductosAgregar.Load(imagen);

            }
            catch (Exception ex)
            {

                pbxProductosAgregar.Load("https://media.istockphoto.com/id/1147544807/vector/thumbnail-image-vector-graphic.jpg?s=612x612&w=0&k=20&c=rnCKVbdxqkjlcs3xH87-9gocETqpspHFXu5dIGB4wuM=");
            }


        }

       

        private void frmAgregarProducto_Load_1(object sender, EventArgs e)
        {
            MarcaNegocio marcas = new MarcaNegocio();
            CategoriaNegocio categorias = new CategoriaNegocio();

            

           

            try
            {
                cboMarca.DataSource = marcas.listar();
                cboMarca.ValueMember = "Id";
                cboMarca.DisplayMember = "Descripcion";

                cboCategoria.DataSource = categorias.listar();
                cboCategoria.ValueMember = "Id";
                cboCategoria.DisplayMember = "Descripcion";

                if (producto != null)
                {
                    txtCodigo.Text = producto.Codigo;
                    txtNombre.Text = producto.Nombre;
                    txtDescripcion.Text = producto.Descripcion;
                    cboMarca.SelectedValue = producto.Marca.Id;
                    cboCategoria.SelectedValue = producto.Categoria.Id;
                    cargarImagen(producto.ImagenUrl);
                    txtImagen.Text = producto.ImagenUrl;
                    txtPrecio.Text = producto.Precio.ToString();

                }

               

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void txtImagen_Leave(object sender, EventArgs e)
        {
            cargarImagen(txtImagen.Text);
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {

           
            ProductoNegocio negocio = new ProductoNegocio();
            
            

            try
            {
                if (producto == null)
                    producto = new Producto();

                if (verificarCeledas())
                    return;


                producto.Codigo = txtCodigo.Text;
                producto.Nombre = txtNombre.Text;
                producto.Descripcion = txtDescripcion.Text;
                producto.ImagenUrl = txtImagen.Text;  
                producto.Marca = (Marca)cboMarca.SelectedItem;
                producto.Categoria = (Categoria)cboCategoria.SelectedItem;
                producto.Precio = decimal.Parse(txtPrecio.Text);

               
                
               
          

                if (producto.Id != 0)
                {
                    negocio.modificar(producto);
                    MessageBox.Show("Modificación Exitosa");
                }
                else
                {
                    DialogResult respuesta = MessageBox.Show("Los Desplegables estan predeterminados¿ Los Contolaste?", "Control de Campos", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                      if (respuesta == DialogResult.Yes)
                        {
                            negocio.agregar(producto);
                            MessageBox.Show("Producto Agregado Exitosamente");
                        }
                       else
                        {
                           MessageBox.Show("Selecciona los Correctos y Continua");
                            return;
                         }

                }
                
                if(archivo != null && !(txtImagen.Text.ToUpper().Contains("HTTP")))
                {
                    File.Copy(archivo.FileName, ConfigurationManager.AppSettings["images-folder"] + archivo.SafeFileName);
                }
              

                Close();

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }

          
        }

        private void button1_Click(object sender, EventArgs e)
        {
            archivo = new OpenFileDialog();
            archivo.Filter = "jpg|*.jpg;|png|*png;|webp|*.webp";

            if(archivo.ShowDialog() == DialogResult.OK)
            {
                txtImagen.Text = archivo.FileName;
                cargarImagen(archivo.FileName);
            }
        }

        private bool verificarCeledas()
        {
            if (string.IsNullOrEmpty(txtCodigo.Text))
            {
                MessageBox.Show("Debes Ingeresar el Codigo");
                return true;
            }
            if (string.IsNullOrEmpty(txtNombre.Text))
            {
                MessageBox.Show("Debes Ingresar el Nombre");
                return true;
            }
                    
            if (string.IsNullOrEmpty(txtDescripcion.Text))
            {
                MessageBox.Show("Debes Ingresar la Descripcion");
                return true;
            }

            if(txtDescripcion.TextLength > 50)
            {
                MessageBox.Show("Tu Descripcion supero los 50 caracteres");
                return true;
            }

            if(cboMarca.SelectedIndex < 0)
            {
                MessageBox.Show("Debes Ingresar la Marca");
                return true;
            }
            if(cboCategoria.SelectedIndex < 0)
            {
                MessageBox.Show("Debes Ingresar la Categoria o Area");
                return true;
            }
            
            if (string.IsNullOrEmpty(txtPrecio.Text))
            {
                MessageBox.Show("Debes Ingresar un Precio");
                return true;

            }
            if (!(soloNumeros(txtPrecio.Text)))
            {
                    return true;
            }

            return false;
        }

        public bool soloNumeros(string texto)
        {
            foreach (char caracterer in texto)
            {
                if (!(char.IsNumber(caracterer)))
                {
                    MessageBox.Show("Llena el Campo de Precio con números");
                    return false;
                }
                return true;
            }

            return true;
        }
    }
}
