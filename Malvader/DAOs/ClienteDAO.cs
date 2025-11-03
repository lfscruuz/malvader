using Malvader.DAO;
using Malvader.Models;
using MySql.Data.MySqlClient;

namespace Malvader.DAOs
{
    public class ClienteDAO
    {
        private readonly DbConnectionFactory _connectionFactory;

        public ClienteDAO(DbConnectionFactory connectionFactory, UsuarioDAO usarioDao)
        {
            _connectionFactory = connectionFactory;
        }

        public Cliente Inserir(Cliente cliente) 
        {
            using var conn = _connectionFactory.CreateConnection();
            conn.Open();

            string sql = @"
                INSERT INTO cliente (id_usuario, score_credito)
                VALUES (@usuarioId, @scoreCredito);
                SELECT LAST_INSERT_ID();
            ";

            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@usuarioId", cliente.Usuario.Id);
            cmd.Parameters.AddWithValue("@scoreCredito", cliente.ScoreCredito);

            var insertedId = Convert.ToInt32(cmd.ExecuteScalar());
            cliente.Id = insertedId;

            return cliente;
        }
    }
}
