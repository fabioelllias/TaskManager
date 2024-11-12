﻿using TaskManager.Application.Interfaces;
using TaskManager.Application.Mapper;
using TaskManager.Domain.Entitys;
using TaskManager.Domain.Enuns;
using TaskManager.Infrastructure.Interfaces;
using TaskManager.Shared;
using TaskManager.Shared.Interfaces;
using TaskManager.ViewModel.Projeto;
using TaskManager.ViewModel.Tarefa;

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

        public ActionResult AtualizarTarefa(int projetoId, int usuarioId, int tarefaId, TarefaAtualizarViewModel tarefaViewModel)
        {
            var projeto = _projetoRepository.GetById(projetoId, "Tarefas");
            if (projeto == null)
                return ActionResult.Create(false, "Projeto não encontrado.", string.Empty);

            if (tarefaViewModel == null)
                return ActionResult.Create(false, "Informe os dados da tarefa a serem atualizados.", string.Empty);

            var usuarioExiste = _usuarioRepository.GetAll().Any(item => item.Id == usuarioId);
            if (!usuarioExiste)
                return ActionResult.Create(false, "Usuário não encontrado.", null);

            var tarefa = projeto.Tarefas.SingleOrDefault(item => item.Id == tarefaId);
            if(tarefa == null)
                return ActionResult.Create(false, "Tarefa não encontrada ou não pertence a este projeto.", null);

            var tarefaCadastrada = ProjetoMapper.MapToTarefaAtualizarViewModel(tarefa);

            var diferencas = Comparator.ObterDiferencas<TarefaAtualizarViewModel, TarefaAtualizarViewModel>(tarefaCadastrada , tarefaViewModel);

            if (!diferencas.Any())
                return ActionResult.Create(false, "Nenhuma informação da tarefa foi modificada.", null);

            projeto.AtualizarTarefa(tarefaId, tarefaViewModel.Titulo, tarefaViewModel.Descricao, tarefaViewModel.DataVencimento, tarefaViewModel.Status);
            projeto.AdicionarHistorico(tarefaId, usuarioId, diferencas);

            _projetoRepository.Save(projeto);

            return ActionResult.Create(true, string.Empty, null);
        }

        public ActionResult IncluirComentarioNaTarefa(int projetoId, int usuarioId, int tarefaId, string comentario)
        {
            var projeto = _projetoRepository.GetById(projetoId, "Tarefas");
            if (projeto == null)
                return ActionResult.Create(false, "Projeto não encontrado.", string.Empty);

            var usuarioExiste = _usuarioRepository.GetAll().Any(item => item.Id == usuarioId);
            if (!usuarioExiste)
                return ActionResult.Create(false, "Usuário não encontrado.", null);

            projeto.AdicionarComentario(tarefaId, usuarioId, comentario);

            if (!projeto.IsValid)
                return ActionResult.Create(false, "Operação não realizada.", projeto.GetErrors());

            _projetoRepository.Save(projeto);          

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

        public ActionResult CriarTarefa(int projetoId, TarefaCriarViewModel tarefaViewModel)
        {
            var projeto = _projetoRepository.GetById(projetoId, "Tarefas");
            if (projeto == null)
                return ActionResult.Create(false, "Projeto não encontrado.", string.Empty);

            projeto.AdicionarTarefa(new Tarefa(
             tarefaViewModel.Titulo,
             tarefaViewModel.Descricao,
             tarefaViewModel.DataVencimento,
             (Status)tarefaViewModel.Status,
             (Prioridade)tarefaViewModel.Prioridade,
             projeto.Id));

            if (!projeto.IsValid)
                return ActionResult.Create(false, "Operação não concluída.", projeto.GetErrors());


            _projetoRepository.Save(projeto);

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
            var projeto = _projetoRepository.GetById(projetoId, "Tarefas");
            if (projeto == null)
                return ActionResult.Create(false, "Projeto não encontrado.", string.Empty);

            projeto.RemoverTarefa(tarefaId);


            if(!projeto.IsValid)
                return ActionResult.Create(false, "Exclusão não permitida.", projeto.GetErrors());

            _projetoRepository.Save(projeto);


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
