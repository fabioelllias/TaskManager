namespace TaskManager.ViewModel.Projeto
{
    public class TarefaNovaViewModel
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public DateTime DataVencimento { get; set; }
        public string Prioridade { get; set; }
        public string Status { get; set; }
    }
}
