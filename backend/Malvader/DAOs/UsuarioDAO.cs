using Malvader.Models;
using MySql.Data.MySqlClient;
using System.Data;
using System.Security.Authentication;

namespace Malvader.DAO
{
    public class UsuarioDAO
    {
        private readonly DbConnectionFactory _dbConnectionFactory;

        public UsuarioDAO(DbConnectionFactory connectionFactory)
        {
            _dbConnectionFactory = connectionFactory;
        }
        public Usuario Insert(Usuario usuario)
        {
            using var conn = _dbConnectionFactory.CreateConnection();
            conn.Open();

            string sql = @"
                INSERT INTO usuario (nome, cpf, data_nascimento, telefone, tipo_usuario, senha_hash)
                VALUES (@nome, @cpf, @dataNascimento, @telefone, @tipoUsuario, @senhaHash);
                SELECT LAST_INSERT_ID();
            ";

            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@nome", usuario.Nome);
            cmd.Parameters.AddWithValue("@cpf", usuario.CPF);
            cmd.Parameters.AddWithValue("@dataNascimento", usuario.DataNascimento);
            cmd.Parameters.AddWithValue("@telefone", usuario.Telefone);
            cmd.Parameters.AddWithValue("@tipoUsuario", usuario.TipoUsuario.ToString());
            cmd.Parameters.AddWithValue("@senhaHash", usuario.SenhaHash);

            var insertedId = Convert.ToInt32(cmd.ExecuteScalar());
            usuario.Id = insertedId;
            UpdateSenhaHash(insertedId, usuario.SenhaHash);
            return usuario;
        }

        public void UpdateSenhaHash(int usuarioId, string senha)
        {
            using var conn = _dbConnectionFactory.CreateConnection();
            conn.Open();

            using var cmd = new MySqlCommand("alterar_senha_usuario", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@p_id_usuario", usuarioId);
            cmd.Parameters.AddWithValue("@p_senha_clara", senha);

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                DeleteById(usuarioId);
                throw new Exception(ex.Message);
            }
        }
        public List<Usuario> ListarTodos()
        {
            var usuarios = new List<Usuario>();
            using var conn = _dbConnectionFactory.CreateConnection();
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
                    TipoUsuario = Enum.Parse<TipoUsuario>(reader.GetString("tipo_usuario")),
                    SenhaHash = reader.GetString("senha_hash")
                });
            }

            return usuarios;
        }

        public Usuario? GetById(int id)
        {
            using var conn = _dbConnectionFactory.CreateConnection();
            conn.Open();

            string sql = @"
                SELECT * FROM usuario WHERE id_usuario = @usuarioId
            ";

            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@usuarioId", id);

            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new Usuario
                {
                    Id = reader.GetInt32("id_usuario"),
                    Nome = reader.GetString("nome"),
                    CPF = reader.GetString("cpf"),
                    DataNascimento = reader.GetDateTime("data_nascimento"),
                    Telefone = reader.GetString("telefone"),
                    TipoUsuario = Enum.Parse<TipoUsuario>(reader.GetString("tipo_usuario")),
                    SenhaHash = reader.GetString("senha_hash")
                };
            }

            return null;
        }

        public Usuario GetByCpf(string cpf) {
            using var conn = _dbConnectionFactory.CreateConnection();
            conn.Open();

            string sql = @"
                SELECT * FROM usuario WHERE cpf = @cpf
            ";

            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@cpf", cpf);

            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new Usuario
                {
                    Id = reader.GetInt32("id_usuario"),
                    Nome = reader.GetString("nome"),
                    CPF = reader.GetString("cpf"),
                    DataNascimento = reader.GetDateTime("data_nascimento"),
                    Telefone = reader.GetString("telefone"),
                    TipoUsuario = Enum.Parse<TipoUsuario>(reader.GetString("tipo_usuario")),
                    SenhaHash = reader.GetString("senha_hash")
                };
            }
            throw new AuthenticationException("CPF ou senha incorreto");
        }

        public void DeleteById(int id)
        {
            using var conn = _dbConnectionFactory.CreateConnection();
            conn.Open();

            var sql = "DELETE FROM usuario WHERE id_usuario = @id";
            using var cmd = new MySqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }
    }
}
