using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;

namespace negocio
{
    public class MarcaNegocio
    {
        private List<Marca> listaMarca = new List<Marca>();
        AccesoDatos datos = new AccesoDatos();
        public List<Marca> listar()
        {

            try
            {
                datos.setearConsulta("SELECT Id, Descripcion FROM MARCAS");
                datos.ejecutarConsulta();

                while (datos.Lector.Read())
                {
                    Marca aux = new Marca();

                    aux.Id = (int)datos.Lector["Id"]; 
                    aux.Descripcion = (string)datos.Lector["Descripcion"];

                    listaMarca.Add(aux);

                }
                    return listaMarca;

            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }
}
