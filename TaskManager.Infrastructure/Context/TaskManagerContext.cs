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
            //modelBuilder.Entity<Usuario>(entity =>
            //{
            //    entity.HasKey(e => e.Id);
            //    entity.Property(e => e.Nome).IsRequired().HasMaxLength(100);
            //    entity.Property(e => e.Funcao).IsRequired(); // Configuração do relacionamento one-to-many
            //    entity.HasMany(u => u.Projetos).WithOne(p => p.Responsavel).HasForeignKey(p => p.ResponsavelId);
            //});


            modelBuilder.Entity<Projeto>().Property(e => e.Id).ValueGeneratedOnAdd().IsRequired();
            //modelBuilder.Entity<Projeto>(entity =>
            //{
            //    entity.HasKey(e => e.Id);
            //    entity.Property(e => e.Titulo).IsRequired().HasMaxLength(200); // Configuração do relacionamento many-to-one
            //    entity.HasOne(p => p.Responsavel).WithMany(u => u.Projetos).HasForeignKey(p => p.ResponsavelId);
            //});


            //modelBuilder.Entity<Projeto>().HasOne(p => p.Responsavel).WithMany(u => u.Projetos).HasForeignKey(p => p.ResponsavelId);

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
                                                             new Projeto("Website Corporativo", Usuarios.ElementAtOrDefault(0).Id), // Carlos Oliveira
                                                             new Projeto("Portal de Serviços", Usuarios.ElementAtOrDefault(0).Id), // Maria Souza
                                                             new Projeto("Aplicativo Mobile", Usuarios.ElementAtOrDefault(0).Id) }; // Paulo Lima
                Projetos.AddRange(projetos);
                SaveChanges();
            }

            //if (!Tarefas.Any())
            //{
            //    var tarefaFaker = new AutoFaker<Tarefa>("pt_BR");
            //    var tarefas = tarefaFaker.Generate(10);
            //    Tarefas.AddRange(tarefas);
            //    SaveChanges();
            //}
        }
    }
}