using TaskManager.Application.Interfaces;
using TaskManager.ViewModel.Usuario;

namespace TaskManager.Application
{
    public class UsuarioApplication : IUsuarioApplication
    {
        public ActionResult DesempenhoNoPeriodo(int gerenteId, int numeroDias)
        {
            return ActionResult.Create(true, string.Empty, null);
        }

        public ActionResult IncluirComentarioNaTarefa(int usuarioId, string comentario)
        {
            return ActionResult.Create(true, string.Empty, null);
        }

        public ActionResult ProjetosPorUsuario(int usuarioId)
        {
            return ActionResult.Create(true, string.Empty, new List<ProjetoViewModel>());
        }
    }
}