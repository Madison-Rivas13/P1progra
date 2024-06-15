using System.Collections.Generic;

namespace p1final2024
{
    public class ArticuloDAO
    {
        // Cadena de conexión a la base de datos MySQL
        private string connectionString = "server=sql5.freesqldatabase.com;" +
            "user=sql5712512;" +
            "database=sql5712512;" +
            "port=3306;" +
            "password=rUpYP1VwYa";

        // Constructor
        public ArticuloDAO()
        {
        }

        // Método para crear un nuevo artículo
        public void Create(Articulo articulo)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "INSERT INTO articulos (nombre, descripcion, precio, imagen) VALUES (@nombre, @descripcion, @precio, @imagen)";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@nombre", articulo.Nombre);
                    cmd.Parameters.AddWithValue("@descripcion", articulo.Descripcion);
                    cmd.Parameters.AddWithValue("@precio", articulo.Precio);
                    cmd.Parameters.AddWithValue("@imagen", articulo.Imagen);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Método para leer un artículo por ID
        public Articulo Read(int id)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM articulos WHERE id = @id";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Articulo articulo = new Articulo()
                            {
                                Id = reader.GetInt32("id"),
                                Nombre = reader.GetString("nombre"),
                                Descripcion = reader.GetString("descripcion"),
                                Precio = reader.GetDecimal("precio"),
                                Imagen = reader["imagen"] as byte[]
                            };

                            return articulo;
                        }
                    }
                }
            }
            return null;
        }

        // Método para leer todos los artículos
        public List<Articulo> ReadAll()
        {
            List<Articulo> articulos = new List<Articulo>();
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM articulos";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            articulos.Add(new Articulo
                            {
                                Id = reader.GetInt32("id"),
                                Nombre = reader.GetString("nombre"),
                                Descripcion = reader.GetString("descripcion"),
                                Precio = reader.GetDecimal("precio"),
                                Imagen = reader["imagen"] as byte[]
                            });
                        }
                    }
                }
            }
            return articulos;
        }

        // Método para actualizar un artículo existente
        public void Update(Articulo articulo)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "UPDATE articulos SET nombre = @nombre, descripcion = @descripcion, precio = @precio, imagen = @imagen WHERE id = @id";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@nombre", articulo.Nombre);
                    cmd.Parameters.AddWithValue("@descripcion", articulo.Descripcion);
                    cmd.Parameters.AddWithValue("@precio", articulo.Precio);
                    cmd.Parameters.AddWithValue("@imagen", articulo.Imagen);
                    cmd.Parameters.AddWithValue("@id", articulo.Id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Método para eliminar un artículo por ID
        public void Delete(int id)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "DELETE FROM articulos WHERE id = @id";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
