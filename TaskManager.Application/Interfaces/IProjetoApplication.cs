using TaskManager.ViewModel.Projeto;

namespace TaskManager.Application.Interfaces
{
    public interface IProjetoApplication
    {
        ActionResult TarefasPorProjeto(int projetoId);
        ActionResult CriarProjeto(ProjetoViewModel projetoViewModel);
        ActionResult CriarTarefa(int projetoId, TarefaViewModel tarefaViewModel);
        ActionResult AtualizarTarefa(int projetoId, int tarefaId, TarefaViewModel tarefaViewModel);
        ActionResult ExcluirTarefa(int projetoId, int tarefaId);
        ActionResult ExcluirProjeto(int projetoId);
    }
}
