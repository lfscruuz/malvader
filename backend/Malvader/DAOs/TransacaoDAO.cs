using Malvader.Models;
using MySql.Data.MySqlClient;
using System.Data;
using System.Transactions;

namespace Malvader.DAOs
{
    public class TransacaoDAO
    {
        private readonly DbConnectionFactory _dbConnectionFactory;

        public TransacaoDAO(DbConnectionFactory dbConnecionFactory)
        {
            _dbConnectionFactory = dbConnecionFactory;
        }

        public Transacao Insert(Transacao transacao)
        {
            using var conn = _dbConnectionFactory.CreateConnection();
            conn.Open();

            string sql = @"
                INSERT INTO transacao (id_conta_origem, id_conta_destino, tipo_transacao, valor, data_hora, descricao)
                VALUES (@contaOrigemId, @contaDestinoId, @tipoTransacao, @valor, @dataHora, @descricao);
                SELECT LAST_INSERT_ID();
            ";

            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@contaOrigemId",transacao.ContaOrigemId);
            cmd.Parameters.AddWithValue("@contaDestinoId",transacao.ContaDestinoId);
            cmd.Parameters.AddWithValue("@tipoTransacao",transacao.TipoTransacao.ToString());
            cmd.Parameters.AddWithValue("@valor",transacao.Valor);
            cmd.Parameters.AddWithValue("@dataHora",transacao.DataHora);
            cmd.Parameters.AddWithValue("@descricao",transacao.Descricao);

            var insertedId = Convert.ToInt32(cmd.ExecuteScalar());
            transacao.Id = insertedId;

            return transacao;
        }

        public List<Transacao> GetExtrato(int contaId, int limite) 
        {
            var transacoes = new List<Transacao>();

            using var conn = _dbConnectionFactory.CreateConnection();
            conn.Open();

            string sql = @"
                SELECT 
                    id_transacao,
                    id_conta_origem,
                    id_conta_destino,
                    tipo_transacao,
                    valor,
                    data_hora,
                    descricao
                FROM transacao
                WHERE (id_conta_origem = @contaId OR id_conta_destino = @contaId)
                ORDER BY data_hora DESC
                LIMIT @limite;
            ";

            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@contaId", contaId);
            cmd.Parameters.AddWithValue("@limite", limite);

            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                var transacao = new Transacao
                {
                    Id = reader.GetInt32("id_transacao"),
                    ContaOrigemId = reader.IsDBNull("id_conta_origem")
                        ? null
                        : reader.GetInt32("id_conta_origem"),
                    ContaDestinoId = reader.IsDBNull("id_conta_destino")
                        ? null
                        : reader.GetInt32("id_conta_destino"),
                    TipoTransacao = Enum.Parse<TipoTransacao>(reader.GetString("tipo_transacao").ToString()),
                    Valor = reader.GetDecimal("valor"),
                    DataHora = reader.GetDateTime("data_hora"),
                    Descricao = reader.GetString("descricao")
                };

                transacoes.Add(transacao);
            }

            return transacoes;
        }
        public List<Transacao> GetExtrato(int contaId, int limite, DateTime? dataInicio, DateTime? dataFim) 
        {
            var transacoes = new List<Transacao>();
            dataFim = dataFim.Value.Date.AddDays(1).AddTicks(-1);

            using var conn = _dbConnectionFactory.CreateConnection();
            conn.Open();

            string sql = @"
                SELECT 
                    id_transacao,
                    id_conta_origem,
                    id_conta_destino,
                    tipo_transacao,
                    valor,
                    data_hora,
                    descricao
                FROM transacao
                WHERE (id_conta_origem = @contaId OR id_conta_destino = @contaId)
                    AND data_hora BETWEEN @dataInicio AND @dataFim
                ORDER BY data_hora DESC
                LIMIT @limite;
            ";

            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@contaId", contaId);
            cmd.Parameters.AddWithValue("@dataInicio", dataInicio);
            cmd.Parameters.AddWithValue("@dataFim", dataFim);
            cmd.Parameters.AddWithValue("@limite", limite);

            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                var transacao = new Transacao
                {
                    Id = reader.GetInt32("id_transacao"),
                    ContaOrigemId = reader.IsDBNull("id_conta_origem")
                        ? null
                        : reader.GetInt32("id_conta_origem"),
                    ContaDestinoId = reader.IsDBNull("id_conta_destino")
                        ? null
                        : reader.GetInt32("id_conta_destino"),
                    TipoTransacao = Enum.Parse<TipoTransacao>(reader.GetString("tipo_transacao").ToString()),
                    Valor = reader.GetDecimal("valor"),
                    DataHora = reader.GetDateTime("data_hora"),
                    Descricao = reader.GetString("descricao")
                };

                transacoes.Add(transacao);
            }

            return transacoes;
        }
    }
}
