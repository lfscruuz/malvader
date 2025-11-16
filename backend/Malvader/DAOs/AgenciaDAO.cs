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
        public Agencia GetById(int id)
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

            throw new KeyNotFoundException("Agencia não foi encontrada");
        }

        public Agencia GetByCodigo(string codigo)
        {
            using var conn = _dbConnectionFactory.CreateConnection();
            conn.Open();

            string sql = @"
                SELECT * FROM agencia WHERE codigo_agencia = @codigo
            ";

            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@codigo", codigo);

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
            throw new KeyNotFoundException("Agencia não foi encontrada");
        }

        public List<Agencia> GetAll()
        {
            var agencias = new List<Agencia>();
            using var conn = _dbConnectionFactory.CreateConnection();
            conn.Open();

            string sql = @"
                SELECT * FROM agencia
            ";

            using var cmd = new MySqlCommand(sql, conn);

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var agencia = new Agencia
                {
                    Id = reader.GetInt32("id_agencia"),
                    Nome = reader.GetString("nome"),
                    CodigoAgencia = reader.GetString("codigo_agencia"),
                    EnderecoAgenciaId = reader.GetInt32("endereco_id")
                };
                agencias.Add(agencia);
                
            }
            throw new KeyNotFoundException("Erro na busca por agências");
        }
    }
}