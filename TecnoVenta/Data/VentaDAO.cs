using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using TecnoVenta.Models;

namespace TecnoVenta.Data
{
    public class VentaDAO
    {
        public List<Venta> ObtenerVentas()
        {
            List<Venta> lista = new List<Venta>();

            using (SqlConnection conn = new SqlConnection(Conexion.cadena))
            {
                conn.Open();
                string sql = "SELECT * FROM Ventas";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Venta v = new Venta
                    {
                        Id = (int)reader["Id"],
                        ClienteId = (int)reader["ClienteId"],
                        ProductoId = (int)reader["ProductoId"],
                        Cantidad = (int)reader["Cantidad"],
                        Total = (decimal)reader["Total"],
                        Fecha = (DateTime)reader["Fecha"]
                    };
                    lista.Add(v);
                }
            }

            return lista;
        }

        public void AgregarVenta(Venta venta)
        {
            using (SqlConnection conn = new SqlConnection(Conexion.cadena))
            {
                conn.Open();
                string sql = "INSERT INTO Ventas (ClienteId, ProductoId, Cantidad, Total) VALUES (@clienteId,@productoId,@cantidad,@total)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@clienteId", venta.ClienteId);
                cmd.Parameters.AddWithValue("@productoId", venta.ProductoId);
                cmd.Parameters.AddWithValue("@cantidad", venta.Cantidad);
                cmd.Parameters.AddWithValue("@total", venta.Total);

                cmd.ExecuteNonQuery();
            }
        }
    }
}
