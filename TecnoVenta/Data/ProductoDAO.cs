using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using TecnoVenta.Models;

namespace TecnoVenta.Data
{
    public class ProductoDAO
    {
        string conexion = Conexion.cadena;

        // LISTAR PRODUCTOS
        public List<Producto> ObtenerProductos()
        {
            List<Producto> lista = new List<Producto>();

            using (SqlConnection conn = new SqlConnection(conexion))
            {
                string sql = "SELECT * FROM Productos";

                SqlCommand cmd = new SqlCommand(sql, conn);

                conn.Open();

                SqlDataReader lector = cmd.ExecuteReader();

                while (lector.Read())
                {
                    Producto p = new Producto();

                    p.Id = Convert.ToInt32(lector["Id"]);
                    p.Nombre = lector["Nombre"].ToString();
                    p.Precio = Convert.ToDecimal(lector["Precio"]);
                    p.Stock = Convert.ToInt32(lector["Stock"]);

                    lista.Add(p);
                }
            }

            return lista;
        }

        // AGREGAR PRODUCTO
        public void AgregarProducto(Producto p)
        {
            using (SqlConnection conn = new SqlConnection(conexion))
            {
                string sql = "INSERT INTO Productos (Nombre,Precio,Stock) VALUES (@nombre,@precio,@stock)";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@nombre", p.Nombre);
                cmd.Parameters.AddWithValue("@precio", p.Precio);
                cmd.Parameters.AddWithValue("@stock", p.Stock);

                conn.Open();

                cmd.ExecuteNonQuery();
            }
        }

        // ELIMINAR PRODUCTO
        public void EliminarProducto(int id)
        {
            using (SqlConnection conn = new SqlConnection(conexion))
            {
                string sql = "DELETE FROM Productos WHERE Id=@id";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@id", id);

                conn.Open();

                cmd.ExecuteNonQuery();
            }
        }
        // ACTUALIZAR PRODUCTO
public void ActualizarProducto(Producto p)
{
    using (SqlConnection conn = new SqlConnection(conexion))
    {
        string sql = "UPDATE Productos SET Nombre=@nombre, Precio=@precio, Stock=@stock WHERE Id=@id";

        SqlCommand cmd = new SqlCommand(sql, conn);

        cmd.Parameters.AddWithValue("@id", p.Id);
        cmd.Parameters.AddWithValue("@nombre", p.Nombre);
        cmd.Parameters.AddWithValue("@precio", p.Precio);
        cmd.Parameters.AddWithValue("@stock", p.Stock);

        conn.Open();

        cmd.ExecuteNonQuery();
    }
}
public void DescontarStock(int productoId, int cantidad)
{
    using (SqlConnection conn = new SqlConnection(Conexion.cadena))
    {
        conn.Open();
        string sql = "UPDATE Productos SET Stock = Stock - @cantidad WHERE Id = @id";
        SqlCommand cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@cantidad", cantidad);
        cmd.Parameters.AddWithValue("@id", productoId);
        cmd.ExecuteNonQuery();
    }
}
    }
}
