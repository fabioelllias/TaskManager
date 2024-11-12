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
        private readonly IRepository<Usuario> _usuarioRepository;

        public ProjetoApplication(IValidator validator, IRepository<Projeto> projetoRepository, IRepository<Usuario> usuarioRepository)
        {
            _validator = validator;
            _projetoRepository = projetoRepository;
            _usuarioRepository = usuarioRepository;
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
            var usuarioExiste = _usuarioRepository.GetAll().Any(item => item.Id == projetoViewModel.UsuarioId);
            if (!usuarioExiste)
                return ActionResult.Create(false, "Usuário não encontrado.", null);

            var projeto = new Projeto(projetoViewModel.Titulo, projetoViewModel.UsuarioId);

            if (!projeto.IsValid)
                return ActionResult.Create(false, "Operação não concluída.", projeto.GetErrors());

            _projetoRepository.Save(projeto);

            return ActionResult.Create(true, string.Empty, ProjetoMapper.MapToProjetoViewModel(projeto));
        }

        public ActionResult CriarTarefa(int projetoId, TarefaAtualizarViewModel tarefaViewModel)
        {
            return ActionResult.Create(true, string.Empty, null);
        }

        public ActionResult ExcluirProjeto(int projetoId)
        {
            var projeto = _projetoRepository.GetById(projetoId, "Tarefas");
            if (projeto == null)
                return ActionResult.Create(false, "Projeto não encontrado.", string.Empty);

            if (projeto.PossuiTarefasPendentes())
                return ActionResult.Create(false, "Exclusão não permitida.", "Projeto possui tarefas pendentes. Conclua ou exclua estas tarefas antes de remover o projeto");

            _projetoRepository.Delete(projeto.Id);

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
