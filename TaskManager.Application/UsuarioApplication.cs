using TaskManager.Application.Interfaces;
using TaskManager.ViewModel.Usuario;

namespace TaskManager.Application
{
    public class UsuarioApplication : IUsuarioApplication
    {
        private readonly IValidator _validator;
        public UsuarioApplication(IValidator validator)
        {
            _validator = validator;
        }
        public ActionResult DesempenhoNoPeriodo(int gerenteId, int numeroDias)
        {
            if (gerenteId == 0) _validator.AddError("usuarioId", "Usuário não informado.");
            if (numeroDias < 1) _validator.AddError("numeroDias", "Numero de dias deve ser maior ou igual a 1.");

            if (!_validator.IsValid)
                return ActionResult.Create(false, "", _validator.GetErrors());

            return ActionResult.Create(true, string.Empty, null);
        }

        public ActionResult IncluirComentarioNaTarefa(int usuarioId, int tarefaId, string comentario)
        {
            if (usuarioId == 0) _validator.AddError("usuarioId", "Usuário não informado.");
            if (tarefaId == 0) _validator.AddError("tarefaId", "Tarefa não informada.");
            if (string.IsNullOrEmpty(comentario)) _validator.AddError("comentario", "Comentário não informado.");

            if (!_validator.IsValid)
                return ActionResult.Create(false, "", _validator.GetErrors());

            return ActionResult.Create(true, string.Empty, null);
        }

        public ActionResult ProjetosPorUsuario(int usuarioId)
        {
            if (usuarioId == 0) _validator.AddError("usuarioId", "Usuário não informado.");
            if (!_validator.IsValid)
                return ActionResult.Create(false, "", _validator.GetErrors());

            return ActionResult.Create(true, string.Empty, new List<ProjetoViewModel>());
        }
    }
}