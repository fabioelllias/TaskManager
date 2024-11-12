using TaskManager.Domain.Enuns;

namespace TaskManager.Domain.Entitys
{
    public class Tarefa : BaseEntity
    {
        private readonly List<TarefaComentario> _comentarios = new();
        private readonly List<TarefaHistorico> _historico = new();

        public Tarefa(string titulo, string descricao, DateTime dataVencimento, Status status, Prioridade prioridade, int projetoId)
        {
            Titulo = titulo;
            Descricao = descricao;
            DataVencimento = dataVencimento;
            Status = status;
            Prioridade = prioridade;
            ProjetoId = projetoId;

            if (string.IsNullOrEmpty(Titulo)) AddError("Titulo", "Titulo não informado.");
            if (string.IsNullOrEmpty(Descricao)) AddError("Descricao", "Descrição não informada.");
            if (DataVencimento == DateTime.MinValue) AddError("DataVencimento", "Data de vencimento não informada.");
            if (!Enum.IsDefined(typeof(Status), status)) AddError("Status", "Status informado é inválido.");
            if (!Enum.IsDefined(typeof(Prioridade), prioridade)) AddError("Prioridade", "Prioridade informada é inválida.");

        }

        public string Titulo { get; private set; }
        public string Descricao { get; private set; }
        public DateTime DataVencimento { get; private set; }
        public Status Status { get; private set; }
        public Prioridade Prioridade { get; private set; }
        public int ProjetoId { get; private set; }

        public IReadOnlyCollection<TarefaComentario> Comentarios => _comentarios.AsReadOnly();
        public IReadOnlyCollection<TarefaHistorico> Historico => _historico.AsReadOnly();

        internal void AddDataVencimento(DateTime dataVencimento)
        {
            if (dataVencimento < DateTime.Now)
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

        internal void AdicionarComentario(string comentario, int usuario)
        {
            if (string.IsNullOrEmpty(comentario))
            {
                AddError("comentario", "Comentário da tarefa não informado.");
                return;
            }

            _comentarios.Add(new TarefaComentario(comentario, usuario));
            _historico.Add(new TarefaHistorico($"Comentário incluido:{comentario}", usuario, DateTime.Now.ToUniversalTime()));
        }

        internal void AdicionarHistorico(TarefaHistorico historico)
        {
            _historico.Add(historico);
        }
    }
}
