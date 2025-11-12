using Malvader.Models;
using MySql.Data.MySqlClient;

namespace Malvader.DAOs
{
    public class ContaCorrenteDAO
    {
        private readonly DbConnectionFactory _dbConnectionFactory;

        public ContaCorrenteDAO(DbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public ContaCorrente Insert(ContaCorrente contaCorrente)
        {
            using var conn = _dbConnectionFactory.CreateConnection();
            conn.Open();

            string sql = @"
                INSERT INTO conta_corrente (id_conta, data_vencimento)
                VALUES (@contaId, @dataVencimento);
                SELECT LAST_INSERT_ID();
            ";

            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@contaId", contaCorrente.ContaId);
            cmd.Parameters.AddWithValue("@dataVencimento", contaCorrente.DataVencimento);

            var insertedId = Convert.ToInt32(cmd.ExecuteScalar());
            contaCorrente.Id = insertedId;

            return contaCorrente;
        }
    }
}
