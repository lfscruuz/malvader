using Malvader.Models;
using MySql.Data.MySqlClient;

namespace Malvader.DAOs
{
    public class ContaInvestimentoDAO
    {
        private readonly DbConnectionFactory _dbConnectionFactory;

        public ContaInvestimentoDAO(DbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }
        public ContaInvestimento Insert(ContaInvestimento contaInvestimento)
        {
            using var conn = _dbConnectionFactory.CreateConnection();
            conn.Open();

            string sql = @"
                INSERT INTO conta_investimento (id_conta, perfil_risco, valor_minimo, taxa_rendimento_base)
                VALUES (@contaId, @perfilRisco, @valorMinimo, @taxaRendimentoBase);
                SELECT LAST_INSERT_ID();
            ";

            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@contaId", contaInvestimento.ContaId);
            cmd.Parameters.AddWithValue("@perfilRisco", contaInvestimento.PerfilRisco.ToString());
            cmd.Parameters.AddWithValue("@valorMinimo", contaInvestimento.ValorMinimo);
            cmd.Parameters.AddWithValue("@taxaRendimentoBase", contaInvestimento.TaxaRendimentoBase);

            var insertedId = Convert.ToInt32(cmd.ExecuteScalar());
            contaInvestimento.Id = insertedId;

            return contaInvestimento;
        }
    }
}
