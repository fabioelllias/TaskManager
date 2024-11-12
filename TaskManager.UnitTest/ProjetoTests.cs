using TaskManager.Domain.Entitys;
using TaskManager.Domain.Enuns;

namespace TaskManager.UnitTest
{
    [TestFixture]
    public class ProjetoTests
    {
        private Projeto _projeto;

        [SetUp]
        public void SetUp()
        {
            _projeto = new Projeto("Projeto Teste", 1);
        }

        [Test]
        public void Constructor_DeveRetornarErro_QuandoTituloEstaVazio()
        {
            // Arrange & Act
            var projeto = new Projeto("", 1);

            // Assert
            Assert.IsTrue(projeto.IsValid == false);
            Assert.AreEqual("Título não informado", projeto.GetErrors().FirstOrDefault().Value);
        }

        [Test]
        public void AdicionarTarefa_DeveAdicionarTarefa_QuandoValido()
        {
            // Arrange
            var tarefa = new Tarefa("Tarefa Teste", "Descrição", DateTime.Now.AddDays(1), Status.Pendente, Prioridade.Alta, _projeto.Id);

            // Act
            _projeto.AdicionarTarefa(tarefa);

            // Assert
            Assert.Contains(tarefa, _projeto.Tarefas.ToList());
        }

        [Test]
        public void AdicionarTarefa_DeveRetornarErro_QuandoTarefaIsNull()
        {
            // Act
            _projeto.AdicionarTarefa(null);

            // Assert
            Assert.IsTrue(_projeto.IsValid == false);
            Assert.AreEqual("Tarefa não informada.", _projeto.GetErrors().FirstOrDefault().Value);
        }

        [Test]
        public void AdicionarTarefa_DeveRetornarErro_QuandoLimiteDeTarefasAtingido()
        {
            // Arrange
            for (int i = 0; i < 20; i++)
                _projeto.AdicionarTarefa(new Tarefa("Tarefa " + i, "Descrição", DateTime.Now.AddDays(1), Status.Pendente, Prioridade.Alta, _projeto.Id));

            // Act
            _projeto.AdicionarTarefa(new Tarefa("Tarefa Excedente", "Descrição", DateTime.Now.AddDays(1), Status.Pendente, Prioridade.Alta, _projeto.Id));

            // Assert
            Assert.IsTrue(_projeto.IsValid == false);
            Assert.AreEqual("O limite de tarefas por projeto foi atingido: 20", _projeto.GetErrors().FirstOrDefault().Value);
        }

        [Test]
        public void AdicionarComentario_DeveAdicionarComentario_QuandoValido()
        {
            // Arrange
            var tarefa = new Tarefa("Tarefa Teste", "Descrição", DateTime.Now.AddDays(1), Status.Pendente, Prioridade.Alta, _projeto.Id);
            _projeto.AdicionarTarefa(tarefa);

            // Act
            _projeto.AdicionarComentario(tarefa.Id, 1, "Comentário Teste");

            // Assert
            Assert.IsFalse(_projeto.IsValid == false);
            Assert.AreEqual(1, tarefa.Comentarios.Count);
        }

        [Test]
        public void AdicionarComentario_DeveRetornarErro_QuandoTarefaNaoEncontrada()
        {
            // Act
            _projeto.AdicionarComentario(999, 1, "Comentário Teste");

            // Assert
            Assert.IsTrue(_projeto.IsValid == false);
            Assert.AreEqual("Tarefa não encontrada.", _projeto.GetErrors().FirstOrDefault().Value);
        }

        [Test]
        public void TarefasConcluidas_DeveRetornarQuantidadeCorreta_QuandoPeriodoEDado()
        {
            // Arrange
            _projeto.AdicionarTarefa(new Tarefa("Tarefa 1", "Descrição", DateTime.Now, Status.Concluida, Prioridade.Alta, _projeto.Id));
            _projeto.AdicionarTarefa(new Tarefa("Tarefa 2", "Descrição", DateTime.Now.AddDays(-10), Status.Concluida, Prioridade.Alta, _projeto.Id));

            // Act
            var count = _projeto.TarefasConcluidas(7);

            // Assert
            Assert.AreEqual(1, count);
        }

        [Test]
        public void PossuiTarefasPendentes_DeveRetornarTrue_QuandoExisteTarefaPendente()
        {
            // Arrange
            _projeto.AdicionarTarefa(new Tarefa("Tarefa 1", "Descrição", DateTime.Now, Status.Pendente, Prioridade.Alta, _projeto.Id));

            // Act
            var result = _projeto.PossuiTarefasPendentes();

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void RemoverTarefa_DeveRemoverTarefa_QuandoTarefaExiste()
        {
            // Arrange
            var tarefa = new Tarefa("Tarefa Teste", "Descrição", DateTime.Now.AddDays(1), Status.Pendente, Prioridade.Alta, _projeto.Id);
            _projeto.AdicionarTarefa(tarefa);

            // Act
            _projeto.RemoverTarefa(tarefa.Id);

            // Assert
            Assert.IsFalse(_projeto.Tarefas.Contains(tarefa));
        }

        [Test]
        public void AtualizarTarefa_DeveAtualizar_QuandoValido()
        {
            // Arrange
            var tarefa = new Tarefa("Tarefa Teste", "Descrição", DateTime.Now.AddDays(1), Status.Pendente, Prioridade.Alta, _projeto.Id);
            _projeto.AdicionarTarefa(tarefa);

            // Act
            _projeto.AtualizarTarefa(tarefa.Id, "Novo Título", "Nova Descrição", DateTime.Now.AddDays(2), (int)Status.Concluida);

            // Assert
            Assert.AreEqual("Novo Título", tarefa.Titulo);
            Assert.AreEqual("Nova Descrição", tarefa.Descricao);
            Assert.AreEqual(Status.Concluida, tarefa.Status);
        }
    }
}
