using Microsoft.AspNetCore.Mvc;

namespace TaskManager.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly ILogger<UsuarioController> _logger;

        public UsuarioController(ILogger<UsuarioController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Listagem de Projetos
        /// </summary>
        /// <remarks>Listar todos os projetos do usuário</remarks>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet, Route("{usuarioId}/projetos")]
        public async Task<IActionResult> GetProjetos(int usuarioId)
        {
            return Ok(usuarioId);       
        }

        /// <summary>
        /// Relatórios de Desempenho
        /// </summary>
        /// <remarks>Retornar média de tarefas de usuários em um período informado</remarks>
        /// <param name="gerenteId"></param>
        /// <param name="dias"></param>
        /// <returns></returns>
        [HttpGet, Route("{gerenteId}/media-tarefas/{dias}")]
        public async Task<IActionResult> GetMediaTarefas(int gerenteId, int dias)
        {
            return Ok(gerenteId);
        }

        /// <summary>
        /// Comentários nas Tarefas
        /// </summary>
        /// <remarks>Incluir comentário em uma tarefa</remarks>
        /// <param name="comentario"></param>
        /// <returns></returns>
        [HttpPatch, Route("{usuarioId}/tarefa/comentario")]
        public async Task<IActionResult> PatchComentario([FromRoute] int usuarioId, [FromBody] string comentario)
        {
            return Ok(comentario);
        }
    }
}
