using GeradorDeTestes.Dominio.ModuloTeste;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GeradorDeTestes.Infraestrutura.Orm.ModuloTeste;

public class MapeadorTesteEmOrm : IEntityTypeConfiguration<Teste>
{
    public void Configure(EntityTypeBuilder<Teste> builder)
    {
        builder.Property(t => t.Id)
            .ValueGeneratedNever();

        builder.Property(t => t.Titulo)
            .IsRequired()
            .HasMaxLength(200);

        builder.HasIndex(t => t.Titulo)
            .IsUnique();

        builder.Property(t => t.QuantidadeQuestoes)
            .IsRequired();

        builder.Property(t => t.Serie)
            .IsRequired();

        builder.Property(t => t.TipoTeste)
            .IsRequired();

        builder.HasOne(t => t.Disciplina)
            .WithMany(x => x.Testes)
            .IsRequired();

        builder.HasOne(t => t.Materia)
            .WithMany(x => x.Testes);
    }
}
