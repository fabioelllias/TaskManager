using Microsoft.AspNetCore.Mvc;

namespace TaskManager.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProjetoController : ControllerBase
    {
        private readonly ILogger<ProjetoController> _logger;

        public ProjetoController(ILogger<ProjetoController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Visualização de Tarefas
        /// </summary>
        /// <remarks>Visualizar todas as tarefas de um projeto específico</remarks>
        /// <param name="projetoId"></param>
        /// <returns></returns>
        [HttpGet, Route("{projetoId}/tarefas")]
        public async Task<IActionResult> GetTarefas([FromRoute] int projetoId)
        {
            return Ok(projetoId);
        }

        /// <summary>
        /// Criação de Projetos
        /// </summary>
        /// <remarks>Criar um novo projeto</remarks>
        /// <param name="projetoViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> PostProjeto([FromBody] object projetoViewModel)
        {
            return Ok(projetoViewModel);
        }

        /// <summary>
        /// Criação de Tarefas
        /// </summary>
        /// <remarks>Adicionar uma nova tarefa a um projeto</remarks>
        /// <param name="projetoId"></param>
        /// <param name="tarefaViewModel"></param>
        /// <returns></returns>
        [HttpPost, Route("{projetoId}/tarefa")]
        public async Task<IActionResult> PostTarefa([FromRoute] int projetoId, [FromBody] object tarefaViewModel)
        {
            return Ok(projetoId);
        }

        /// <summary>
        /// Atualização de Tarefas
        /// </summary>
        /// <remarks>Atualizar o status ou detalhes de uma tarefa</remarks>
        /// <param name="projetoId"></param>
        /// <param name="tarefaId"></param>
        /// <param name="tarefaViewModel"></param>
        /// <returns></returns>
        [HttpPut, Route("{projetoId}/tarefa/{tarefaId}")]
        public async Task<IActionResult> PutTarefa([FromRoute] int projetoId, [FromRoute] int tarefaId, [FromBody] object tarefaViewModel)
        {
            return Ok(projetoId);
        }

        /// <summary>
        /// Remoçao de Projetos
        /// </summary>
        /// <remarks>Remover um projeto</remarks>
        /// <param name="projetoId"></param>
        /// <returns></returns>
        [HttpDelete, Route("{projetoId}")]
        public async Task<IActionResult> DeleteProjeto([FromRoute] int projetoId)
        {
            return Ok(projetoId);
        }

        /// <summary>
        /// Remoção de Tarefas
        /// </summary>        
        /// <remarks>remover uma tarefa de um projeto</remarks>
        /// <param name="projetoId"></param>
        /// <param name="tarefaId"></param>
        /// <returns></returns>
        [HttpDelete, Route("{projetoId}/tarefa/{tarefaId}")]
        public async Task<IActionResult> DeleteTarefa([FromRoute] int projetoId, [FromRoute] int tarefaId)
        {
            return Ok(projetoId);
        }
    }
}
