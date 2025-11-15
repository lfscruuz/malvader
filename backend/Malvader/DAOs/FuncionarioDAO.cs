using Malvader.DAO;
using Malvader.Models;
using MySql.Data.MySqlClient;

namespace Malvader.DAOs
{
    public class FuncionarioDAO
    {
        private readonly DbConnectionFactory _dbConnectionFactory;
        private readonly UsuarioDAO _usuarioDao;

        public FuncionarioDAO(DbConnectionFactory connectionFactory, UsuarioDAO usuarioDao)
        {
            _dbConnectionFactory = connectionFactory;
            _usuarioDao = usuarioDao;
        }

        public Funcionario Insert(Funcionario funcionario) 
        {
            try
            {
                using var conn = _dbConnectionFactory.CreateConnection();
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
                cmd.Parameters.AddWithValue("@supervisorId", funcionario.SupervisorId);

                var insertedId = Convert.ToInt32(cmd.ExecuteScalar());
                funcionario.Id = insertedId;

                return funcionario;
            }
            catch (Exception ex)
            {
                _usuarioDao.DeleteById(funcionario.UsuarioId);
                throw;
            }
        }

        public Funcionario? GetById(int id)
        {
            using var conn = _dbConnectionFactory.CreateConnection();
            conn.Open();

            string sql = @"
                SELECT * FROM funcionario WHERE id_funcionario = @funcionarioId
            ";

            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@funcionarioId", id);

            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new Funcionario
                {
                    Id = reader.GetInt32("id_funcionario"),
                    UsuarioId = reader.GetInt32("id_usuario"),
                    AgenciaId = reader.GetInt32("id_agencia"),
                    CodigoFuncionario = reader.GetString("codigo_funcionario"),
                    Cargo = Enum.Parse<Cargo>(reader.GetString("cargo")),
                    SupervisorId = reader.IsDBNull(reader.GetOrdinal("id_supervisor")) ? null : reader.GetInt32("id_supervisor")
                };
            }

            return null;
        }
        public Funcionario? GetByUsuarioId(int id)
        {
            Console.WriteLine(id);
            using var conn = _dbConnectionFactory.CreateConnection();
            conn.Open();

            string sql = @"
                SELECT * FROM funcionario WHERE id_usuario = @usuarioId
            ";

            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@usuarioId", id);

            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new Funcionario
                {
                    Id = reader.GetInt32("id_funcionario"),
                    UsuarioId = reader.GetInt32("id_usuario"),
                    AgenciaId = reader.GetInt32("id_agencia"),
                    CodigoFuncionario = reader.GetString("codigo_funcionario"),
                    Cargo = Enum.Parse<Cargo>(reader.GetString("cargo")),
                    SupervisorId = reader.IsDBNull(reader.GetOrdinal("id_supervisor")) ? null : reader.GetInt32("id_supervisor")
                };
            }

            return null;
        }
    }
}
