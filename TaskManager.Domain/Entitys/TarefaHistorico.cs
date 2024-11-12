namespace TaskManager.Domain.Entitys
{
    public class TarefaHistorico : BaseEntity
    {
        public TarefaHistorico(string alteracao, int alteradoPor, DateTime alteradoEm)
        {
            Alteracao = alteracao;
            AlteradoPor = alteradoPor;
            AlteradoEm = alteradoEm;
        }

        public string Alteracao { get; private set; }
        public int AlteradoPor { get; private set; }
        public DateTime AlteradoEm { get; private set; }
    }
}
