using Malvader.Models;

namespace Malvader.DAOs
{
    public class ContaInvestimentoDAO
    {
        private readonly DbConnectionFactory _dbConnectionFactory;

        public ContaInvestimentoDAO(DbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }
        //public ContaInvestimento Insert(ContaInvestimento contaInvestimento)
        //{
        //}
    }
}
