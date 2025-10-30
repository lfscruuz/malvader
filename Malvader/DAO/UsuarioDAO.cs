using Malvader.Models;
using MySql.Data.MySqlClient;

namespace Malvader.DAO
{
    public class UsuarioDAO
    {
        private readonly DbConnectionFactory _connectionFactory;

        public UsuarioDAO(DbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public List<Usuario> ListarTodos()
        {
            var usuarios = new List<Usuario>();
            using var conn = _connectionFactory.CreateConnection();
            conn.Open();

            string sql = "SELECT * FROM usuario";
            using var cmd = new MySqlCommand(sql, conn);
            using var reader = cmd.ExecuteReader();

            while (reader.Read()) {
                usuarios.Add(new Usuario
                {
                    Id = reader.GetInt32("id_usuario"),
                    Nome = reader.GetString("nome"),
                    CPF = reader.GetString("cpf"),
                    DataNascimento = reader.GetDateTime("data_nascimento"),
                    Telefone = reader.GetString("telefone"),
                    Tipo = Enum.Parse<TipoUsuario>(reader.GetString("tipo_usuario")),
                    SenhaHash = reader.GetString("senha_hash")
                });
            }

            return usuarios;
        }

    }
}
