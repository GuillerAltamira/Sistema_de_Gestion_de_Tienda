using System.Collections.Generic;
using System.Data.SqlClient;
using TecnoVenta.Models;

namespace TecnoVenta.Data
{
    public class ProductoDAO
    {
        Conexion conexion = new Conexion();

        public List<Producto> ObtenerProductos()
        {
            List<Producto> lista = new List<Producto>();

            using (SqlConnection conn = conexion.ObtenerConexion())
            {
                string query = "SELECT * FROM Productos";

                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Producto p = new Producto();

                    p.Id = (int)reader["Id"];
                    p.Nombre = reader["Nombre"].ToString();
                    p.Precio = (decimal)reader["Precio"];
                    p.Stock = (int)reader["Stock"];

                    lista.Add(p);
                }
            }

            return lista;
        }

        public void AgregarProducto(Producto producto)
        {
            using (SqlConnection conn = conexion.ObtenerConexion())
            {
                string query = "INSERT INTO Productos (Nombre, Precio, Stock) VALUES (@nombre,@precio,@stock)";

                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@nombre", producto.Nombre);
                cmd.Parameters.AddWithValue("@precio", producto.Precio);
                cmd.Parameters.AddWithValue("@stock", producto.Stock);

                cmd.ExecuteNonQuery();
            }
        }

        public void ActualizarProducto(Producto producto)
        {
            using (SqlConnection conn = conexion.ObtenerConexion())
            {
                string query = "UPDATE Productos SET Nombre=@nombre, Precio=@precio, Stock=@stock WHERE Id=@id";

                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@id", producto.Id);
                cmd.Parameters.AddWithValue("@nombre", producto.Nombre);
                cmd.Parameters.AddWithValue("@precio", producto.Precio);
                cmd.Parameters.AddWithValue("@stock", producto.Stock);

                cmd.ExecuteNonQuery();
            }
        }

        public void EliminarProducto(int id)
        {
            using (SqlConnection conn = conexion.ObtenerConexion())
            {
                string query = "DELETE FROM Productos WHERE Id=@id";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);

                cmd.ExecuteNonQuery();
            }
        }
    }
}
