using eAgenda.Dominio.Modulos.Compromissos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eAgenda.Infraestrutura.Modulos.Compromissos;

public sealed class CompromissoConfiguration : IEntityTypeConfiguration<Compromisso>
{
    public void Configure(EntityTypeBuilder<Compromisso> builder)
    {
        builder.ToTable("TBCompromissos");

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).ValueGeneratedNever();

        builder.Property(c => c.Assunto)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(c => c.DataDeOcorrencia)
            .HasColumnType("date")
            .IsRequired();

        builder.Property(c => c.HoraDeInicio)
            .HasColumnType("time")
            .IsRequired();

        builder.Property(c => c.HoraDeTermino)
            .HasColumnType("time")
            .IsRequired();

        builder.Property(c => c.Tipo)
            .HasConversion<string>()
            .IsRequired();

        builder.Property(c => c.Local)
            .HasMaxLength(200)
            .IsRequired();

        builder.HasOne(c => c.Contato)
            .WithMany()
            .IsRequired(false)
            .OnDelete(DeleteBehavior.SetNull);
    }
}

