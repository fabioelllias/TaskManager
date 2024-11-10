using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.ViewModel.Usuario
{
    public class DesempenhoViewModel
    {
        public int PeriodoEmDias { get; set; }
        public List<DesempenhoItemViewModel> DesempenhoPorUsuario { get; set; } = new List<DesempenhoItemViewModel>();
    }
}
