//Nome e prontuário da dupla:
//Lucas da Silva Santos CB3030598
//Kaueh Farias Ferreira dos Santos CB3031438

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TP04.Domain.Models;

namespace TP04.Web.Data.Configurations
{
    public class TarefaConfiguration : IEntityTypeConfiguration<Tarefa>
    {
        public void Configure(EntityTypeBuilder<Tarefa> builder)
        {
            builder.ToTable("Tarefas");

            builder.HasKey(tarefa => tarefa.Id);

            builder.Property(tarefa => tarefa.Titulo)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(tarefa => tarefa.Descricao)
                .HasMaxLength(500);

            builder.Property(tarefa => tarefa.DataCriacao)
                .IsRequired();

            builder.Property(tarefa => tarefa.DataVencimento)
                .IsRequired();

            builder.Property(tarefa => tarefa.Status)
                .HasConversion<string>()
                .IsRequired();

            builder.Property(tarefa => tarefa.Prioridade)
                .HasConversion<string>()
                .IsRequired();
        }
    }
}