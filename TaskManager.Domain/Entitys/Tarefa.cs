using TaskManager.Domain.Enuns;

namespace TaskManager.Domain.Entitys
{
    public class Tarefa : BaseEntity
    {
        private readonly List<TarefaComentario> _comentarios = new();
        private readonly List<TarefaHistorico> _historico = new();

        public Tarefa(string titulo, string descricao, DateTime dataVencimento, Status status, Prioridade prioridade)
        {
            Titulo = titulo;
            Descricao = descricao;
            DataVencimento = dataVencimento;
            Status = status;
            Prioridade = prioridade;
        }

        public string Titulo { get; private set; }
        public string Descricao { get; private set; }
        public DateTime DataVencimento { get; private set; }
        public Status Status { get; private set; }
        public Prioridade Prioridade { get; private set; }
        public Projeto Projeto { get; private set; }

        public IReadOnlyCollection<TarefaComentario> Comentarios => _comentarios.AsReadOnly();
        public IReadOnlyCollection<TarefaHistorico> Historico => _historico.AsReadOnly();

        internal void AddDataVencimento(DateTime dataVencimento)
        {
            if(dataVencimento < DateTime.Now)
            {
                AddError("dataVencimento", "Data de vencimento não informada.");
                return;
            }
            this.DataVencimento = dataVencimento;
        }

        internal void AddDescricao(string descricao)
        {
            if (string.IsNullOrEmpty(descricao))
            {
                AddError("descricao", "Descrição da tarefa não informada.");
                return;
            }
            this.Descricao = descricao;
        }

        internal void AdicionarComentario(string comentario)
        {
            _comentarios.Add(new TarefaComentario(comentario));
        }

        internal void AdicionarHistorico(TarefaHistorico historico)
        {
            _historico.Add(historico);
        }
    }
}
