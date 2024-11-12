namespace TaskManager.ViewModel.Tarefa
{
    public class TarefaViewModel
    {
        public TarefaViewModel(string titulo, string descricao, DateTime dataVencimento, string status, string prioridade)
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
        public string Status { get; private set; }
        public string Prioridade { get; private set; }
    }
}
