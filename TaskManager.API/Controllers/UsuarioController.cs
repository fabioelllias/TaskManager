using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.Interfaces;

namespace TaskManager.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly ILogger<UsuarioController> _logger;
        private readonly IUsuarioApplication _usuarioApplication;

        public UsuarioController(ILogger<UsuarioController> logger, IUsuarioApplication usuarioApplication)
        {
            _logger = logger;
            _usuarioApplication = usuarioApplication;
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
            return Ok(_usuarioApplication.ProjetosPorUsuario(usuarioId));
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
            return Ok(_usuarioApplication.DesempenhoNoPeriodo(gerenteId, dias));
        }

        /// <summary>
        /// Comentários nas Tarefas
        /// </summary>
        /// <remarks>Incluir comentário em uma tarefa</remarks>
        /// <param name="comentario"></param>
        /// <returns></returns>
        [HttpPatch, Route("{usuarioId}/tarefa/{tarefaId}/comentario")]
        public async Task<IActionResult> PatchComentario([FromRoute] int usuarioId, [FromRoute] int tarefaId, [FromBody] string comentario)
        {
            return Ok(_usuarioApplication.IncluirComentarioNaTarefa(usuarioId, tarefaId, comentario));
        }
    }
}
