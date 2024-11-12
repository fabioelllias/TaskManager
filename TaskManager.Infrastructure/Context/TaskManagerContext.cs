using Microsoft.EntityFrameworkCore;
using TaskManager.Domain.Entitys;
using TaskManager.Domain.Enuns;
using TaskManager.Infrastructure.Interfaces;

namespace TaskManager.Infrastructure.Context
{
    public class TaskManagerContext : DbContext, IUnitOfWork
    {
        public TaskManagerContext(DbContextOptions<TaskManagerContext> options) : base(options) { }

        public virtual DbSet<Usuario> Usuarios { get; set; }
        public virtual DbSet<Projeto> Projetos { get; set; }
        public virtual DbSet<Tarefa> Tarefas { get; set; }
        public virtual DbSet<TarefaComentario> ComentariosDasTarefas { get; set; }
        public virtual DbSet<TarefaHistorico> HistoricoDeTarefas { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=postgres;Database=dockerdb;Username=postgres;Password=postgres");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>().Property(e => e.Id).ValueGeneratedOnAdd().IsRequired();
            modelBuilder.Entity<Projeto>().Property(e => e.Id).ValueGeneratedOnAdd().IsRequired();
            modelBuilder.Entity<Tarefa>().Property(e => e.Id).ValueGeneratedOnAdd().IsRequired();
            modelBuilder.Entity<TarefaComentario>().Property(e => e.Id).ValueGeneratedOnAdd().IsRequired();
            modelBuilder.Entity<TarefaHistorico>().Property(e => e.Id).ValueGeneratedOnAdd().IsRequired();
        }

        public void SeedData()
        {
            if (!Usuarios.Any())
            {
                List<Usuario> usuarios = new List<Usuario> {
                    new Usuario("João Silva", Funcao.Desenvolvedor),
                    new Usuario("Ana Santos", Funcao.Gerente),
                    new Usuario("Carlos Oliveira", Funcao.Desenvolvedor),
                    new Usuario("Maria Souza", Funcao.Gerente),
                    new Usuario("Paulo Lima", Funcao.Desenvolvedor) };
                Usuarios.AddRange(usuarios);
                SaveChanges();
            }

            if (!Projetos.Any())
            {
                List<Projeto> projetos = new List<Projeto> { new Projeto("Sistema de Gerenciamento", Usuarios.ElementAtOrDefault(0).Id), // João Silva
                                                             new Projeto("Aplicativo de Vendas", Usuarios.ElementAtOrDefault(0).Id), // Ana Santos
                                                             new Projeto("Website Corporativo", Usuarios.ElementAtOrDefault(1).Id), // Carlos Oliveira
                                                             new Projeto("Portal de Serviços", Usuarios.ElementAtOrDefault(2).Id), // Maria Souza
                                                             new Projeto("Aplicativo Mobile", Usuarios.ElementAtOrDefault(0).Id) }; // Paulo Lima
                Projetos.AddRange(projetos);
                SaveChanges();
            }

            if (!Tarefas.Any())
            {
                List<Tarefa> tarefas = new List<Tarefa>                    {
                   new Tarefa("Desenvolver Feature A", "Implementar a funcionalidade A.", DateTime.Now.AddDays(-7).ToUniversalTime(), Status.Pendente, Prioridade.Alta, Projetos.ElementAtOrDefault(0).Id),
                   new Tarefa("Corrigir Bug B", "Corrigir o bug B encontrado no módulo X.", DateTime.Now.AddDays(-2).ToUniversalTime(), Status.Concluida, Prioridade.Media,Projetos.ElementAtOrDefault(0).Id) ,
                   new Tarefa("Refatorar Código C", "Refatorar o código do componente C.", DateTime.Now.AddMonths(-1).ToUniversalTime(), Status.Pendente, Prioridade.Baixa,Projetos.ElementAtOrDefault(0).Id) ,
                   new Tarefa("Escrever Documentação D", "Escrever a documentação para a API D.", DateTime.Now.AddDays(-10).ToUniversalTime(), Status.Pendente, Prioridade.Media,Projetos.ElementAtOrDefault(1).Id) ,
                   new Tarefa("Testar Funcionalidade E", "Realizar testes unitários na funcionalidade E.", DateTime.Now.AddDays(-5).ToUniversalTime(), Status.Concluida, Prioridade.Alta,Projetos.ElementAtOrDefault(2).Id),
                   new Tarefa("Desenvolver Feature ADB", "Implementar a funcionalidade AB.", DateTime.Now.AddDays(-37).ToUniversalTime(), Status.Pendente, Prioridade.Alta, Projetos.ElementAtOrDefault(2).Id),
                   new Tarefa("Corrigir Bug BDBC", "Corrigir o bug B encontrado no módulo XX.", DateTime.Now.AddDays(-2).ToUniversalTime(), Status.Concluida, Prioridade.Media,Projetos.ElementAtOrDefault(2).Id) ,
                   new Tarefa("Refatorar Código CC", "Refatorar o código do componente CX.", DateTime.Now.AddMonths(-31).ToUniversalTime(), Status.Pendente, Prioridade.Baixa,Projetos.ElementAtOrDefault(1).Id) ,
                   new Tarefa("Escrever Documentação CD", "Escrever a documentação para a API DD.", DateTime.Now.AddDays(-10).ToUniversalTime(), Status.Pendente, Prioridade.Media,Projetos.ElementAtOrDefault(1).Id) ,
                   new Tarefa("Testar Funcionalidade BE", "Realizar testes unitários na funcionalidade RE.", DateTime.Now.AddDays(-65).ToUniversalTime(), Status.Concluida, Prioridade.Alta,Projetos.ElementAtOrDefault(2).Id)
                   };
                Tarefas.AddRange(tarefas);
                SaveChanges();
            }
        }
    }
}