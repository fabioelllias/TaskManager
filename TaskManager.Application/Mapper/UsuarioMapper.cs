using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Domain.Entitys;
using TaskManager.ViewModel.Projeto;

namespace TaskManager.Application.Mapper
{
    public static class UsuarioMapper
    {
        internal static List<ProjetoViewModel> MapToProjetoViewModelList(IReadOnlyCollection<Projeto> projetos)
        {
            return projetos.Select(item => new ProjetoViewModel { Titulo = item.Titulo }).ToList();
        }
    }
}
