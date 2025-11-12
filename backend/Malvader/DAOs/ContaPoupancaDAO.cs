using Malvader.Models;
using MySql.Data.MySqlClient;

namespace Malvader.DAOs
{
    public class ContaPoupancaDAO
    {
        private readonly DbConnectionFactory _dbConnectionFactory;

        public ContaPoupancaDAO(DbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public ContaPoupanca Insert(ContaPoupanca contaPoupanca)
        {
            using var conn = _dbConnectionFactory.CreateConnection();
            conn.Open();

            string sql = @"
                INSERT INTO conta_poupanca (id_conta, taxa_rendimento, ultimo_rendimento)
                VALUES (@contaId, @taxaRendimento, @ultimoRendimento);
                SELECT LAST_INSERT_ID();
            ";

            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@contaId", contaPoupanca.ContaId);
            cmd.Parameters.AddWithValue("@taxaRendimento", contaPoupanca.TaxaRendimento);
            cmd.Parameters.AddWithValue("@ultimoRendimento", contaPoupanca.UltimoRendimento);

            var insertedId = Convert.ToInt32(cmd.ExecuteScalar());
            contaPoupanca.Id = insertedId;

            return contaPoupanca;
        }
    }
}
