using Malvader.Models;
using MySql.Data.MySqlClient;
using System.Data;

namespace Malvader.DAOs
{
    public class ContaDAO
    {
        private readonly DbConnectionFactory _dbConnectionFactory;

        public ContaDAO(DbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public Conta Insert(Conta conta)
        {
            using var conn = _dbConnectionFactory.CreateConnection();
            conn.Open();

            string sql = @"
                INSERT INTO conta (id_agencia, tipo_conta, id_cliente, status)
                VALUES (@agenciaId, @tipoConta, @clienteId, @status);
                SELECT LAST_INSERT_ID();
            ";

            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@agenciaId", conta.AgenciaId);
            cmd.Parameters.AddWithValue("@tipoConta", conta.TipoConta.ToString());
            cmd.Parameters.AddWithValue("@clienteId", conta.ClienteId);
            cmd.Parameters.AddWithValue("@status", conta.StatusConta.ToString());

            var insertedId = Convert.ToInt32(cmd.ExecuteScalar());
            conta.Id = insertedId;

            return conta;
        }

        public Conta GetById(int id)
        {
            using var conn = _dbConnectionFactory.CreateConnection();
            conn.Open();

            string sql = @"
                SELECT * FROM conta WHERE id_conta = @contaId
            ";

            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@contaId", id);

            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new Conta
                {
                    Id = reader.GetInt32("id_conta"),
                    NumeroConta = reader.GetString("numero_conta"),
                    AgenciaId = reader.GetInt32("id_agencia"),
                    Saldo = reader.GetDecimal("saldo"),
                    TipoConta = Enum.Parse<TipoConta>(reader.GetString("tipo_conta")),
                    ClienteId = reader.GetInt32("id_cliente"),
                    DataAbertura = reader.GetDateTime("data_abertura"),
                    StatusConta = Enum.Parse<StatusConta>(reader.GetString("status"))
                };
            }

            throw new Exception("ID da conta não encontrado");
        }

        public int GetContaIdByNumeroConta (string numero)
        {
            using var conn = _dbConnectionFactory.CreateConnection();
            conn.Open();

            string sql = @"
                SELECT * FROM conta WHERE numero_conta = @numero
            ";

            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@numero", numero);

            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return reader.GetInt32("id_conta");
            }
            throw new KeyNotFoundException("Número da conta não encontrado");
        }

        public void CloseContaById(int contaId, string motivo)
        {
            using var conn = _dbConnectionFactory.CreateConnection();
            conn.Open();

            using var cmd = new MySqlCommand("encerrar_conta", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@p_id_conta", contaId);
            cmd.Parameters.AddWithValue("@p_motivo", motivo);

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}
