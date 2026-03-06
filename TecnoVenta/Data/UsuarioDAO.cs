using Microsoft.Data.SqlClient;
using TecnoVenta.Models;

namespace TecnoVenta.Data {
    public class UsuarioDAO {
        public Usuario? ValidarLogin(string nombreUsuario, string contraseña) {
            using (SqlConnection conn = new SqlConnection(Conexion.cadena)) {
                conn.Open();
                string sql = "SELECT * FROM Usuarios WHERE NombreUsuario=@usuario AND Contraseña=@pass";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@usuario", nombreUsuario);
                cmd.Parameters.AddWithValue("@pass", contraseña);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read()) {
                    return new Usuario {
                        Id = (int)reader["Id"],
                        NombreUsuario = reader["NombreUsuario"].ToString(),
                        Contraseña = reader["Contraseña"].ToString()
                    };
                }
            }
            return null;
        }
    }
}
