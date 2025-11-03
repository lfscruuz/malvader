using Malvader.Models;
using MySql.Data.MySqlClient;

namespace Malvader.DAOs
{
    public class AgenciaDAO
    {
        private readonly DbConnectionFactory _dbConnectionFactory;

        public AgenciaDAO(DbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public Agencia Insert(Agencia agencia)
        {
            using var conn = _dbConnectionFactory.CreateConnection();
            conn.Open();

            string sql = @"
                INSERT INTO agencia (nome, codigo_agencia, endereco_id)
                VALUES (@nome, @codigoAgencia, @enderecoId);
                SELECT LAST_INSERT_ID();
            ";

            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@nome", agencia.Nome);
            //cmd.Parameters.AddWithValue("@codigoAgencia",agencia.CodigoAgencia);
            cmd.Parameters.AddWithValue("@enderecoId", agencia.EnderecoAgencia.Id);

            var insertedId = Convert.ToInt32(cmd.ExecuteScalar());
            agencia.Id = insertedId;

            return agencia;
        }
    }
}
