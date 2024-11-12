using TaskManager.Domain.Entitys;
using TaskManager.ViewModel.Projeto;
using TaskManager.ViewModel.Tarefa;

namespace TaskManager.Application.Mapper
{
    public static class ProjetoMapper
    {
        internal static List<TarefaViewModel> MapToTarefaViewModelList(IReadOnlyCollection<Tarefa> tarefas)
        {
            return tarefas.Select(item => new TarefaViewModel(item.Id, item.Titulo, item.Descricao, item.DataVencimento, item.Status.ToString(), item.Prioridade.ToString())).ToList();
        }

        internal static TarefaAtualizarViewModel MapToTarefaAtualizarViewModel(Tarefa tarefa)
        {
            return new TarefaAtualizarViewModel { DataVencimento = tarefa.DataVencimento, Descricao = tarefa.Descricao, Status = (int)tarefa.Status, Titulo = tarefa.Titulo };
        }

        internal static ProjetoViewModel MapToProjetoViewModel(Projeto projeto)
        {
            return new ProjetoViewModel { Id = projeto.Id, Titulo = projeto.Titulo, UsuarioId = projeto.UsuarioId };
        }
    }
}
