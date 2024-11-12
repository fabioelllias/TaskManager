using TaskManager.Application.Interfaces;
using TaskManager.Application.Mapper;
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

            var usuario = _usuarioRepository.GetById(gerenteId, "Projetos");
            if (usuario == null)
                return ActionResult.Create(false, "", "Usuário não encontrado.");


            if (usuario.Funcao != Domain.Enuns.Funcao.Gerente)
                return ActionResult.Create(false, "", "Funcionalidade acessível somente para usuarios com perfil Gerente");


            if (numeroDias < 1)
                return ActionResult.Create(false, "", "Numero de dias deve ser maior ou igual a 1.");

           
            var resultado = _usuarioRepository.GetAll()
                .Select(item => new { item.Nome, Quantidade = item.QuantidadeTarefasConcluidas(numeroDias) })
                .ToList();

            return ActionResult.Create(true, string.Empty, resultado);
        }

        public ActionResult ProjetosPorUsuario(int usuarioId)
        {
            var usuario = _usuarioRepository.GetById(usuarioId, "Projetos");
            if (usuario == null)
                return ActionResult.Create(false, "", "Usuário não encontrado.");

            return ActionResult.Create(true, "", UsuarioMapper.MapToProjetoViewModelList(usuario.Projetos));
        }
    }
}