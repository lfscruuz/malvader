using Malvader.DAO;
using Malvader.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Malvader.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioDAO _usuarioDao;

        public UsuarioController(UsuarioDAO usuarioDao)
        {
            _usuarioDao = usuarioDao;
        }

        [HttpGet]
        public ActionResult<List<Usuario>> GetAllUsuarios()
        {
            var usuarios = _usuarioDao.ListarTodos();
            return Ok(usuarios);
        }
    }
}
