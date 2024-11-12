namespace TaskManager.Domain.Entitys
{
    public class TarefaComentario : BaseEntity
    {
        public string Comentario { get; private set; }
        public int UsuarioId { get; private set; }

        public TarefaComentario(string comentario, int usuarioId)
        {
            Comentario = comentario;
            UsuarioId = usuarioId;
        }
    }
}
