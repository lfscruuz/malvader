using Malvader.DAOs;
using Malvader.DTOs.RequestDTOs.Create;
using Malvader.Models;

namespace Malvader.Services
{
    public class AgenciaService
    {
        private readonly AgenciaDAO _agenciaDao;
        private readonly EnderecoAgenciaDAO _enderecoAgenciaDao;

        public AgenciaService(AgenciaDAO agenciaDao, EnderecoAgenciaDAO enderecoAgenciaDao)
        {
            _agenciaDao = agenciaDao;
            _enderecoAgenciaDao = enderecoAgenciaDao;
        }

        public (Agencia agencia, EnderecoAgencia endereco) CreateAgencia(CreateAgenciaRequestDTO requestDto)
        {
            var enderecoAgencia = CreateEnderecoAgencia(requestDto);
            var errors = new List<string>();

            if (string.IsNullOrEmpty(requestDto.Nome))
            {
                throw new ArgumentException("Nome é obrigatório.");
            }

            var novaAgencia = new Agencia
            {
                Nome = requestDto.Nome,
                CodigoAgencia = requestDto.CodigoAgencia,
                EnderecoAgenciaId = enderecoAgencia.Id,
            };

            novaAgencia = _agenciaDao.Insert(novaAgencia);
            return (novaAgencia, enderecoAgencia);
        }

        public (Agencia agencia, EnderecoAgencia enderecoAgencia) GetById(int id)
        {
            var agencia = _agenciaDao.GetById(id);
            var enderecoAgencia = GetEnderecoAgenciaById(agencia.EnderecoAgenciaId);
            return (agencia, enderecoAgencia);
        }

        public (Agencia, EnderecoAgencia) GetAgenciaIdByCodigo(string codigo)
        {
            var agencia = _agenciaDao.GetByCodigo(codigo);
            var enderecoAgencia = GetEnderecoAgenciaById(agencia.EnderecoAgenciaId);
            return (agencia, enderecoAgencia);
        }
        public List<Agencia> GetAllAgencias()
        {
            var agencias = _agenciaDao.GetAll();
            return agencias;
        }
        
        #region Private Methods
        private EnderecoAgencia CreateEnderecoAgencia(CreateAgenciaRequestDTO requestDto)
        {
            var errors = new List<string>();
            if (string.IsNullOrEmpty(requestDto.Cep))
            {
                errors.Add("Cep é obrigatório");
            }
            if (string.IsNullOrEmpty(requestDto.Local))
            {
                errors.Add("Local é obrigatório");
            }
            if (string.IsNullOrEmpty(requestDto.Numero.ToString()))
            {
                errors.Add("Numero é obrigatório");
            }
            if (string.IsNullOrEmpty(requestDto.Bairro))
            {
                errors.Add("Bairro é obrigatório");
            }
            if (string.IsNullOrEmpty(requestDto.Cidade))
            {
                errors.Add("Cidade é obrigatório");
            }
            if (string.IsNullOrEmpty(requestDto.Complemento))
            {
                errors.Add("Complemento é obrigatório");
            }

            if (errors.Any())
            {
                throw new ArgumentException(string.Join("\n", errors));
            }
            var novoEnderecoAgencia = new EnderecoAgencia
            {
                Cep = requestDto.Cep,
                Local = requestDto.Local,
                Numero = requestDto.Numero,
                Bairro = requestDto.Bairro,
                Cidade = requestDto.Cidade,
                Estado = requestDto.Estado,
                Complemento = requestDto.Complemento
            };

            novoEnderecoAgencia = _enderecoAgenciaDao.Insert(novoEnderecoAgencia);
            return novoEnderecoAgencia;
        }

        private EnderecoAgencia GetEnderecoAgenciaById(int id)
        {
            var errorResponse = new ErrorResponse();
            var enderecoAgencia = _enderecoAgenciaDao.GetById(id);
            return (enderecoAgencia);
        }
        #endregion
    }
}
