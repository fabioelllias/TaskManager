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
        /// Listagem de Projetos - listar todos os projetos do usu�rio
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet, Route("{usuarioId}/projetos")]
        public async Task<IActionResult> GetProjetos(int usuarioId)
        {
            return Ok(usuarioId);       
        }

        /// <summary>
        /// Relat�rios de Desempenho - retornar m�dia de tarefas de usu�rios em um per�odo informado
        /// </summary>
        /// <param name="gerenteId"></param>
        /// <param name="dias"></param>
        /// <returns></returns>
        [HttpGet, Route("{gerenteId}/media-tarefas/{dias}")]
        public async Task<IActionResult> GetMediaTarefas(int gerenteId, int dias)
        {
            return Ok(gerenteId);
        }

        /// <summary>
        /// Coment�rios nas Tarefas - incluir coment�rio em uma tarefa
        /// </summary>
        /// <param name="comentario"></param>
        /// <returns></returns>
        [HttpPatch, Route("{usuarioId}/tarefa/comentario")]
        public async Task<IActionResult> PatchComentario([FromRoute] int usuarioId, [FromBody] string comentario)
        {
            return Ok(comentario);
        }



    }
}
