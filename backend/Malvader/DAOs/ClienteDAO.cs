using Malvader.DAO;
using Malvader.Models;
using MySql.Data.MySqlClient;

namespace Malvader.DAOs
{
    public class ClienteDAO
    {
        private readonly DbConnectionFactory _dbConnectionFactory;
        private readonly UsuarioDAO _usuarioDao;

        public ClienteDAO(DbConnectionFactory connectionFactory, UsuarioDAO usuarioDao)
        {
            _dbConnectionFactory = connectionFactory;
            _usuarioDao = usuarioDao;
        }

        public Cliente Insert(Cliente cliente) 
        {
            try
            {
                using var conn = _dbConnectionFactory.CreateConnection();
                conn.Open();

                string sql = @"
                    INSERT INTO cliente (id_usuario, score_credito)
                    VALUES (@usuarioId, @scoreCredito);
                    SELECT LAST_INSERT_ID();
                ";

                using var cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@usuarioId", cliente.UsuarioId);
                cmd.Parameters.AddWithValue("@scoreCredito", cliente.ScoreCredito);

                var insertedId = Convert.ToInt32(cmd.ExecuteScalar());
                cliente.Id = insertedId;

                return cliente;
            }
            catch (Exception ex)
            {
                _usuarioDao.DeleteById(cliente.UsuarioId);
                throw;
            }
        }

        public Cliente? GetById(int id)
        {
            using var conn = _dbConnectionFactory.CreateConnection();
            conn.Open();

            string sql = @"
                SELECT * FROM cliente WHERE id_cliente = @clienteId
            ";

            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@clienteId", id);

            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new Cliente
                {
                    Id = reader.GetInt32("id_cliente"),
                    UsuarioId = reader.GetInt32("id_usuario"),
                    ScoreCredito = reader.GetDecimal("score_credito")
                };
            }

            return null;
        }

        public Cliente GetByUsuarioId(int id)
        {
            using var conn = _dbConnectionFactory.CreateConnection();
            conn.Open();

            string sql = @"
                SELECT * FROM cliente WHERE id_usuario = @usuarioId
            ";

            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@usuarioId", id);

            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new Cliente
                {
                    Id = reader.GetInt32("id_cliente"),
                    UsuarioId = reader.GetInt32("id_usuario"),
                    ScoreCredito = reader.GetDecimal("score_credito")
                };
            }

            return null;
        }
    }
}
