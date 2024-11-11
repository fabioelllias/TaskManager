using TaskManager.Application.Interfaces;
using TaskManager.Domain.Entitys;
using TaskManager.Infrastructure.Interfaces;
using TaskManager.Shared.Interfaces;

namespace TaskManager.Application
{
    public class UsuarioApplication : IUsuarioApplication
    {
        private readonly IValidator _validator;
        private readonly IRepository<Usuario> _usuarioRepository;
        public UsuarioApplication(IValidator validator, IRepository<Usuario> usuarioRepository)
        {
            _validator = validator;
            _usuarioRepository = usuarioRepository;
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
            if (usuarioId == 0)
            {
                _validator.AddError("usuarioId", "Usuário não informado.");
                return ActionResult.Create(false, "", _validator.GetErrors());
            }

            var usuario =_usuarioRepository.GetById(usuarioId);
            if (usuario == null)
            {
                _validator.AddError("usuarioId", "Usuário não encontrado.");
                return ActionResult.Create(false, "", _validator.GetErrors());
            }

            return ActionResult.Create(true, "", usuario.Projetos);
            //return ActionResult.Create(true, string.Empty, new List<ProjetoViewModel>());
        }
    }
}