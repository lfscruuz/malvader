using Malvader.Models;

namespace Malvader.DAOs
{
    public class ContaPoupancaDAO
    {
        private readonly DbConnectionFactory _dbConnectionFactory;

        public ContaPoupancaDAO(DbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        //public ContaPoupanca Insert(ContaPoupanca contaPoupanca)
        //{
        //}
    }
}
