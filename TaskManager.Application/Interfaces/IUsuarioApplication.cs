using TaskManager.ViewModel.Usuario;

namespace TaskManager.Application.Interfaces
{
    public interface IUsuarioApplication
    {
        ActionResult ProjetosPorUsuario(int usuarioId);
        ActionResult DesempenhoNoPeriodo(int gerenteId, int numeroDias);
        ActionResult IncluirComentarioNaTarefa(int usuarioId, int tarefaId, string comentario);
    }
}
