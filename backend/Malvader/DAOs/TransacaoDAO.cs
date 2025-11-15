using Malvader.Models;
using MySql.Data.MySqlClient;
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

    }
}
