using TaskManager.ViewModel.Projeto;

namespace TaskManager.Application.Interfaces
{
    public interface IProjetoApplication
    {
        ActionResult TarefasPorProjeto(int projetoId);
        ActionResult CriarProjeto(ProjetoViewModel projetoViewModel);
        ActionResult CriarTarefa(int projetoId, TarefaAtualizarViewModel tarefaViewModel);
        ActionResult AtualizarTarefa(int projetoId, int tarefaId, TarefaAtualizarViewModel tarefaViewModel);
        ActionResult IncluirComentarioNaTarefa(int projetoId, int usuarioId, int tarefaId, string comentario);
        ActionResult ExcluirTarefa(int projetoId, int tarefaId);
        ActionResult ExcluirProjeto(int projetoId);
    }
}
