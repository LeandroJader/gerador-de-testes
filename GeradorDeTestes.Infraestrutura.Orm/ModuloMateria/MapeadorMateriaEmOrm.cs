using GeradorDeTestes.Dominio.ModuloMateria;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GeradorDeTestes.Infraestrutura.Orm.ModuloMateria;

public class MapeadorMateriaEmOrm : IEntityTypeConfiguration<Materia>
{
    public void Configure(EntityTypeBuilder<Materia> builder)
    {
        builder.Property(x => x.Id);

        builder.Property(x => x.Nome)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.Serie)
            .IsRequired();

        builder.HasOne(m => m.Disciplina)
            .WithMany(d => d.Materias);

        builder.HasMany(x => x.Questoes)
            .WithOne(x => x.Materia);
    }
}
