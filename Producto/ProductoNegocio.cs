using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using dominio;


namespace negocio
{
    public class ProductoNegocio
    {
        AccesoDatos datos = new AccesoDatos();
        public List<Producto> listaproductos = new List<Producto>();
     

        public List<Producto> listar()
        {
            try
            {
                datos.setearConsulta("SELECT A.Id, A.CODIGO, A.NOMBRE, A.Descripcion, M.Descripcion AS Marca, C.Descripcion AS Area, ImagenUrl, Precio  FROM ARTICULOS A, MARCAS M, CATEGORIAS C WHERE A.IdMarca = M.Id AND A.IdCategoria = C.Id AND Codigo !='NN'");
                datos.ejecutarConsulta();

                while (datos.Lector.Read())
                {
                    Producto aux = new Producto();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Codigo = (string)datos.Lector["Codigo"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];
                    aux.Marca = new Marca();
                    aux.Marca.Descripcion = (string)datos.Lector["Marca"];
                    aux.Categoria = new Categoria();
                    aux.Categoria.Descripcion = (string)datos.Lector["Area"];
                    if (!(datos.Lector["ImagenUrl"] is DBNull))
                            aux.ImagenUrl = (string)datos.Lector["ImagenUrl"];
                        
                    aux.Precio = (decimal)datos.Lector["Precio"];

                    listaproductos.Add(aux);

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }

            return listaproductos;
        }

        public void agregar(Producto nuevo)
        {
            AccesoDatos datos = new AccesoDatos();


            try
            {
                datos.setearConsulta("INSERT INTO ARTICULOS (Codigo, Nombre, Descripcion, ImagenUrl, IdMarca, IdCategoria,Precio) VALUES(@codigo, @nombre, @descripcion, @imagenUrl, @idMarca,@idCategoria,@precio)");
                datos.setearParametros("@codigo", nuevo.Codigo);
                datos.setearParametros("@nombre", nuevo.Nombre);
                datos.setearParametros("@descripcion", nuevo.Descripcion);
                datos.setearParametros("@imagenUrl", nuevo.ImagenUrl);
                datos.setearParametros("@idMarca", nuevo.Marca.Id);
                datos.setearParametros("@idCategoria",nuevo.Categoria.Id);
                datos.setearParametros("@precio",nuevo.Precio);

                datos.ejecutarAccion();
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

        public void modificar(Producto producto)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE ARTICULOS SET Codigo=@codigo, Nombre=@nombre, Descripcion=@descripcion, IdMarca=@marca, IdCategoria=@categoria , ImagenUrl=@urlImg, Precio=@precio WHERE Id=@id");
                datos.setearParametros("@codigo", producto.Codigo);
                datos.setearParametros("@nombre", producto.Nombre);
                datos.setearParametros("@descripcion", producto.Descripcion);
                datos.setearParametros("@marca", producto.Marca.Id);
                datos.setearParametros("@categoria", producto.Categoria.Id);
                datos.setearParametros("@urlImg", producto.ImagenUrl);
                datos.setearParametros("@precio", producto.Precio);
                datos.setearParametros("@id", producto.Id);

                datos.ejecutarAccion();
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

        public void eliminar(int id)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("DELETE FROM ARTICULOS WHERE Id=@id");
                datos.setearParametros("@id",id);
                datos.ejecutarAccion();
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
        public void eliminarLogico(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE ARTICULOS SET Codigo = 'NN' WHERE Id=@id");
                datos.setearParametros("@id", id);
                datos.ejecutarAccion();
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

        public List<Producto> filtrar(string campo , string criterio , string filtro)
        {
            listaproductos = new List<Producto>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                //Nombre Marca Precio
                string consulta = "SELECT A.Id, Codigo,Nombre,A.Descripcion,M.Descripcion AS Marca ,C.Descripcion AS Categoria, ImagenUrl, Precio ,A.IdMarca,A.IdCategoria FROM ARTICULOS A, CATEGORIAS C, MARCAS M WHERE A.IdMarca=M.Id AND A.IdCategoria=C.Id AND Codigo!='NN' AND ";
                
                if (campo == "Nombre")
                {
                    switch (criterio)
                    {
                        case "Comienza con:":
                            consulta += "Nombre like '" +filtro+ "%'";
                            break;
                        case "Termina con:":
                            consulta += "Nombre like '%" +filtro+"'";
                                break;
                        default:
                            consulta += "Nombre like '%" + filtro + "%'";
                            break;
                    }
                }else if(campo == "Marca")
                {
                    switch (criterio)
                    {
                        case "Comineza con:":
                            consulta += "M.Descripcion like '" + filtro+"%'";
                                break;
                        case "Termina con:":
                            consulta += "M.Descripcion like '%" + filtro + "'";
                            break;
                        default:
                            consulta += "M.Descripcion like '%" + filtro + "%'";  
                        break;
                    }
                }
                else
                {
                    switch (criterio)
                    {
                        case "Mayor a:":
                            consulta += "Precio  > '" + filtro + "'";
                            break;
                        case "Menor a:":
                            consulta += "Precio < '"+filtro+"'";
                            break;
                        default:
                            consulta += "Precio = '" + filtro + "'";
                            break;
                    }
                }



                datos.setearConsulta(consulta);
                datos.ejecutarConsulta();

                while (datos.Lector.Read())
                {
                    Producto aux = new Producto();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Codigo = (string)datos.Lector["Codigo"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];
                    aux.Marca = new Marca();
                    aux.Marca.Id = (int)datos.Lector["IdMarca"];
                    aux.Marca.Descripcion = (string)datos.Lector["Marca"];
                    aux.Categoria = new Categoria();
                    aux.Categoria.Id = (int)datos.Lector["IdCategoria"];
                    aux.Categoria.Descripcion = (string)datos.Lector["Categoria"];
                    aux.ImagenUrl = (string)datos.Lector["ImagenUrl"];
                    aux.Precio = (decimal)datos.Lector["Precio"];

                    listaproductos.Add(aux);

                }

                   return listaproductos;
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

        public List<Producto> listaAlta()
        {
            AccesoDatos datos = new AccesoDatos();
            listaproductos = new List<Producto>();
            try
            {
                datos.setearConsulta("SELECT A.Id, A.Codigo, A.Nombre , M.Descripcion AS Marca, C.Descripcion AS Categoria FROM ARTICULOS A ,MARCAS M, CATEGORIAS C WHERE A.IdMarca = M.Id AND A.IdCategoria = C.Id AND A.Codigo = 'NN'");
                datos.ejecutarConsulta();

                while (datos.Lector.Read())
                {

                    Producto aux = new Producto();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Codigo = (string)datos.Lector["Codigo"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Marca = new Marca();
                    aux.Marca.Descripcion = (string)datos.Lector["Marca"];
                    aux.Categoria = new Categoria();
                    aux.Categoria.Descripcion = (string)datos.Lector["Categoria"];

                    listaproductos.Add(aux);

                }
                return listaproductos;
            }
            catch (Exception ex)
            {

                throw ex;
            }
           
        }

        public void altaProducto(Producto seleccionado, string codigo)
        {
            datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE ARTICULOS SET Codigo =@codigo WHERE Id=@id");
                datos.setearParametros("@id",seleccionado.Id);
                datos.setearParametros("@codigo", codigo);

                datos.ejecutarAccion();
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
