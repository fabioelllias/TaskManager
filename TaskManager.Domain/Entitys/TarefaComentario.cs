using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
