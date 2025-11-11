using Malvader.Models;

namespace Malvader.DAOs
{
    public class ContaCorrenteDAO
    {
        private readonly DbConnectionFactory _dbConnectionFactory;

        public ContaCorrenteDAO(DbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        //public ContaCorrente Insert(ContaCorrente conta)
        //{
        //}
    }
}
