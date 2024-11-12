using TaskManager.Application.Interfaces;
using TaskManager.Application.Mapper;
using TaskManager.Domain.Entitys;
using TaskManager.Domain.Enuns;
using TaskManager.Infrastructure.Interfaces;
using TaskManager.Shared.Interfaces;
using TaskManager.ViewModel.Projeto;

namespace TaskManager.Application
{
    public class ProjetoApplication : IProjetoApplication
    {
        private readonly IValidator _validator;
        private readonly IRepository<Projeto> _projetoRepository;

        public ProjetoApplication(IValidator validator, IRepository<Projeto> projetoRepository)
        {
            _validator = validator;
            _projetoRepository = projetoRepository;
        }

        public ActionResult AtualizarTarefa(int projetoId, int tarefaId, TarefaAtualizarViewModel tarefaViewModel)
        {
            if (projetoId == 0) _validator.AddError("projetoId", "Projeto não informado.");
            if (tarefaId == 0) _validator.AddError("tarefaId", "Tarefa não informada.");
            if (tarefaViewModel == null)
                _validator.AddError("tarefaViewModel", "Informe os dados a serem atualizados na tarefa.");
            else
            {
                if (string.IsNullOrEmpty(tarefaViewModel.Titulo)) _validator.AddError("tarefaViewModel.Titulo", "Titulo não informado.");
                if (string.IsNullOrEmpty(tarefaViewModel.Descricao)) _validator.AddError("tarefaViewModel.Descricao", "Descrição não informada.");
                if (tarefaViewModel.DataVencimento < DateTime.Now) _validator.AddError("tarefaViewModel.DataVencimento", "A data de vencimento não pode ser anterior ao dia corrente.");
                if (!Enum.IsDefined(typeof(Prioridade), tarefaViewModel.Prioridade)) _validator.AddError("tarefaViewModel.Prioridade", "Prioridade informada inválida.");
            }

            if (!_validator.IsValid)
                return ActionResult.Create(false, "", _validator.GetErrors());

            return ActionResult.Create(true, string.Empty, null);
        }

        public ActionResult IncluirComentarioNaTarefa(int projetoId, int usuarioId, int tarefaId, string comentario)
        {
            if (usuarioId == 0) _validator.AddError("usuarioId", "Usuário não informado.");
            if (tarefaId == 0) _validator.AddError("tarefaId", "Tarefa não informada.");
            if (string.IsNullOrEmpty(comentario)) _validator.AddError("comentario", "Comentário não informado.");

            if (!_validator.IsValid)
                return ActionResult.Create(false, "", _validator.GetErrors());

            return ActionResult.Create(true, string.Empty, null);
        }

        public ActionResult CriarProjeto(ProjetoViewModel projetoViewModel)
        {
            return ActionResult.Create(true, string.Empty, null);
        }

        public ActionResult CriarTarefa(int projetoId, TarefaAtualizarViewModel tarefaViewModel)
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
            var projeto = _projetoRepository.GetById(projetoId, "Tarefas");
            if (projeto == null)
                return ActionResult.Create(false, "Projeto não encontrado.", string.Empty);

            return ActionResult.Create(true, string.Empty, ProjetoMapper.MapToTarefaViewModelList(projeto.Tarefas));
        }
    }
}
