
using Moq;
using TaskManager.Application;
using TaskManager.Domain.Entitys;
using TaskManager.Infrastructure.Interfaces;
using TaskManager.ViewModel.Projeto;
using TaskManager.ViewModel.Tarefa;

namespace TaskManager.UnitTest;

[TestFixture]
public class ProjetoApplicationTests
{
    private Mock<IRepository<Projeto>> _projetoRepositoryMock;
    private Mock<IRepository<Usuario>> _usuarioRepositoryMock;
    private ProjetoApplication _projetoApplication;

    [SetUp]
    public void SetUp()
    {
        _projetoRepositoryMock = new Mock<IRepository<Projeto>>();
        _usuarioRepositoryMock = new Mock<IRepository<Usuario>>();
        _projetoApplication = new ProjetoApplication(_projetoRepositoryMock.Object, _usuarioRepositoryMock.Object);
    }

    [Test]
    public void AtualizarTarefa_DeveRetornarErro_QuandoProjetoNaoEncontrado()
    {
        // Arrange
        _projetoRepositoryMock.Setup(repo => repo.GetById(It.IsAny<int>(), "Tarefas")).Returns((Projeto)null);

        // Act
        var result = _projetoApplication.AtualizarTarefa(1, 1, 1, new TarefaAtualizarViewModel());

        // Assert
        Assert.IsFalse(result.Success);
        Assert.AreEqual("Projeto não encontrado.", result.Message);
    }

    [Test]
    public void AtualizarTarefa_DeveRetornarErro_QuandoUsuarioNaoEncontrado()
    {
        // Arrange
        var projeto = new Projeto();
        _projetoRepositoryMock.Setup(repo => repo.GetById(It.IsAny<int>(), "Tarefas")).Returns(projeto);
        _usuarioRepositoryMock.Setup(repo => repo.GetAll()).Returns(Enumerable.Empty<Usuario>().AsQueryable());

        // Act
        var result = _projetoApplication.AtualizarTarefa(1, 1, 1, new TarefaAtualizarViewModel());

        // Assert
        Assert.IsFalse(result.Success);
        Assert.AreEqual("Usuário não encontrado.", result.Message);
    }

    [Test]
    public void IncluirComentarioNaTarefa_DeveRetornarErro_QuandoProjetoNaoEncontrado()
    {
        // Arrange
        _projetoRepositoryMock.Setup(repo => repo.GetById(It.IsAny<int>(), "Tarefas")).Returns((Projeto)null);

        // Act
        var result = _projetoApplication.IncluirComentarioNaTarefa(1, 1, 1, "Comentário");

        // Assert
        Assert.IsFalse(result.Success);
        Assert.AreEqual("Projeto não encontrado.", result.Message);
    }

    [Test]
    public void CriarProjeto_DeveRetornarErro_QuandoUsuarioNaoEncontrado()
    {
        // Arrange
        _usuarioRepositoryMock.Setup(repo => repo.GetAll()).Returns(Enumerable.Empty<Usuario>().AsQueryable());

        // Act
        var result = _projetoApplication.CriarProjeto(new ProjetoCriarViewModel { UsuarioId = 1 });

        // Assert
        Assert.IsFalse(result.Success);
        Assert.AreEqual("Usuário não encontrado.", result.Message);
    }

    [Test]
    public void TarefasPorProjeto_DeveRetornarSucesso_QuandoProjetoExists()
    {
        // Arrange
        var projeto = new Projeto();
        _projetoRepositoryMock.Setup(repo => repo.GetById(It.IsAny<int>(), "Tarefas")).Returns(projeto);

        // Act
        var result = _projetoApplication.TarefasPorProjeto(1);

        // Assert
        Assert.IsTrue(result.Success);
    }
}
