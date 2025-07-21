using GeradorDeTestes.Dominio.ModuloDisciplina;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeradorDeTestes.Infraestrutura.Orm.ModuloDisciplina
{
    public class MapeadorDisciplinaEmOrm : IEntityTypeConfiguration<Disciplina>
    {
        public void Configure(EntityTypeBuilder<Disciplina> builder)
        {
            builder.Property(x => x.Id)
                .ValueGeneratedNever()
                .IsRequired();
            
            builder.Property(x => x.Nome)
                .IsRequired();                         
        }
    }
}
