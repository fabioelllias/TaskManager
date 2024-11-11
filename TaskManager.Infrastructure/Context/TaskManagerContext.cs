﻿using AutoBogus;
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
                List<Projeto> projetos = new List<Projeto> { new Projeto("Sistema de Gerenciamento", Usuarios.ElementAtOrDefault(0)), // João Silva
                                                             new Projeto("Aplicativo de Vendas", Usuarios.ElementAtOrDefault(0)), // Ana Santos
                                                             new Projeto("Website Corporativo", Usuarios.ElementAtOrDefault(0)), // Carlos Oliveira
                                                             new Projeto("Portal de Serviços", Usuarios.ElementAtOrDefault(0)), // Maria Souza
                                                             new Projeto("Aplicativo Mobile", Usuarios.ElementAtOrDefault(0)) }; // Paulo Lima
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