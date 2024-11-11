namespace TaskManager.Domain.Entitys
{
    public class TarefaComentario : BaseEntity
    {
        public string Comentario { get; private set; }

        public TarefaComentario(string comentario)
        {
            Comentario = comentario;
        }
    }
}
