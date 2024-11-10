using TaskManager.Application.Interfaces;
using TaskManager.ViewModel.Projeto;

namespace TaskManager.Application
{
    public class ProjetoApplication : IProjetoApplication
    {
        public ActionResult AtualizarTarefa(int projetoId, int tarefaId, TarefaViewModel tarefaViewModel)
        {
            return ActionResult.Create(true, string.Empty, null);
        }

        public ActionResult CriarProjeto(ProjetoViewModel projetoViewModel)
        {
            return ActionResult.Create(true, string.Empty, null);
        }

        public ActionResult CriarTarefa(int projetoId, TarefaViewModel tarefaViewModel)
        {
            return ActionResult.Create(true, string.Empty, null);
        }

        public ActionResult ExcluirProjeto(int projetoId)
        {
            return ActionResult.Create(true, string.Empty, null);
        }

        public ActionResult ExcluirTarefa(int projetoId, int tarefaId)
        {
            return ActionResult.Create(true, string.Empty, null);
        }

        public ActionResult TarefasPorProjeto(int projetoId)
        {
            return ActionResult.Create(true, string.Empty, null);
        }
    }
}
