using Malvader.Models;
using Microsoft.AspNetCore.Connections;
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
            cmd.Parameters.AddWithValue("@codigoAgencia", agencia.CodigoAgencia);
            cmd.Parameters.AddWithValue("@enderecoId", agencia.EnderecoAgenciaId);

            var insertedId = Convert.ToInt32(cmd.ExecuteScalar());
            agencia.Id = insertedId;

            return agencia;
        }
        public Agencia? GetById(int id)
        {
            using var conn = _dbConnectionFactory.CreateConnection();
            conn.Open();

            string sql = @"
                SELECT * FROM agencia WHERE id_agencia = @agenciaId
            ";

            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@agenciaId", id);

            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new Agencia
                {
                    Id = reader.GetInt32("id_agencia"),
                    Nome = reader.GetString("nome"),
                    CodigoAgencia = reader.GetString("codigo_agencia"),
                    EnderecoAgenciaId = reader.GetInt32("endereco_id")
                };
            }

            return null;
        }
    }
}
