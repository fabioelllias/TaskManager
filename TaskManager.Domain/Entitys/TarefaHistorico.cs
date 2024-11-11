namespace TaskManager.Domain.Entitys
{
    public class TarefaHistorico : BaseEntity
    {
        public string Alteracao { get; private set; }
        public int AlteradoPor { get; private set; }
        public DateTime AlteradoEm { get; private set; }
    }
}
