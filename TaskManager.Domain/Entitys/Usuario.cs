using TaskManager.Domain.Enuns;

namespace TaskManager.Domain.Entitys
{
    public class Usuario : BaseEntity
    {
        private readonly List<Projeto> _projetos = new();
        public Usuario(string nome, Funcao funcao)
        {
            Nome = nome;
            Funcao = funcao;
        }

        public string Nome { get; private set; }
        public Funcao Funcao { get; private set; }
        public IReadOnlyCollection<Projeto> Projetos => _projetos.AsReadOnly();
    }
}
