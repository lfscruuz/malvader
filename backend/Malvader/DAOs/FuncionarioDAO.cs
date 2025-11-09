using Malvader.DAO;
using Malvader.Models;
using MySql.Data.MySqlClient;

namespace Malvader.DAOs
{
    public class FuncionarioDAO
    {
        private readonly DbConnectionFactory _connectionFactory;

        public FuncionarioDAO(DbConnectionFactory connectionFactory, UsuarioDAO suarioDao)
        {
            _connectionFactory = connectionFactory;
        }

        public Funcionario Inserir(Funcionario funcionario) 
        {
            using var conn = _connectionFactory.CreateConnection();
            conn.Open();

            string sql = @"
                INSERT INTO funcionario (id_usuario, id_agencia, codigo_funcionario, cargo, id_Supervisor)
                VALUES (@usuarioId, @agenciaId, @codigoFuncionario, @cargo, @supervisorId);
                SELECT LAST_INSERT_ID();
            ";

            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@usuarioId", funcionario.UsuarioId);
            cmd.Parameters.AddWithValue("@agenciaId", funcionario.AgenciaId);
            cmd.Parameters.AddWithValue("@codigoFuncionario", funcionario.CodigoFuncionario);
            cmd.Parameters.AddWithValue("@cargo", funcionario.Cargo.ToString());
            cmd.Parameters.AddWithValue("@supervisorId", funcionario.UsuarioId);

            var insertedId = Convert.ToInt32(cmd.ExecuteScalar());
            funcionario.Id = insertedId;

            return funcionario;
        }
    }
}
