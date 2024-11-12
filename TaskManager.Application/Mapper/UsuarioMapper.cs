using TaskManager.Domain.Entitys;
using TaskManager.ViewModel.Projeto;

namespace TaskManager.Application.Mapper
{
    public static class UsuarioMapper
    {
        internal static List<ProjetoViewModel> MapToProjetoViewModelList(IReadOnlyCollection<Projeto> projetos)
        {
            return projetos.Select(item => new ProjetoViewModel { Id = item.Id, Titulo = item.Titulo, UsuarioId = item.UsuarioId }).ToList();
        }
    }
}
