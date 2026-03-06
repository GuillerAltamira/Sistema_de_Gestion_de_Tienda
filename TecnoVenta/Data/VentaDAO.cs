using System;
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
                        Fecha = (DateTime)reader["Fecha"],
                        Total = (decimal)reader["Total"]
                    };
                    lista.Add(v);
                }
            }
            return lista;
        }
        public bool AgregarVenta(Venta venta) 
        {
          using (SqlConnection conn = new SqlConnection(Conexion.cadena)) 
           {
             conn.Open();

               // 1. Verificar stock del producto
               string sqlStock = "SELECT Stock FROM Productos WHERE Id=@productoId";
               SqlCommand cmdStock = new SqlCommand(sqlStock, conn);
               cmdStock.Parameters.AddWithValue("@productoId", venta.ProductoId);

              object result = cmdStock.ExecuteScalar();
              if (result == null) 
              {
                // Producto no encontrado
                return false;
              }

             int stockActual = Convert.ToInt32(result);

             if (stockActual < venta.Cantidad) 
             {
              // No hay stock suficiente → salir inmediatamente
              return false;
             }

             // 2. Insertar la venta (solo si hay stock suficiente)
             string sqlVenta = "INSERT INTO Ventas (ClienteId,ProductoId,Cantidad,Fecha,Total) VALUES (@clienteId,@productoId,@cantidad,@fecha,@total)";
             SqlCommand cmdVenta = new SqlCommand(sqlVenta, conn);
             cmdVenta.Parameters.AddWithValue("@clienteId", venta.ClienteId);
             cmdVenta.Parameters.AddWithValue("@productoId", venta.ProductoId);
             cmdVenta.Parameters.AddWithValue("@cantidad", venta.Cantidad);
             cmdVenta.Parameters.AddWithValue("@fecha", venta.Fecha);
             cmdVenta.Parameters.AddWithValue("@total", venta.Total);
             cmdVenta.ExecuteNonQuery();

             // 3. Actualizar stock
             string sqlUpdate = "UPDATE Productos SET Stock = Stock - @cantidad WHERE Id=@productoId";
             SqlCommand cmdUpdate = new SqlCommand(sqlUpdate, conn);
             cmdUpdate.Parameters.AddWithValue("@cantidad", venta.Cantidad);
             cmdUpdate.Parameters.AddWithValue("@productoId", venta.ProductoId);
             cmdUpdate.ExecuteNonQuery();
              return true;
           }
        }
    }    
}
