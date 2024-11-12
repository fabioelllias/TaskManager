using System.Linq.Expressions;
using TaskManager.Domain.Enuns;

namespace TaskManager.Domain.Entitys
{
    public class Usuario : BaseEntity
    {
        private readonly List<Projeto> _projetos = new List<Projeto>();
        public IReadOnlyCollection<Projeto> Projetos => _projetos.AsReadOnly();
        public Usuario(string nome, Funcao funcao)
        {
            Nome = nome;
            Funcao = funcao;
        }

        public string Nome { get; private set; }
        public Funcao Funcao { get; private set; }

        public int QuantidadeTarefasConcluidas(int periodoEmDias)
        {
            return _projetos.Sum(p => p.TarefasConcluidas(periodoEmDias));
        }

        //public static Expression<Func<Usuario, ICollection<Projeto>>> ProjetoMapping
        //{
        //    get { return c => c._projetos; }
        //}
    }
}
