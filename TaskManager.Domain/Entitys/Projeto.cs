using System.Linq.Expressions;

namespace TaskManager.Domain.Entitys
{
    public class Projeto : BaseEntity
    {
        private readonly List<Tarefa> _tarefas = new();
        public Projeto()
        {

        }
        public string Titulo { get; private set; }
        public Usuario AtribuidoA { get; private set; }

        public IReadOnlyCollection<Tarefa> Tarefas => _tarefas.AsReadOnly();

        public void AdicionarTarefa(Tarefa tarefa)
        {
            if (tarefa == null)
            {
                AddError("tarefa", "Tarefa não informada.");
                return;
            }
            if (_tarefas.Count == 20)
            {
                AddError("tarefa", "O limite de tarefas por projeto foi atingido: 20");
                return;
            }
            _tarefas.Add(tarefa);
        }

        public void AdicionarComentario(int tarefa, string comentario)
        {
            var entity = _tarefas.SingleOrDefault(item => item.Id == tarefa);
            if (entity == null)
            {
                AddError("Tarefa", "Tarefa não encontrada.");
                return;
            }
            entity.AdicionarComentario(comentario);
        }

        public void AtualizarTarefa(Tarefa tarefa)
        {
            var entity = _tarefas.SingleOrDefault(item => item.Id == tarefa.Id);
            if (entity == null)
            {
                AddError("Tarefa", "Tarefa não encontrada.");
                return;
            }

            if(entity.Prioridade != tarefa.Prioridade)
            {
                AddError("tarefa.Prioridade", "Não é permitido alterar a prioridade de uma tarefa depois que ela foi criada.");
                return;
            }

            entity.AddDescricao(tarefa.Descricao);
            entity.AddDataVencimento(tarefa.DataVencimento);

            //entity.GetErrors()

            entity.AdicionarHistorico(null);
        }

        public static Expression<Func<Projeto, ICollection<Tarefa>>> TarefaMapping
        {
            get { return c => c._tarefas; }
        }

    }
}
