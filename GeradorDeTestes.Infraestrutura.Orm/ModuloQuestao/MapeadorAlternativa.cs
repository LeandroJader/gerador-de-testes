using GeradorDeTestes.Dominio.ModuloQuestao;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GeradorDeTestes.Infraestrutura.Orm.ModuloQuestao;

public class MapeadorAlternativa : IEntityTypeConfiguration<Alternativa>
{
    public void Configure(EntityTypeBuilder<Alternativa> builder)
    {
        builder.Property(x => x.Id)
            .ValueGeneratedNever()
            .IsRequired();

        builder.Property(x => x.Resposta)
            .HasMaxLength(300)
            .IsRequired();

        builder.Property(x => x.Correta)
            .IsRequired();

        builder.HasOne(x => x.Questao)
            .WithMany(x => x.Alternativas)
            .IsRequired();
    }
}
