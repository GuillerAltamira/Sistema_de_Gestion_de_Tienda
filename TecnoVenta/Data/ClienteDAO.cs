using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using TecnoVenta.Models;

namespace TecnoVenta.Data
{
    public class ClienteDAO
    {
        public List<Cliente> ObtenerClientes()
        {
            List<Cliente> lista = new List<Cliente>();

            using (SqlConnection conn = new SqlConnection(Conexion.cadena))
            {
                conn.Open();

                string sql = "SELECT * FROM Clientes";

                SqlCommand cmd = new SqlCommand(sql, conn);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Cliente c = new Cliente();

                    c.Id = (int)reader["Id"];
                    c.Nombre = reader["Nombre"].ToString();
                    c.Telefono = reader["Telefono"].ToString();
                    c.Correo = reader["Correo"].ToString();

                    lista.Add(c);
                }
            }

            return lista;
        }

        public void AgregarCliente(Cliente cliente)
        {
            using (SqlConnection conn = new SqlConnection(Conexion.cadena))
            {
                conn.Open();

                string sql = "INSERT INTO Clientes (Nombre,Telefono,Correo) VALUES (@nombre,@telefono,@correo)";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@nombre", cliente.Nombre);
                cmd.Parameters.AddWithValue("@telefono", cliente.Telefono);
                cmd.Parameters.AddWithValue("@correo", cliente.Correo);

                cmd.ExecuteNonQuery();
            }
        }
    }
}