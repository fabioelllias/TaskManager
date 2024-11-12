using TaskManager.Domain.Enuns;

namespace TaskManager.Domain.Entitys
{
    public class Projeto : BaseEntity
    {
        private readonly List<Tarefa> _tarefas = new();

        public Projeto()
        {

        }
        public Projeto(string titulo, int usuarioId)
        {
            Titulo = titulo;
            UsuarioId = usuarioId;

            if (string.IsNullOrEmpty(Titulo))
                AddError("Titulo", "Título não informado");
        }

        public string Titulo { get; private set; }
        public int UsuarioId { get; private set; }

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

            if (!tarefa.IsValid)
            {
                base.AddError(tarefa.GetErrors());
                return;
            }

            _tarefas.Add(tarefa);
        }


        public void AdicionarComentario(int tarefa, int usuario, string comentario)
        {
            var entity = _tarefas.SingleOrDefault(item => item.Id == tarefa);
            if (entity == null)
            {
                AddError("Tarefa", "Tarefa não encontrada.");
                return;
            }
            entity.AdicionarComentario(comentario, usuario);

            if (!entity.IsValid)
                AddError(entity.GetErrors());
        }

        public int TarefasConcluidas(int periodoEmDias)
        {
            return _tarefas.Count(t => t.Status == Enuns.Status.Concluida &&
                                  t.DataVencimento >= DateTime.Now.AddDays(-periodoEmDias));
        }

        public bool PossuiTarefasPendentes()
        {
            return _tarefas.Any(item => item.Status == Enuns.Status.Pendente);
        }

        public void RemoverTarefa(int tarefaId)
        {
            var tarefa = _tarefas.SingleOrDefault(item => item.Id == tarefaId);
            if (tarefa == null)
            {
                AddError("tarefaId", "Tarefa não encontrada.");
                return;
            }

            tarefa.RemoverTodosComentarios();
            tarefa.RemoverTodoHistorico();
            _tarefas.Remove(tarefa);
        }

        public void AtualizarTarefa(int tarefaId, string titulo, string descricao, DateTime dataVencimento, int status)
        {
            var tarefa = _tarefas.SingleOrDefault(item => item.Id == tarefaId);
            if (tarefa == null)
            {
                AddError("Tarefa", "Tarefa não encontrada.");
                return;
            }

            if (!string.IsNullOrEmpty(titulo)) tarefa.AddTitulo(titulo);
            if (!string.IsNullOrEmpty(descricao)) tarefa.AddDescricao(descricao);
            if (dataVencimento > DateTime.MinValue) tarefa.AddDataVencimento(dataVencimento);
            if (Enum.IsDefined(typeof(Status), status)) tarefa.AddStatus((Status)status);
        }

        public void AdicionarHistorico(int tarefaId, int usuarioId, List<string> diferencas)
        {
            var tarefa = _tarefas.SingleOrDefault(item => item.Id == tarefaId);
            foreach (var diferenca in diferencas)
                tarefa.AdicionarHistorico(usuarioId, diferenca);
        }
    }
}