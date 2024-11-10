using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.Interfaces;
using TaskManager.ViewModel.Projeto;

namespace TaskManager.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProjetoController : ControllerBase
    {
        private readonly ILogger<ProjetoController> _logger;
        private readonly IProjetoApplication _projetoApplication;

        public ProjetoController(ILogger<ProjetoController> logger, IProjetoApplication projetoApplication)
        {
            _logger = logger;
            _projetoApplication = projetoApplication;
        }

        /// <summary>
        /// Visualiza��o de Tarefas
        /// </summary>
        /// <remarks>Visualizar todas as tarefas de um projeto espec�fico</remarks>
        /// <param name="projetoId"></param>
        /// <returns></returns>
        [HttpGet, Route("{projetoId}/tarefas")]
        public async Task<IActionResult> GetTarefas([FromRoute] int projetoId)
        {
            return Ok(_projetoApplication.TarefasPorProjeto(projetoId));
        }

        /// <summary>
        /// Cria��o de Projetos
        /// </summary>
        /// <remarks>Criar um novo projeto</remarks>
        /// <param name="projetoViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> PostProjeto([FromBody] ProjetoViewModel projetoViewModel)
        {
            return Ok(_projetoApplication.CriarProjeto(projetoViewModel));
        }

        /// <summary>
        /// Cria��o de Tarefas
        /// </summary>
        /// <remarks>Adicionar uma nova tarefa a um projeto</remarks>
        /// <param name="projetoId"></param>
        /// <param name="tarefaViewModel"></param>
        /// <returns></returns>
        [HttpPost, Route("{projetoId}/tarefa")]
        public async Task<IActionResult> PostTarefa([FromRoute] int projetoId, [FromBody] TarefaViewModel tarefaViewModel)
        {
            return Ok(_projetoApplication.CriarTarefa(projetoId, tarefaViewModel));
        }

        /// <summary>
        /// Atualiza��o de Tarefas
        /// </summary>
        /// <remarks>Atualizar o status ou detalhes de uma tarefa</remarks>
        /// <param name="projetoId"></param>
        /// <param name="tarefaId"></param>
        /// <param name="tarefaViewModel"></param>
        /// <returns></returns>
        [HttpPut, Route("{projetoId}/tarefa/{tarefaId}")]
        public async Task<IActionResult> PutTarefa([FromRoute] int projetoId, [FromRoute] int tarefaId, [FromBody] TarefaViewModel tarefaViewModel)
        {
            return Ok(_projetoApplication.AtualizarTarefa(projetoId, tarefaId, tarefaViewModel));
        }

        /// <summary>
        /// Remo�ao de Projetos
        /// </summary>
        /// <remarks>Remover um projeto</remarks>
        /// <param name="projetoId"></param>
        /// <returns></returns>
        [HttpDelete, Route("{projetoId}")]
        public async Task<IActionResult> DeleteProjeto([FromRoute] int projetoId)
        {
            return Ok(_projetoApplication.ExcluirProjeto(projetoId));
        }

        /// <summary>
        /// Remo��o de Tarefas
        /// </summary>        
        /// <remarks>remover uma tarefa de um projeto</remarks>
        /// <param name="projetoId"></param>
        /// <param name="tarefaId"></param>
        /// <returns></returns>
        [HttpDelete, Route("{projetoId}/tarefa/{tarefaId}")]
        public async Task<IActionResult> DeleteTarefa([FromRoute] int projetoId, [FromRoute] int tarefaId)
        {
            return Ok(_projetoApplication.ExcluirTarefa(projetoId, tarefaId));
        }
    }
}
