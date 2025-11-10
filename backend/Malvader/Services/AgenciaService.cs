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

        public (Agencia? agencia, EnderecoAgencia? endereco, ErrorResponse? errorResponse) CriarAgencia(CreateAgenciaRequestDTO requestDto, List<string> errors)
        {
            var (enderecoAgencia, errorResponse) = CriarEnderecoAgencia(requestDto, errors);
            if (enderecoAgencia == null)
            {
                return (null, null, errorResponse);
            }

            if (string.IsNullOrEmpty(requestDto.Nome))
            {
                errors.Add("Nome é obrigatório");
            }
            if (errors.Any())
            {
                errorResponse = new ErrorResponse { Errors = errors };
                return (null, null, errorResponse);
            }
            var novaAgencia = new Agencia
            {
                Nome = requestDto.Nome,
                CodigoAgencia = requestDto.CodigoAgencia,
                EnderecoAgenciaId = enderecoAgencia.Id,
            };

            novaAgencia = _agenciaDao.Insert(novaAgencia);
            return (novaAgencia, enderecoAgencia, null);
        }

        public (Agencia? agencia, EnderecoAgencia? enderecoAgencia, ErrorResponse? errorResponse) GetById(int id)
        {
            var errorResponse = new ErrorResponse();
            var agencia = _agenciaDao.GetById(id);
            if (agencia == null) { 
                errorResponse.Errors.Add("Não foi possível encontrar a agencia com o ID informado");
                return (null, null, errorResponse);
            }
            (var enderecoAgencia, errorResponse) = GetEnderecoAgenciaById(agencia.EnderecoAgenciaId);
            if (enderecoAgencia == null)
            {
                errorResponse.Errors.Add("Não foi possível encontrar o endereço da agencia com o ID informado");
                return (null, null, errorResponse);
            }
            return (agencia, enderecoAgencia, null);
        }

        #region Private Methods
        private (EnderecoAgencia? agencia, ErrorResponse? errorResponse) CriarEnderecoAgencia(
            CreateAgenciaRequestDTO requestDto,
            List<string> errors)
        {
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
                var errorResponse = new ErrorResponse { Errors = errors };
                return (null,  errorResponse);
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
            return (novoEnderecoAgencia, null);
        }

        private (EnderecoAgencia? enderecoAgencia, ErrorResponse? errorResponse) GetEnderecoAgenciaById(int id)
        {
            var errorResponse = new ErrorResponse();
            var enderecoAgencia = _enderecoAgenciaDao.GetById(id);
            if (enderecoAgencia == null)
            {
                errorResponse.Errors.Add("Não foi possível encontrar o endereço da agencia com o ID informado");
                return (null, errorResponse);
            }
            return (enderecoAgencia, null);
        }
        #endregion
    }
}
