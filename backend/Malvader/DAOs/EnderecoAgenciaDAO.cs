using Malvader.Models;
using MySql.Data.MySqlClient;

namespace Malvader.DAOs
{
    public class EnderecoAgenciaDAO
    {
        private readonly DbConnectionFactory _connectionFactory;

        public EnderecoAgenciaDAO(DbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public EnderecoAgencia Insert(EnderecoAgencia endereco)
        {
            using var conn = _connectionFactory.CreateConnection();
            conn.Open();

            string sql = @"
                INSERT INTO endereco_agencia (cep, local, numero, bairro, cidade, estado, complemento)
                VALUES (@cep, @local, @numero, @bairro, @cidade, @estado, @complemento);
                SELECT LAST_INSERT_ID();
            ";

            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@cep", endereco.Cep);
            cmd.Parameters.AddWithValue("@local", endereco.Local);
            cmd.Parameters.AddWithValue("@numero", endereco.Numero);
            cmd.Parameters.AddWithValue("@bairro", endereco.Bairro);
            cmd.Parameters.AddWithValue("@cidade", endereco.Cidade);
            cmd.Parameters.AddWithValue("@estado", endereco.Estado);
            cmd.Parameters.AddWithValue("@complemento", endereco.Complemento);

            var insertedId = Convert.ToInt32(cmd.ExecuteScalar());
            endereco.Id = insertedId;

            return endereco;
        }
    }
}
